using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ocean2City.Business.Interfaces;
using Ocean2City.Common.CommonData;
using Ocean2City.Common.Extensions;
using Ocean2City.ViewModel.Category;

namespace Ocean2City.WebApi.Controllers
{
    /// <summary>
    /// Category Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Category/[Action]")]
    public class CategoryController : Controller
    {
       
        private readonly ICategoryManagerService _categoryManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the CategoryController
        /// </summary>
        /// <param name="categoryManager"></param>
        /// <param name="hostingEnvironment"></param>
        public CategoryController(ICategoryManagerService categoryManager, IHostingEnvironment hostingEnvironment)
        {
            _categoryManager = categoryManager;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Get All Categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IResult Categories()
        {
            var categories = _categoryManager.GetAllCategories();
            return categories;
        }

        /// <summary>
        /// Get a specific Category by its identifier.
        /// </summary>
        /// <returns>The specific category.</returns>
        /// <param name="id">Identifier of the Category.</param>
        [HttpGet]
        public IResult Details(string id)
        {
            var category = _categoryManager.GetCategoryById(id);
            return category;
        }

        /// <summary>
        /// Add new category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IResult AddCategory()
        {
            IResult category = null;
            var categoryViewModel = JsonConvert.DeserializeObject<CategoryViewModel>(Request.Form["model"]);
            if (categoryViewModel != null)
            {
                var image = Request.Form.Files["image"];
                if (image != null)
                {
                    var docName = categoryViewModel.CategoryName + FileHelper.GetExtension(image);
                    categoryViewModel.Image = docName;
                    categoryViewModel.ImagePath  = FileHelper.SaveFile(image, docName, _hostingEnvironment, "uploadImages");
                }
            }
            category = _categoryManager.AddCategory(categoryViewModel);
            return category;
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IResult UpdateCategory()
        {
            IResult category = null;
            var categoryViewModel = JsonConvert.DeserializeObject<CategoryViewModel>(Request.Form["model"]);
            if (categoryViewModel != null)
            {
                var image = Request.Form.Files["image"];
                if (image != null)
                {
                    var docName = categoryViewModel.CategoryName + FileHelper.GetExtension(image);
                    categoryViewModel.Image = docName;
                    categoryViewModel.ImagePath = FileHelper.SaveFile(image, docName, _hostingEnvironment, "uploadImages");
                }
            }
            category = _categoryManager.UpdateCategory(categoryViewModel);
            return category;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Ocean2City.Business.Interfaces;
using Ocean2City.Common.CommonData;
using Newtonsoft.Json;
using Ocean2City.ViewModel.Item;
using Ocean2City.Common.Extensions;

namespace Ocean2City.WebApi.Controllers
{
    /// <summary>
    /// Item Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Item/[Action]")]
    public class ItemController : Controller
    {
        private readonly IItemManagerService _itemManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the ItemController
        /// </summary>
        /// <param name="itemManager"></param>
        /// <param name="hostingEnvironment"></param>
        public ItemController(IItemManagerService itemManager, IHostingEnvironment hostingEnvironment)
        {
            _itemManager = itemManager;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Get All Items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IResult Items()
        {
            var items = _itemManager.GetAllItems();
            return items;
        }

        /// <summary>
        /// Get a specific Item by its identifier.
        /// </summary>
        /// <returns>The specific item.</returns>
        /// <param name="id">Identifier of the Item.</param>
        [HttpGet]
        public IResult Details(string id)
        {
            var item = _itemManager.GetItemById(id);
            return item;
        }

        /// <summary>
        /// Get Items by it category id.
        /// </summary>
        /// <returns>The items.</returns>
        /// <param name="id">Identifier of the category.</param>
        [HttpGet]
        public IResult GetItemsByCategory(string id)
        {
            var items = _itemManager.GetItemByCategory(id);
            return items;
        }

        /// <summary>
        /// Add new item.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IResult AddItem()
        {
            IResult item = null;
            var itemViewModel = JsonConvert.DeserializeObject<ItemViewModel>(Request.Form["model"]);
            if (itemViewModel != null)
            {
                var image = Request.Form.Files["image"];
                if (image != null)
                {
                    FileHelper.SaveFile(image, _hostingEnvironment, "uploadImages");
                    itemViewModel.Image = image.FileName;
                }
            }
            item = _itemManager.AddItem(itemViewModel);
            return item;
        }

        /// <summary>
        /// Update item.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IResult UpdateItem()
        {
            IResult item = null;
            var itemViewModel = JsonConvert.DeserializeObject<ItemViewModel>(Request.Form["model"]);
            if (itemViewModel != null)
            {
                var image = Request.Form.Files["image"];
                if (image != null)
                {
                    FileHelper.SaveFile(image, _hostingEnvironment, "uploadImages");
                    itemViewModel.Image = image.FileName;
                }
            }
            item = _itemManager.UpdateItem(itemViewModel);
            return item;
        }
    }
}
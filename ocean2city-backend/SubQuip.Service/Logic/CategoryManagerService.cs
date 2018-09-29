using Ocean2City.Business.Interfaces;
using System;
using Ocean2City.Common.CommonData;
using Ocean2City.ViewModel.Category;
using Ocean2City.Data.Interfaces;
using Ocean2City.Common.Enums;
using Ocean2City.Entity.Models;
using Ocean2City.Common.Extensions;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Ocean2City.Business.Logic
{
    public class CategoryManagerService : ICategoryManagerService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ClaimsPrincipal _principal;

        public CategoryManagerService(ICategoryRepository categoryRepository, IPrincipal principal)
        {
            _categoryRepository = categoryRepository;
            _principal = principal as ClaimsPrincipal;
        }

        /// <summary>
        /// Get All Categories.
        /// </summary>
        /// <returns></returns>
        public IResult GetAllCategories()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var categoryLists = new List<CategoryViewModel>();
                var categories = _categoryRepository.Query.ToList();
                if (categories != null && categories.Any())
                {
                    result.Body = categories.Select(x =>
                    {
                        var category = new CategoryViewModel();
                        category.MapFromModel(x);
                        category.ImagePath = FileHelper.GetBase64StringForImage(x.ImagePath);
                        return category;
                    }).ToList();
                }
                else
                {
                    result.Message = CommonErrorMessages.NoResultFound;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        /// <summary>
        /// Get Specific Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult GetCategoryById(string id)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    CategoryViewModel categoryViewModel = null;
                    var category = _categoryRepository.GetOne(t => t.CategoryId == ObjectId.Parse(id));
                    if (category != null)
                    {
                        categoryViewModel = new CategoryViewModel();
                        result.Body = categoryViewModel.MapFromModel(category);
                    }
                    else
                    {
                        result.Message = CategoryNotification.NoCategory;
                    }
                }
                else
                {
                    result.Status = Status.Fail;
                    result.Message = CommonErrorMessages.NoIdentifierProvided;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        /// <summary>
        /// Add a Category
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        public IResult AddCategory(CategoryViewModel categoryViewModel)
        {
            categoryViewModel.CategoryId = null;
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                Category category = null;
                if (categoryViewModel != null)
                {
                    category = new Category();
                    // category.MapFromViewModel(categoryViewModel, (ClaimsIdentity)_principal.Identity);
                    category.MapFromViewModel(categoryViewModel);
                    _categoryRepository.InsertOne(category);
                    result.Body = categoryViewModel.CategoryName;
                    result.Message = CategoryNotification.Added;
                }
                else
                {
                    result.Message = CategoryNotification.CategoryNotProvided;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        /// <summary>
        ///  Update Category
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        public IResult UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var result = new Result
            {
                Operation = Operation.Update,
                Status = Status.Success
            };
            try
            {
                var updateDefinition = Builders<Category>.Update
                    .Set(x => x.CategoryName, categoryViewModel.CategoryName)
                    .Set(x => x.Image, categoryViewModel.Image)
                    .Set(x => x.IsAvailable, categoryViewModel.IsAvailable)
                    .Set(x => x.ModifiedDate, DateTime.Now);
                _categoryRepository.UpdateOne(t => t.CategoryId == ObjectId.Parse(categoryViewModel.CategoryId), updateDefinition);

                result.Message = CategoryNotification.Updated;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }
    }
}

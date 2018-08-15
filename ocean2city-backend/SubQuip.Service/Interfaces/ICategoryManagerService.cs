using Ocean2City.Common.CommonData;
using Ocean2City.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Business.Interfaces
{
    public interface ICategoryManagerService
    {
        /// <summary>
        /// Get Specific Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IResult GetCategoryById(string id);

        /// <summary>
        /// Get All Categories.
        /// </summary>
        /// <returns></returns>
        IResult GetAllCategories();

        /// <summary>
        /// Add New Category
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        IResult AddCategory(CategoryViewModel categoryViewModel);

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        IResult UpdateCategory(CategoryViewModel categoryViewModel);
    }
}

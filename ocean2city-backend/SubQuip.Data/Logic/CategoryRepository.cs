using Microsoft.Extensions.Configuration;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Data.Logic
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        public IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the CategoryRepository
        /// </summary>
        /// <param name="configuration"></param>
        public CategoryRepository(IConfiguration configuration) : base(configuration, "category")
        {
            _configuration = configuration;
        }
    }
}

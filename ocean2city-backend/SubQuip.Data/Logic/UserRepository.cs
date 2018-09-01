using Microsoft.Extensions.Configuration;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Data.Logic
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the UserRepository
        /// </summary>
        /// <param name="configuration"></param>
        public UserRepository(IConfiguration configuration) : base(configuration, "user")
        {
            _configuration = configuration;
        }
    }
}

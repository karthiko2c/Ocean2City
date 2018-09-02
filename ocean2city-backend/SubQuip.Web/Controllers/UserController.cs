using Microsoft.AspNetCore.Mvc;
using Ocean2City.Business.Interfaces;
using Ocean2City.Common.CommonData;

namespace Ocean2City.WebApi.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/User/[Action]")]
    public class UserController : Controller
    {
        private readonly IUserManagerService _userManager;
        /// <summary>
        /// Initializes a new instance of the UserController
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(IUserManagerService userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get All Users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IResult Users()
        {
            var users = _userManager.GetAllUsers();
            return users;
        }

        /// <summary>
        /// Get a specific User by its identifier.
        /// </summary>
        /// <returns>The specific user.</returns>
        /// <param name="id">Identifier of the user.</param>
        [HttpGet]
        public IResult Details(string id)
        {
            var users = _userManager.GetUserById(id);
            return users;
        }
    }
}
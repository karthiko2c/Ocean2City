using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ocean2City.Common.CommonData;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ocean2City.ViewModel.User;
using Ocean2City.Common.Enums;

namespace Ocean2City.WebApi.Controllers
{
    /// <summary>
    /// Login Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login/[Action]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SubQuip.WebApi.Controllers.LoginController"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <returns>Access token for the logged in user</returns>
        /// <param name="loginViewModel">Login view model.</param>
        [HttpPost]
        public IResult LoginUser([FromBody]UserLoginViewModel loginViewModel)
        {          
            var userResult = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                if (loginViewModel.UserName == "ocean2city" && loginViewModel.UserPassword == "ocean2city")
                {
                    var token = new ObjectResult(GenerateToken());
                    userResult.Body = token.Value;
                }
                else
                {
                    userResult.Body = Unauthorized();
                }
                return userResult;
            }
            catch (Exception e)
            {
                userResult.Message = e.Message;
                userResult.Status = Status.Fail;
            }
            return userResult;
        }

        private string GenerateToken()
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "ocean2city"),
                new Claim(ClaimTypes.Email,"karthikkharvi25@gmail.com"),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
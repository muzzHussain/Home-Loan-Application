using BusinessLogic;
using BusinessLogic.DTO;
using HomeLoanAPI.AuthorizationLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    ///<summary>
    ///     Provide functionality to the /User/ route
    ///</summary>
    public class UserController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private ILogger _logger;
        private IConfiguration _config;

        public UserController(IBusinessLogic businessLogic, ILogger logger, IConfiguration config)
        {
            _businessLogic = businessLogic;
            _logger = logger;
            _config = config;            
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetClaim()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault((x) => x.Type == ClaimTypes.Email)?.Value;
        }

        /// <summary>
        ///     To register User
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegisterActionTask([FromBody] RegisterUserDTO userDTO)
        {
            _logger.LogInfo("-->Entered UserRegisterActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->User registeration details are incorrect");
                return StatusCode(400, "User registeration details are incorrect");
            }
            bool response = await _businessLogic.GetUserBusinessLogic().RegisterTask(userDTO);
            if (response)
            {
                _logger.LogInfo("-->New user has been registered");
                return StatusCode(201, response);
            }
            else
            {
                _logger.LogInfo("-->New user has not been registered");
                return StatusCode(200);
            }
        }
        
        /// <summary>
        ///     To update user password
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserChangePasswordActionTask([FromBody] UpdatePasswordDTO entity)
        {
            _logger.LogInfo("-->Entered UserChangePasswordActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->User update password details are Incorrect");
                return StatusCode(400, false);
                //return StatusCode(400, "User update password details are Incorrect");
            }
            bool result = await _businessLogic.GetUserBusinessLogic().UpdatePasswordTask(GetClaim(), entity);
            if (result == true)
            {
                _logger.LogInfo("-->User Password Changed Successfully");
                return StatusCode(202, true);
                //return StatusCode(202, "User Password Changed Successfully");
            }
            else
            {
                _logger.LogInfo("-->User Password Not Changed");
                return StatusCode(406, false);
                //return StatusCode(406, "User Password Not Changed");
            }
        }

        /// <summary>
        ///     To Login User
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserLoginActionTask(
        [FromHeader]
        [Required(ErrorMessage = "User email id is required")]
        [EmailAddress]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "User email id format is not correct")]
        string emailId, 
        [FromHeader]
        [Required(ErrorMessage = "User password is required")]
        [PasswordPropertyText(true)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "User password format is not correct")]
        string password)
        {
            _logger.LogInfo("-->Entered UserLoginActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->User login Details are incorrect");
                return StatusCode(400, "User login Details are incorrect");
            }
            bool result = await _businessLogic.GetUserBusinessLogic().LoginTask(emailId, password);
            if (result == true)
            {
                GenerateToken _generateToken = new GenerateToken(emailId, _config, "User");
                _logger.LogInfo("-->User Login Sucessful");
                return StatusCode(202, _generateToken.GetToken());
            }
            else
            {
                _logger.LogInfo("-->User Login Failed, email id or password is incorrect");
                return StatusCode(401, "User Login Failed, email id or password is incorrect");
            }
        }

    }
}

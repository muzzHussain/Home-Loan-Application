using BusinessLogic;
using BusinessLogic.DTO;
using HomeLoanAPI.AuthorizationLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;
using static Utility.Enums;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    ///<summary>
    ///     Provide functionality to the /Advisor/ route
    ///</summary>
    public class AdvisorController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private ILogger _logger;
        private IConfiguration _config;
        public AdvisorController(IBusinessLogic businessLogic, ILogger logger, IConfiguration config)
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
        ///     To Register Advisor
        /// </summary>
        /// <param name="advisorDTO"></param>
        /// <returns type = "IActionResult"></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AdvisorRegisterActionTask([FromBody] AdvisorRegisterDTO advisorDTO)
        {
            _logger.LogInfo("-->Entered AdvisorRegisterActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Advisor register details are incorrect");
                return StatusCode(400, "Advisor register details are incorrect");
            }
            bool response = await _businessLogic.GetAdvisorBusinessLogic().RegisterTask(advisorDTO);
            if (response)
            {
                _logger.LogInfo("-->New advisor has been registered");
                return StatusCode(201, response);
            }
            else
            {
                _logger.LogInfo("-->New advisor has not been registered");
                return StatusCode(200);
            }
        }

        /// <summary>
        ///     To Login Advisor
        /// </summary>
        /// <param name="advisor"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AdvisorLoginActionTask(
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
            _logger.LogInfo("-->Entered AdvisorLoginActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Advisor login details are incorrect");
                return StatusCode(400, "Advisor login details are incorrect");
            }
            bool result = await _businessLogic.GetAdvisorBusinessLogic().LoginTask(emailId, password);
            if (result == true)
            {
                GenerateToken _generateToken = new GenerateToken(emailId, _config, "Advisor");
                _logger.LogInfo("-->Advisor login successfully");
                return StatusCode(202, _generateToken.GetToken());
            }
            else
            {
                _logger.LogInfo("-->Advisor login failed");
                return StatusCode(401, "Advisor Details Incorrect");
            }
        }

        /// <summary>
        ///     To update Advisor Password
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPut]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> AdvisorChangePasswordActionTask([FromBody] UpdatePasswordDTO entity)
        {
            _logger.LogInfo("-->Entered AdvisorLoginActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Advisor change password details are incorrect");
                return StatusCode(400, false);
                //return StatusCode(400, "Advisor change password details are incorrect");
            }
            bool result = await _businessLogic.GetAdvisorBusinessLogic().UpdatePasswordTask(GetClaim(), entity);
            if (result == true)
            {
                _logger.LogInfo("-->Advisor password Changed Sucessfully");
                return StatusCode(202, true);
                //return StatusCode(202, "Advisor password Changed Sucessfully");
            }
            else
            {
                _logger.LogInfo("-->Advisor password Not Changed");
                return StatusCode(406, false);
                //return StatusCode(406, "Advisor password Not Changed");
            }
        }               
     
    }
}

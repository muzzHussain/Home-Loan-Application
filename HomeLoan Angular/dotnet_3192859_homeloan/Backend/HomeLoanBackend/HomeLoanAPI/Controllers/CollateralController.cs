using BusinessLogic;
using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;
using Microsoft.AspNetCore.Authorization;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollateralController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private ILogger _logger;
        private IConfiguration _config;
        public CollateralController(IBusinessLogic businessLogic, ILogger logger, IConfiguration config)
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
        ///     To create Collateral For current user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="IActionResult"></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateCollateralActionTask([FromBody] ApplyCollateralDTO entity)
        {
            _logger.LogInfo("-->Entered CreateCollateralActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Collateral Details are Incorrect");
                return StatusCode(400, "Collateral Details are Incorrect");
            }
            Guid id = await _businessLogic.GetCollateralBusinessLogic().AddCollateralByUserTask(GetClaim(), entity);
            _logger.LogInfo("-->Collateral is created");
            return StatusCode(201, $"Your collateral are submited against this {id}");
        }

        /// <summary>
        ///     To Edit Collateral of current user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="IActionResult></returns>
        [HttpPut("EditCollateralActionTask")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditCollateralActionTask([FromBody] EditCollateralDTO entity)
        {
            _logger.LogInfo("-->Entered EditCollateralActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Collateral Details are Incorrect");
                return StatusCode(400, false);
            }
            Guid id = await _businessLogic.GetCollateralBusinessLogic().EditCollateralByUserTask(GetClaim(), entity);
            _logger.LogInfo("-->Collateral is edited");
            return StatusCode(201, true);
        }

        /// <summary>
        ///     To remove collateral of current user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns type="IActionResult"></returns>
        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveCollateralActionTask([FromQuery][Required] Guid Id)
        {
            _logger.LogInfo("-->Entered RemoveCollateralActionTask action method");

            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Collateral id is incorrect");
                return StatusCode(400, false);
            }
            await _businessLogic.GetCollateralBusinessLogic().DeleteCollateralByUserTask(GetClaim(), Id);
            _logger.LogInfo("-->Your collateral is deleted");
            return StatusCode(201,true);
        }

        /// <summary>
        ///     Fetch All collateral using current user EmailID 
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllCollateralsTask()
        {
            _logger.LogInfo("-->Entered GetAllCollateralsForUserActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->User email id is incorrect");
                return StatusCode(400, "User email id is incorrect");
            }
            IEnumerable<DeleteCollateralDTO> listOfAllCollateralsForUserTask = await _businessLogic.GetCollateralBusinessLogic().GetAllCollateralByUserEmailTask(GetClaim());
            _logger.LogInfo("-->List of collaterals sucessfully retrieved");
            return StatusCode(200, listOfAllCollateralsForUserTask);
        }

        /// <summary>
        ///     To link collateral to loan application
        /// </summary>
        /// <param name="collaterId"></param>
        /// <param name="applicationId"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPost("LinkCollateralToLoanApplicationActionTask")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> LinkCollateralToLoanApplicationActionTask([FromQuery][Required] Guid collaterId, [FromQuery][Required] Guid applicationId)
        {
            _logger.LogInfo("-->Entered SetCollateralForApplicationActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Entered GetAllCollateralsForUserActionTask action method");
                return StatusCode(400, "CollateralId and applicationId Details are Incorrect");
            }
            bool result = await _businessLogic.GetCollateralBusinessLogic().SetCollateralToAnApplicationByUserTask(GetClaim(), collaterId, applicationId);
            if (result)
            {
                _logger.LogInfo("-->Collateral and application has been linked");
                return StatusCode(200, "Collateral and application has been linked");
            }
            else
            {
                _logger.LogInfo("-->Collateral and application linked failed");
                return StatusCode(200, "Collateral and application linked failed");
            }
        }
    }
}

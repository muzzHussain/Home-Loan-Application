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
using static Utility.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private ILogger _logger;
        private IConfiguration _config;
        public LoanController(IBusinessLogic businessLogic, ILogger logger, IConfiguration config)
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

        // Advisor specific location task


        /// <summary>
        ///     Get single applied Loan Application using ID
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetAnAppliedLoanApplicationByAdvisorTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAnAppliedLoanApplicationByAdvisorTask([FromQuery][Required] Guid applicationId)
        {
            _logger.LogInfo("-->Entered GetAnLoanApplicationTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->ApplicationId format is Incorrect");
                return StatusCode(400, "ApplicationId format is Incorrect");
            }
            AdvisorLoanApplicationDTO advisorLoanApplicationDTO = await _businessLogic.GetLoanBusinessLogic().FetchAnAppliedLoanApplicationByAdvisorTask(applicationId);
            _logger.LogInfo("-->Applied Loan application retrieved sucessfully by advisor");
            return StatusCode(200, advisorLoanApplicationDTO);
        }



        /// <summary>
        ///     Get single Loan Application using ID
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetAnLoanApplicationByAdvisorTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAnLoanApplicationByAdvisorTask([FromQuery][Required] Guid applicationId)
        {
            _logger.LogInfo("-->Entered GetAnLoanApplicationTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->ApplicationId format is Incorrect");
                return StatusCode(400, "ApplicationId format is Incorrect");
            }
            UserLoanApplicationDTO userLoanApplicationDTO = await _businessLogic.GetLoanBusinessLogic().FetchAnLoanApplicationByAdvisorTask(applicationId);
            _logger.LogInfo("-->Loan application retrieved sucessfully by advisor");
            return StatusCode(200, userLoanApplicationDTO);
        }


        /// <summary>
        ///     Get single Loan Application using User Email ID
        /// </summary>
        /// <param name="userEmailId"></param>
        /// <returns></returns>
        [HttpGet("GetAllAppliedLoanApplicationForUserByAdvisorTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAllAppliedLoanApplicationForUserByAdvisorTask([FromQuery][Required] string userEmailId)
        {
            _logger.LogInfo("-->Entered GetAllLoanApplicationForUser action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->User email id format is Incorrect");
                return StatusCode(400, "User email id format is Incorrect");
            }
            IEnumerable<AdvisorLoanApplicationDTO> listOfAdvisorLoanApplicationDTO = await _businessLogic.GetLoanBusinessLogic().FetchAllUserAppliedLoanApplicationByAdvisorTask(userEmailId);
            _logger.LogInfo("-->All Loan application for a user retrieved sucessfully by advisor");
            return StatusCode(200, listOfAdvisorLoanApplicationDTO);
        }


        /// <summary>
        ///     Get All applied  Loan Application
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllAppliedLoanApplicationByAdvisorTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAllAppliedLoanApplicationByAdvisorTask()
        {
            _logger.LogInfo("-->Entered GetAllAppliedLoanApplicationByAdvisorTask action method");
            IEnumerable<AdvisorLoanApplicationDTO> listOfAdvisorLoanApplicationDTO = await _businessLogic.GetLoanBusinessLogic().FetchAllAppliedLoanApplicationByAdvisorTask();
            _logger.LogInfo("-->All applied Loan application  retrieved sucessfully by advisor");
            return StatusCode(200, listOfAdvisorLoanApplicationDTO);
        }

        /// <summary>
        ///     Get All Loan Application
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllLoanApplicationByAdvisorTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAllLoanApplicationByAdvisorTask()
        {
            _logger.LogInfo("-->Entered GetAllLoanApplicationByAdvisorTask action method");
            IEnumerable<UserLoanApplicationDTO> listOfAdvisorLoanApplicationDTO = await _businessLogic.GetLoanBusinessLogic().FetchAllLoanApplicationByAdvisorTask();
            _logger.LogInfo("-->All Loan application retrieved sucessfully by advisor");
            return StatusCode(200, listOfAdvisorLoanApplicationDTO);
        }


        /// <summary>
        ///     To change Status of Loan Application
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPut("ChangeLoanStatusByAdvisorTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> ChangeLoanStatusByAdvisorTask([FromQuery][Required] Guid Id, [FromQuery][Required] string Status, [FromQuery] string? Notes)
        {
            _logger.LogInfo("-->Entered AdvisorChangeLoanStatusTask action method");
            if (!(Status == "Accepted" || Status == "Recommended" || Status == "Rejected"))
            {
                _logger.LogInfo("-->Status format is incorrect");
                return StatusCode(400, false);
            }
            LoanApplicationStatus loanApplicationStatus = LoanApplicationStatus.Applied;
            if (Status == "Accepted")
            {
                loanApplicationStatus = LoanApplicationStatus.Accepted;
            }
            else if (Status == "Recommended")
            {
                loanApplicationStatus = LoanApplicationStatus.InProgress;
            }
            else
            {
                loanApplicationStatus = LoanApplicationStatus.Rejected;
            }
            Guid id = await _businessLogic.GetLoanBusinessLogic().ChangeLoanApplicationStatusByAdvisorTask(Id, loanApplicationStatus, Notes);
            _logger.LogInfo("-->Loan Status Changed Sucessfully");
            return StatusCode(202, true);
        }



        // User specific location task

        /// <summary>
        ///     To Apply for loan
        /// </summary>
        /// <param name="loanDTO"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ApplyForLoanApplicationByUserTask([FromBody] ApplyLoanDTO loanDTO)
        {
            _logger.LogInfo("-->Entered ApplyForLoanActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Loan Details are Incorrect");
                return StatusCode(400, "Loan Details are Incorrect");
            }
            Guid id = await _businessLogic.GetLoanBusinessLogic().ApplyLoanApplicationByUserTask(GetClaim(), loanDTO);
            _logger.LogInfo("-->Loan application sucessful");
            return StatusCode(200, $"Your loan application is registered against this {id}");
        }

        /// <summary>
        /// To edit created loan application
        /// </summary>
        /// <param name="loanDTO"></param>
        /// <returns type="IActionResult></returns>
        [HttpPatch]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditLoanApplicationByUserTask([FromBody] EditLoanApplicationDTO loanDTO)
        {
            _logger.LogInfo("-->Entered EditLoanApplicationTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Loan Details are Incorrect");
                return StatusCode(400, "Loan Details are Incorrect");
            }
            bool response = await _businessLogic.GetLoanBusinessLogic().EditLoanApplicationByUserTask(GetClaim(), loanDTO);
            if (response)
            {
                _logger.LogInfo("-->Loan application edited sucessfully");
                return StatusCode(200, $"Loan application edited sucessfully");
            }
            else
            {
                _logger.LogInfo("-->Loan application was not edited");
                return StatusCode(200, $"Loan application was not edited");
            }
        }


        /// <summary>
        ///     To get all loan application using user email id
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllLoanApplicationByUserTask()
        {
            _logger.LogInfo("-->Entered GetAllLoanApplicationForUserTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->User email id is Incorrect");
                return StatusCode(400, "User email id is Incorrect");
            }
            IEnumerable<UserLoanApplicationDTO> listOfAllUserLoanApplications = await _businessLogic.GetLoanBusinessLogic().FetchAllLoanApplicationByUser(GetClaim());
            _logger.LogInfo("-->All user Loan application retrieved sucessfully");
            return StatusCode(200, listOfAllUserLoanApplications);
        }


        /// <summary>
        ///     To change status of loan Application by user Creater -> Applied
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPatch("ChangeApplicationStatusFromCreatedToAppliedByUserTask")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangeApplicationStatusFromCreatedToAppliedByUserTask([FromQuery][Required] Guid applicationId)
        {
            _logger.LogInfo("-->Entered ChangeStatusOfUserLoanApplication action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Application Id is Required");
                return StatusCode(400, "Application Id is Required");
            }
            bool result = await _businessLogic.GetLoanBusinessLogic().ChangeLoanApplicationStatusByUserTask(GetClaim(), applicationId);
            if (result)
            {
                _logger.LogInfo("Loan Application has been Applied");
                return StatusCode(200, "Loan Application has been Applied");
            }
            else
            {
                _logger.LogInfo("Loan Application cannot be Applied");
                return StatusCode(200, "Loan Application cannot be Applied");
            }
        }
    }
}

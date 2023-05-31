using BusinessLogic;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private ILogger _logger;
        private IConfiguration _config;
        public PromotionsController(IBusinessLogic businessLogic, ILogger logger, IConfiguration config)
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
        ///     To Add Promotion
        /// </summary>
        /// <param name="promotionsDTO"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPost]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> AddPromotionsActionTask([FromBody] PromotionsDTO promotionsDTO)
        {
            _logger.LogInfo("-->Entered AddPromotionsActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Promotions Details are Incorrect");
                return StatusCode(400, "Promotions Details are Incorrect");
            }
            //PromotionsDTO promotion = await _businessLogic.GetPromotionsBusinessLogic().AddNewPromotionByAdvisorTask(promotionsDTO);
            Guid Id = await _businessLogic.GetPromotionsBusinessLogic().AddNewPromotionByAdvisorTask(promotionsDTO);
            _logger.LogInfo("-->Promotions Added Sucessfully");
            return StatusCode(202, $"Promotions Added Sucessfully against {Id}");
        }

        /// <summary>
        ///     To delete promotion using Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> RemovePromotionsActionTask([FromQuery][Required] Guid Id)
        {
            _logger.LogInfo("-->Entered RemovePromotionsActionTask action method");

            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->promotion id is incorrect");
                return StatusCode(400, "promotion id is incorrect");
            }
            await _businessLogic.GetPromotionsBusinessLogic().RemovePromotionByAdvisorTask(Id);
            _logger.LogInfo("-->Your promotion is deleted");
            return StatusCode(201, $"Your promotion is deleted");
        }
        
        /// <summary>
        ///     Update Promotion 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> EditPromotionActionTask([FromBody] EditPromotionDTO entity)
        {
            _logger.LogInfo("-->Entered EditPromotionActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Promotion Details are Incorrect");
                return StatusCode(400, "Promotion Details are Incorrect");
            }
            Guid id = await _businessLogic.GetPromotionsBusinessLogic().EditPromotionByAdvisorTask(entity);
            _logger.LogInfo("-->Promotion is edited");
            return StatusCode(201, $"Your Promotion are edited against this {id}");
        }

        /// <summary>
        ///     Get Current Active Promotion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetOpenPromotionByAdvisorTask()
        {
            _logger.LogInfo("-->Entered GetOpenPromotionByAdvisorTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Promotion Id is incorrect");
                return StatusCode(400, "Promotion id is incorrect");
            }
            PromotionsDTO promotionByAdvisorTask = await _businessLogic.GetPromotionsBusinessLogic().GetOpenPromotionByAdvisorTask();
            _logger.LogInfo("-->active promotion sucessfully retrieved");
            return StatusCode(200, promotionByAdvisorTask);
        }
    }
}

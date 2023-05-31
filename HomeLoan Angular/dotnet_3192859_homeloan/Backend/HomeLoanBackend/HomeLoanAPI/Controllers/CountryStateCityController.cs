using BusinessLogic;
using BusinessLogic.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
    public class CountryStateCityController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private ILogger _logger;
        private IConfiguration _config;
        public CountryStateCityController(IBusinessLogic businessLogic, ILogger logger, IConfiguration config)
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
        ///     To add Country Code into Database
        /// </summary>
        /// <param name="countryDTO"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPost("AddCountryCodeTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> AddCountryCodeTask([FromBody][Required] CountryDTO countryDTO)
        {
            _logger.LogInfo("-->Entered AddCountryCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Country details are Incorrect");
                return StatusCode(400, "Country details are Incorrect");
            }
            Guid id = await _businessLogic.GetCountryStateCityBusinessLogic().AddCountryCodeByAdvisor(countryDTO);
            _logger.LogInfo("-->Country is Added");
            return StatusCode(201, $" Country is Added , Id: {id}");
        }

        /// <summary>
        ///     To add State Code into Database
        /// </summary>
        /// <param name="stateDTO"></param>
        /// <returns></returns>
        [HttpPost("AddStateCodeTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> AddStateCodeTask([FromBody][Required] StateDTO stateDTO)
        {
            _logger.LogInfo("-->Entered AddStateCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->State details are Incorrect");
                return StatusCode(400, "State details are Incorrect");
            }
            Guid id = await _businessLogic.GetCountryStateCityBusinessLogic().AddStateCodeByAdvisor(stateDTO);
            _logger.LogInfo("-->state is Added");
            return StatusCode(201, $" State is Added , Id: {id}");
        }

        /// <summary>
        ///     To add City Code into Database
        /// </summary>
        /// <param name="cityDTO"></param>
        /// <returns type="IActionResult"></returns>
        [HttpPost("AddCityCodeTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> AddCityCodeTask([FromBody][Required] CityDTO cityDTO)
        {
            _logger.LogInfo("-->Entered AddCityCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->City details are Incorrect");
                return StatusCode(400, "City details are Incorrect");
            }
            Guid id= await _businessLogic.GetCountryStateCityBusinessLogic().AddCityCodeByAdvisor(cityDTO);
            _logger.LogInfo("-->City is Added");
            return StatusCode(201, $" City is Added , Id: {id}");
        }

        /// <summary>
        ///     Get Country Code using Country Name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetCountryCodeTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetCountryCodeTask([FromQuery][Required] string countryName)
        {
            _logger.LogInfo("-->Entered GetCountryCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Country name format is Incorrect");
                return StatusCode(400, "Country name format is Incorrect");
            }
            string countryCode = await _businessLogic.GetCountryStateCityBusinessLogic().GetCountryCodeForCountryNameByAdvisor(countryName);
            _logger.LogInfo("-->Country code retrieved Sucessfully");
            return StatusCode(202, countryCode);
        }

        /// <summary>
        ///     Get State Code using State Name
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetStateCodeTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetStateCodeTask([FromQuery][Required] string stateName)
        {
            _logger.LogInfo("-->Entered GetStateCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->State name format is Incorrect");
                return StatusCode(400, "State name format is Incorrect");
            }
            string stateCode = await _businessLogic.GetCountryStateCityBusinessLogic().GetStateCodeForStateNameByAdvisor(stateName);
            _logger.LogInfo("-->State code retrieved Sucessfully");
            return StatusCode(202, stateCode);
        }

        /// <summary>
        ///     Get City Code using City Name
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetCityCodeTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetCityCodeTask([FromQuery][Required] string cityName)
        {
            _logger.LogInfo("-->Entered GetCityCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->City name format is Incorrect");
                return StatusCode(400, "City name format is Incorrect");
            }
            string cityCode = await _businessLogic.GetCountryStateCityBusinessLogic().GetCityCodeForCityNameByAdvisor(cityName);
            _logger.LogInfo("-->City code retrieved Sucessfully");
            return StatusCode(202, cityCode);
        }

        /// <summary>
        ///     Get All Country Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetAllCountriesTask")]
        //[Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAllCountriesTask()
        {
            _logger.LogInfo("-->Entered GetAllCountriesTask action method");

            IEnumerable<CountryDTO> CountryList= await _businessLogic.GetCountryStateCityBusinessLogic().GetAllCountriesByAdvisor();
            _logger.LogInfo("--> All The Countries retrieved Sucessfully");
            return StatusCode(200, CountryList);
        }
        /// <summary>
        ///     Get All State Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpGet("GetAllStateTask")]
        //[Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAllStateTask([FromQuery][Required] string countryName)
        {
            _logger.LogInfo("-->Entered GetAllStateTask action method");

            IEnumerable<StateDTO> stateList = await _businessLogic.GetCountryStateCityBusinessLogic().GetAllStatesOfACountryByAdvisor(countryName);
            _logger.LogInfo("--> All The States retrieved Sucessfully");
            return StatusCode(200, stateList);
        }
        /// <summary>
         ///     Get All Cities Details
         /// </summary>
         /// <returns type="IActionResult"></returns>
        [HttpGet("GetAllCitiesTask")]
        //[Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetAllCitiesTask([FromQuery][Required] string countryName, [FromQuery][Required] string stateName)
        {
            _logger.LogInfo("-->Entered GetAllCitiesTask action method");

            IEnumerable<CityDTO> citiesList = await _businessLogic.GetCountryStateCityBusinessLogic().GetAllCitiesOfAStateByAdvisor(countryName,stateName);
            _logger.LogInfo("--> All The Cities retrieved Sucessfully");
            return StatusCode(200, citiesList);
        }

        /// <summary>
        ///     Edit Country Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpPut("EditCountryActionTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> EditCountryActionTask([FromBody] EditCountryDTO entity)
        {
            _logger.LogInfo("-->Entered EditCountryActionTask action method");
            
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Country Details are Incorrect");
                return StatusCode(400, false);
            }
            Guid id=await _businessLogic.GetCountryStateCityBusinessLogic().EditCountryByAdvisor(entity);
            _logger.LogInfo("-->Country is edited");
            return StatusCode(201, true);
        }
        [HttpGet("GetLocation")]
        //[Authorize(Roles = "Advisor")]
        public async Task<IActionResult> GetLocation()
        {
            _logger.LogInfo("-->Entered GetCountryCodeTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Country name format is Incorrect");
                return StatusCode(400, "Country name format is Incorrect");
            }
            IEnumerable<locationDTO> alllocationList = await _businessLogic.GetCountryStateCityBusinessLogic().GetLocationByAdvisor();
            _logger.LogInfo("-->Country code retrieved Sucessfully");
            return StatusCode(202, alllocationList);
        }

        /// <summary>
        ///     Edit State Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpPut("EditStateActionTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> EditStateActionTask([FromBody] EditStateDTO entity)
        {
            _logger.LogInfo("-->Entered EditStateActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->State Details are Incorrect");
                return StatusCode(400, false);
            }
            Guid id = await _businessLogic.GetCountryStateCityBusinessLogic().EditStateByAdvisor(entity);
            _logger.LogInfo("-->state is edited");
            return StatusCode(201, true);
        }

        /// <summary>
        ///     Edit City Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpPut("EditCityActionTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> EditCityActionTask([FromBody] EditCityDTO entity)
        {
            _logger.LogInfo("-->Entered EditCityActionTask action method");
            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->City Details are Incorrect");
                return StatusCode(400, false);
            }
            Guid id = await _businessLogic.GetCountryStateCityBusinessLogic().EditCityByAdvisor(entity);
            _logger.LogInfo("-->state is edited");
            return StatusCode(201, true);
        }

        /// <summary>
        ///     Delete Country Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpDelete("RemoveCountryActionTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> RemoveCountryActionTask([FromQuery][Required][StringLength(38, ErrorMessage = "Id length should be 38 Characters.")] Guid Id)
        {
            _logger.LogInfo("-->Entered RemoveCountryActionTask action method");

            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->Country id is incorrect");
                return StatusCode(400, "Country id is incorrect");
            }
            //if (emailId != GetClaim())
            //{
            //    return StatusCode(403, "You are unauthorised to access this resource");
            //}
            bool result = await _businessLogic.GetCountryStateCityBusinessLogic().DeleteCountryByAdvisor(Id);
            if (result)
            {
                _logger.LogInfo("-->Country is deleted Successfully");
                return StatusCode(204, "Country is deleted Successfully");
            }
            else
            {
                _logger.LogInfo("-->Country deletion failed");
                return StatusCode(200, "Country deletion failed");
            }
            
        }

        /// <summary>
        ///     Delete State Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpDelete("RemoveStateActionTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> RemoveStateActionTask([FromQuery][Required][StringLength(38,ErrorMessage ="Id length should be 38 Characters.")] Guid Id)
        {
            _logger.LogInfo("-->Entered RemoveStateActionTask action method");

            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->State id is incorrect");
                return StatusCode(400, "State id is incorrect");
            }
            //if (emailId != GetClaim())
            //{
            //    return StatusCode(403, "You are unauthorised to access this resource");
            //}
            bool result = await _businessLogic.GetCountryStateCityBusinessLogic().DeleteStateByAdvisor(Id);
            if (result)
            {
                _logger.LogInfo("-->State is deleted Successfully");
                return StatusCode(204, "State is deleted Successfully");
            }
            else
            {
                _logger.LogInfo("-->State deletion failed");
                return StatusCode(200, "State deletion failed");
            }

        }

        /// <summary>
        ///     Delete City Details
        /// </summary>
        /// <returns type="IActionResult"></returns>
        [HttpDelete("RemoveCityActionTask")]
        [Authorize(Roles = "Advisor")]
        public async Task<IActionResult> RemoveCityActionTask([FromQuery][Required][StringLength(38, ErrorMessage = "Id length should be 38 Characters.")] Guid Id)
        {
            _logger.LogInfo("-->Entered RemoveCityActionTask action method");

            if (!ModelState.IsValid)
            {
                _logger.LogInfo("-->City id is incorrect");
                return StatusCode(400, "City id is incorrect");
            }
            //if (emailId != GetClaim())
            //{
            //    return StatusCode(403, "You are unauthorised to access this resource");
            //}
            bool result = await _businessLogic.GetCountryStateCityBusinessLogic().DeleteCityByAdvisor(Id);
            if (result)
            {
                _logger.LogInfo("-->City is deleted Successfully");
                return StatusCode(204, "City is deleted Successfully");
            }
            else
            {
                _logger.LogInfo("-->City deletion failed");
                return StatusCode(200, "City deletion failed");
            }

        }
    }
}

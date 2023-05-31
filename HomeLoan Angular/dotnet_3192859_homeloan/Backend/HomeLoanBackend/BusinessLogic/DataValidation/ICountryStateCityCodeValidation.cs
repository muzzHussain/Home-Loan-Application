using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataValidation
{
    internal interface ICountryStateCityCodeValidation
    {
        public Task<bool> CheckCityCodeTask(string cityCode);
        public Task<bool> CheckStateCodeTask(string stateCode);
        public Task<bool> CheckCountryCodeTask(string countryCode);
    }
}

using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataValidation
{
    internal class CountryStateCityCodeValidation:ICountryStateCityCodeValidation
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public CountryStateCityCodeValidation(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
        public async Task<bool> CheckCityCodeTask(string cityCode)
        {
            City city = await _dataAccessLayer.Read().CityRead().GetByConditionTask((x) => x.Code == cityCode);
            return city != null;
        }
        public async Task<bool> CheckStateCodeTask(string stateCode)
        {
            State state = await _dataAccessLayer.Read().StateRead().GetByConditionTask((x) => x.Code == stateCode);
            return state != null;
        }
        public async Task<bool> CheckCountryCodeTask(string countryCode)
        {
            Country country = await _dataAccessLayer.Read().CountryRead().GetByConditionTask((x) => x.Code == countryCode);
            return country != null;
        }

    }
}

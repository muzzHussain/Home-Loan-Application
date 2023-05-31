using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    /// <summary>
    /// This interface defines the business logic methods for managing geographic entities such as countries, states, and cities.
    /// also specifies methods that add and retrieve data related to country, state, and city codes.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    public interface ICountryStateCityBusinessLogic<X,Y,Z,A,B,C,D>
    {
        public Task<IEnumerable<D>> GetLocationByAdvisor();
        //Country
        public Task<Guid> AddCountryCodeByAdvisor(X country);
        public Task<string> GetCountryCodeForCountryNameByAdvisor(string countryName);
        public Task<IEnumerable<X>> GetAllCountriesByAdvisor();
        public Task<Guid> EditCountryByAdvisor(A entity);
        public Task<bool> DeleteCountryByAdvisor(Guid id);
        //State
        public Task<Guid> AddStateCodeByAdvisor(Y state);
        public Task<string> GetStateCodeForStateNameByAdvisor(string stateName);
        public Task<IEnumerable<Y>> GetAllStatesOfACountryByAdvisor(string countryName);
        public Task<Guid> EditStateByAdvisor(B entity);
        public Task<bool> DeleteStateByAdvisor(Guid id);

        //City
        public Task<Guid> AddCityCodeByAdvisor(Z city);
        public Task<string> GetCityCodeForCityNameByAdvisor(string cityName);
        public Task<IEnumerable<Z>> GetAllCitiesOfAStateByAdvisor(string countryName, string stateName);
        public Task<Guid> EditCityByAdvisor(C entity);
        public Task<bool> DeleteCityByAdvisor(Guid id);
    }
}

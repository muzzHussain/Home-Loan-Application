using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;

namespace BusinessLogic
{
    /// <summary>
    /// interface of BusinessLogic that return different business logic objects.
    /// </summary>
    public interface IBusinessLogic
    {
        public IUserBusinessLogic<RegisterUserDTO, UpdatePasswordDTO> GetUserBusinessLogic();
        public ILoanBusinessLogic<ApplyLoanDTO, AdvisorLoanApplicationDTO, UserLoanApplicationDTO, EditLoanApplicationDTO> GetLoanBusinessLogic();
        public IAdvisorBusinessLogic<AdvisorRegisterDTO, UpdatePasswordDTO> GetAdvisorBusinessLogic();
        public ICollateralBusinessLogic<ApplyCollateralDTO, EditCollateralDTO,DeleteCollateralDTO> GetCollateralBusinessLogic();
        public ICountryStateCityBusinessLogic<CountryDTO, StateDTO, CityDTO,EditCountryDTO,EditStateDTO,EditCityDTO,locationDTO> GetCountryStateCityBusinessLogic();
        public IPromotionBusinessLogic<PromotionsDTO, EditPromotionDTO> GetPromotionsBusinessLogic();

    }
}

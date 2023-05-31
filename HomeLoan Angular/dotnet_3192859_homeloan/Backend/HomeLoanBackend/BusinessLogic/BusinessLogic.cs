using AutoMapper;
using BusinessLogic.Business.BusinessImplementation;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer;
using BusinessLogic.DataValidation;

namespace BusinessLogic
{
    public class BusinessLogic:IBusinessLogic
    {

        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        private readonly ICountryStateCityCodeValidation _countryStateCityCodeValidation;

        public IUserBusinessLogic<RegisterUserDTO, UpdatePasswordDTO> _userBusinessLogic;
        public ILoanBusinessLogic<ApplyLoanDTO, AdvisorLoanApplicationDTO, UserLoanApplicationDTO, EditLoanApplicationDTO> _loanBusinessLogic;
        public IAdvisorBusinessLogic<AdvisorRegisterDTO, UpdatePasswordDTO> _advisorBusinessLogic;
        public ICollateralBusinessLogic<ApplyCollateralDTO, EditCollateralDTO,DeleteCollateralDTO> _collateralBusinessLogic;
        public ICountryStateCityBusinessLogic<CountryDTO, StateDTO, CityDTO,EditCountryDTO,EditStateDTO,EditCityDTO,locationDTO> _countryStateCityBusinessLogic;
        public IPromotionBusinessLogic<PromotionsDTO, EditPromotionDTO> _promotionBusinessLogic;

        /// <summary>
        /// Constructor for the BusinessLogic class and it takes an IMapper object as a parameter, initializes the _mapper and _dataAccessLayer fields, 
        /// and then creates instances of the various business logic classes, passing in the _mapper and _dataAccessLayer objects as dependencies.
        /// </summary>
        /// <param name="mapper"></param>
        public BusinessLogic(IMapper mapper)
        {
            _mapper = mapper;
            _dataAccessLayer = new DataAccessLayer.DataAccessLayer();
            _countryStateCityCodeValidation = new CountryStateCityCodeValidation(_dataAccessLayer);

            _userBusinessLogic = new UserBusinessLogic(_mapper, _dataAccessLayer, _countryStateCityCodeValidation);
            _loanBusinessLogic = new LoanBusinessLogic(_mapper, _dataAccessLayer);
            _advisorBusinessLogic = new AdvisorBusinessLogic(_mapper, _dataAccessLayer);
            _collateralBusinessLogic = new CollateralBusinessLogic( _mapper, _dataAccessLayer);
            _promotionBusinessLogic = new PromotionBusinessLogic(_mapper, _dataAccessLayer);
            _countryStateCityBusinessLogic = new CountryStateCityBusinessLogic(_mapper, _dataAccessLayer); 
        }

        /// <summary>
        /// The methods return the respective business logic objects.
        /// </summary>
        public IUserBusinessLogic<RegisterUserDTO, UpdatePasswordDTO> GetUserBusinessLogic()
        {
            return _userBusinessLogic;
            
        }
        public ILoanBusinessLogic<ApplyLoanDTO, AdvisorLoanApplicationDTO, UserLoanApplicationDTO, EditLoanApplicationDTO> GetLoanBusinessLogic()
        {
            return _loanBusinessLogic;
        }
        public IAdvisorBusinessLogic<AdvisorRegisterDTO, UpdatePasswordDTO> GetAdvisorBusinessLogic()
        {
            return _advisorBusinessLogic;
        }
        public ICollateralBusinessLogic<ApplyCollateralDTO, EditCollateralDTO, DeleteCollateralDTO> GetCollateralBusinessLogic()
        {
            return _collateralBusinessLogic;
        }
        public IPromotionBusinessLogic<PromotionsDTO, EditPromotionDTO> GetPromotionsBusinessLogic()
        {
            return _promotionBusinessLogic;
        }
        public ICountryStateCityBusinessLogic<CountryDTO, StateDTO, CityDTO,EditCountryDTO,EditStateDTO,EditCityDTO,locationDTO> GetCountryStateCityBusinessLogic()
        {
            return _countryStateCityBusinessLogic;
        }

    }
}

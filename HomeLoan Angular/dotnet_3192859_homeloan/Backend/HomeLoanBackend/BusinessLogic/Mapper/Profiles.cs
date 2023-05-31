using AutoMapper;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
namespace BusinessLogic.Mapper
{
    /// <summary>
    /// Mapping profile for AutoMapper to map between DTOs and entity models.
    /// </summary>
    public class Profiles:Profile
    { 
        public Profiles()
        {
            // Mapping between ApplyLoanDTO, Property, PersonalIncome, and LoanRequirements
            CreateMap<ApplyLoanDTO, Property>().ReverseMap();
            CreateMap<ApplyLoanDTO, PersonalIncome>().ReverseMap();
            CreateMap<ApplyLoanDTO, LoanRequirements>().ReverseMap();

            // Mapping between User, RegisterUserDTO, LoginDTO, UpdatePasswordDTO, Advisor, and ApplyCollateralDTO
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, AdvisorRegisterDTO>().ReverseMap();
            CreateMap<User, UpdatePasswordDTO>().ReverseMap();
            CreateMap<Advisor, AdvisorRegisterDTO>().ReverseMap();
            CreateMap<ApplyLoanDTO, User>().ReverseMap();
            CreateMap<User, ApplyCollateralDTO>().ReverseMap();
            CreateMap<Collateral, DeleteCollateralDTO>().ReverseMap();
            // Mapping between Collateral, EditCollateralDTO
            CreateMap<Collateral, ApplyCollateralDTO>().ReverseMap();
            CreateMap<User, EditCollateralDTO>().ReverseMap();
            CreateMap<Collateral, EditCollateralDTO>().ReverseMap();

            // Mapping between Promotions and PromotionsDTO
            CreateMap<Promotions, PromotionsDTO>().ReverseMap();
            CreateMap<Advisor, EditPromotionDTO>().ReverseMap();
            CreateMap<Promotions, EditPromotionDTO>().ReverseMap();

            // Mapping between CountryDTO, StateDTO, CityDTO, and their respective entity models
            CreateMap<CountryDTO, Country>().ReverseMap();
            CreateMap<StateDTO, State>().ReverseMap();
            CreateMap<CityDTO, City>().ReverseMap();    
        }
    }
}

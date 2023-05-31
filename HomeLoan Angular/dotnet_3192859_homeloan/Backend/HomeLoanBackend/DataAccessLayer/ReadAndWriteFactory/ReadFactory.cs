using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryImplementation;
using DataAccessLayer.Repository.RepositoryInterface;

namespace DataAccessLayer.ReadAndWriteFactory
{
    internal class ReadFactory:IReadFactory
    {
        private readonly IReadOperation<Advisor> _advisorReadOperation;
        private readonly IReadOperation<User> _userReadOperation;
        private readonly IReadOperation<Collateral> _collateralReadOperation;
        private readonly IReadOperation<CollateralAndLoanApplication> _collateralAndLoanApplicationReadOperation;
        private readonly IReadOperation<LoanApplication> _loanApplicationReadOperation;
        private readonly IReadOperation<LoanRequirements> _loanRequirementsReadOperation;
        private readonly IReadOperation<PersonalIncome> _personalIncomeReadOperation;
        private readonly IReadOperation<Property> _propertyReadOperation;
        private readonly IReadOperation<Promotions> _promotionsReadOperation;
        private readonly IReadOperation<City> _cityReadOperation;
        private readonly IReadOperation<Country> _countryReadOperation;
        private readonly IReadOperation<State> _stateReadOperation;

        public ReadFactory(AppDbContext appDbContext)
        {
            _advisorReadOperation = new AdvisorRepo(appDbContext);
            _userReadOperation= new UserRepo(appDbContext);
            _collateralReadOperation= new CollateralRepo(appDbContext);
            _collateralAndLoanApplicationReadOperation = new CollateralAndLoanApplicationRepo(appDbContext);    
            _loanApplicationReadOperation= new LoanApplicationRepo(appDbContext);
            _loanRequirementsReadOperation= new LoanRequirementsRepo(appDbContext);
            _personalIncomeReadOperation = new PersonalIncomeRepo(appDbContext);
            _propertyReadOperation= new PropertyRepo(appDbContext);
            _promotionsReadOperation = new PromotionsRepo(appDbContext);
            _cityReadOperation = new CityRepo(appDbContext);
            _countryReadOperation = new CountryRepo(appDbContext);
            _stateReadOperation = new StateRepo(appDbContext);
        }

        public IReadOperation<Advisor> AdvisorRead()
        {
            return _advisorReadOperation;
        }

        public IReadOperation<CollateralAndLoanApplication> CollateralAndLoanApplicationRead()
        {
            return _collateralAndLoanApplicationReadOperation;
        }

        public IReadOperation<Collateral> CollateralRead()
        {
            return _collateralReadOperation;
        }

        public IReadOperation<LoanApplication> LoanApplicationRead()
        {
            return _loanApplicationReadOperation;
        }

        public IReadOperation<LoanRequirements> LoanRequirementsRead()
        {
            return _loanRequirementsReadOperation;
        }

        public IReadOperation<PersonalIncome> PersonalIncomeRead()
        {
            return _personalIncomeReadOperation;
        }

        public IReadOperation<Promotions> PromotionsRead()
        {
            return _promotionsReadOperation;
        }

        public IReadOperation<Property> PropertyRead()
        {
            return _propertyReadOperation;
        }

        public IReadOperation<User> UserRead()
        {
            return _userReadOperation;
        }
        public IReadOperation<Country> CountryRead()
        {
            return _countryReadOperation;
        }
        public IReadOperation<State> StateRead()
        {
            return _stateReadOperation;
        }
        public IReadOperation<City> CityRead()
        {
            return _cityReadOperation;
        }
    }
}

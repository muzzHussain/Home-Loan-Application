using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryImplementation;
using DataAccessLayer.Repository.RepositoryInterface;

namespace DataAccessLayer.ReadAndWriteFactory
{
    internal class WriteFactory:IWriteFactory
    {
        private readonly IWriteOperation<Advisor> _advisorWriteOperation;
        private readonly IWriteOperation<User> _userWriteOperation;
        private readonly IWriteOperation<Collateral> _collateralWriteOperation;
        private readonly IWriteOperation<CollateralAndLoanApplication> _collateralAndLoanApplicationWriteOperation;
        private readonly IWriteOperation<LoanApplication> _loanApplicationWriteOperation;
        private readonly IWriteOperation<LoanRequirements> _loanRequirementsWriteOperation;
        private readonly IWriteOperation<PersonalIncome> _personalIncomeWriteOperation;
        private readonly IWriteOperation<Property> _propertyWriteOperation;
        private readonly IWriteOperation<Promotions> _promotionsWriteOperation;
        private readonly ICommitOperation _commitOperation;
        private readonly IWriteOperation<City> _cityWriteOperation;
        private readonly IWriteOperation<Country> _countryWriteOperation;
        private readonly IWriteOperation<State> _stateWriteOperation;


        public WriteFactory(AppDbContext appDbContext)
        {
            _advisorWriteOperation = new AdvisorRepo(appDbContext);
            _userWriteOperation = new UserRepo(appDbContext);
            _collateralWriteOperation = new CollateralRepo(appDbContext);
            _collateralAndLoanApplicationWriteOperation = new CollateralAndLoanApplicationRepo(appDbContext);
            _loanApplicationWriteOperation = new LoanApplicationRepo(appDbContext);
            _loanRequirementsWriteOperation = new LoanRequirementsRepo(appDbContext);
            _personalIncomeWriteOperation = new PersonalIncomeRepo(appDbContext);
            _propertyWriteOperation = new PropertyRepo(appDbContext);
            _promotionsWriteOperation = new PromotionsRepo(appDbContext);
            _commitOperation = new CommitRepo(appDbContext);
            _cityWriteOperation = new CityRepo(appDbContext);
            _countryWriteOperation = new CountryRepo(appDbContext);
            _stateWriteOperation = new StateRepo(appDbContext);
        }

        public IWriteOperation<Advisor> AdvisorWrite()
        {
            return _advisorWriteOperation;
        }

        public IWriteOperation<CollateralAndLoanApplication> CollateralAndLoanApplicationWrite()
        {
            return _collateralAndLoanApplicationWriteOperation;
        }

        public IWriteOperation<Collateral> CollateralWrite()
        {
            return _collateralWriteOperation;
        }

        public ICommitOperation CommitWrite()
        {
            return _commitOperation;
        }

        public IWriteOperation<LoanApplication> LoanApplicationWrite()
        {
            return _loanApplicationWriteOperation;

        }

        public IWriteOperation<LoanRequirements> LoanRequirementsWrite()
        {
            return _loanRequirementsWriteOperation;
        }

        public IWriteOperation<PersonalIncome> PersonalIncomeWrite()
        {
            return _personalIncomeWriteOperation;
        }

        public IWriteOperation<Promotions> PromotionsWrite()
        {
            return _promotionsWriteOperation;
        }

        public IWriteOperation<Property> PropertyWrite()
        {
            return _propertyWriteOperation;
        }

        public IWriteOperation<User> UserWrite()
        {
            return _userWriteOperation;
        }

        public IWriteOperation<Country> CountryWrite()
        {
            return _countryWriteOperation;
        }
        public IWriteOperation<State> StateWrite()
        {
            return _stateWriteOperation;
        }
        public IWriteOperation<City> CityWrite()
        {
            return _cityWriteOperation;
        }
    }
}

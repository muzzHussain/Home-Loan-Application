using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;

namespace DataAccessLayer.ReadAndWriteFactory
{
    public interface IWriteFactory
    {
        public IWriteOperation<Advisor> AdvisorWrite();
        public IWriteOperation<User> UserWrite();
        public IWriteOperation<Collateral> CollateralWrite();
        public IWriteOperation<CollateralAndLoanApplication> CollateralAndLoanApplicationWrite();
        public IWriteOperation<LoanApplication> LoanApplicationWrite();
        public IWriteOperation<LoanRequirements> LoanRequirementsWrite();
        public IWriteOperation<PersonalIncome> PersonalIncomeWrite();
        public IWriteOperation<Property> PropertyWrite();
        public IWriteOperation<Promotions> PromotionsWrite();
        public ICommitOperation CommitWrite();
        public IWriteOperation<Country> CountryWrite();
        public IWriteOperation<State> StateWrite();
        public IWriteOperation<City> CityWrite();
    }
}

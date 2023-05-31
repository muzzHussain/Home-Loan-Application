using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;

namespace DataAccessLayer.ReadAndWriteFactory
{
    public interface IReadFactory
    {
        public IReadOperation<Advisor> AdvisorRead();
        public IReadOperation<User> UserRead();
        public IReadOperation<Collateral> CollateralRead();
        public IReadOperation<CollateralAndLoanApplication> CollateralAndLoanApplicationRead();
        public IReadOperation<LoanApplication> LoanApplicationRead();
        public IReadOperation<LoanRequirements> LoanRequirementsRead();
        public IReadOperation<PersonalIncome> PersonalIncomeRead();
        public IReadOperation<Property> PropertyRead();
        public IReadOperation<Promotions> PromotionsRead();
        public IReadOperation<Country> CountryRead();
        public IReadOperation<City> CityRead();
        public IReadOperation<State> StateRead();

    }
}

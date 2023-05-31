using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DataAccessLayer.Data
{
    public class AppDbContext:DbContext
    {
        private string _databaseConnectionString;
        public AppDbContext()
        {
            _databaseConnectionString  = ConfigurationManager.AppSettings["DatabaseConnectionString"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=HomeLoan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Advisor> Advisors { get; set; }
        public DbSet<LoanApplication> LoanApplication { get; set; }
        public DbSet<LoanRequirements> LoanRequirements { get; set; }
        public DbSet<PersonalIncome> PersonalIncomes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Promotions> Promotions { get; set;}
        public DbSet<Collateral> Collaterals { get; set; }
        public DbSet<CollateralAndLoanApplication> CollateralsAndLoanApplications { get;set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
    }
}


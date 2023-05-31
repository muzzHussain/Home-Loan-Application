using System;
using System.Configuration;

namespace BusinessLogic.Algorithms
{
    /// <summary>
    /// Algorithm for calculating the value of a collateral and determining its eligibility for a loan. 
    /// </summary>
    internal class CollateralCalculator
    {
        //collateral values
        private  string _type;
        private  decimal _value;
        private  decimal _share;
        //Bank Evaluations;
        private readonly decimal _gold;
        private readonly decimal _insurancePolicy;
        private readonly decimal _stocks;
        private readonly decimal _property;

        #region CollateralCalculator
        public CollateralCalculator(string type, decimal value, decimal share)
        {
            _type = type;
            _value = value;
            _share = share;
            _gold = Decimal.Parse(ConfigurationManager.AppSettings["GoldBankEvaluation"]);
            _insurancePolicy = Decimal.Parse(ConfigurationManager.AppSettings["InsurancePolicyBankEvaluation"]);
            _property = Decimal.Parse(ConfigurationManager.AppSettings["PropertyBankEvaluation"]);
            _stocks = Decimal.Parse(ConfigurationManager.AppSettings["StockBankEvaluation"]);

        }
        /// <summary>
        /// this method calculates the value of the collateral based on its type and share using the bank evaluations.
        /// </summary>
        public decimal calculateValue()
        {
            _value = (_value * _share) / 100;
            if(_type == "Gold")
            {
                _value = (_value * _gold)/ 100;
            }
            else if(_type == "InsurancePolicy")
            {
                _value = (_value * _insurancePolicy) / 100;
            }
            else if (_type == "Stocks")
            {
                _value = (_value * _stocks) / 100;
            }
            else
            {
                _value = (_value * _property) / 100;
            }
            return _value;
        }
        /// <summary>
        /// This method calculates the eligibility of the collateral for a loan based on its value and the loan amount.
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <returns>It returns an integer value that represents the eligibility status.</returns>
        public int CalculateEligibility(decimal loanAmount)
        {
            decimal collateralValue = calculateValue();
            if(collateralValue <= (loanAmount*0.4M))
            {
                return 0; // Red status
            }
            else if(collateralValue > loanAmount*0.4M && collateralValue <= loanAmount * 0.7M)
            {
                return 1; //Yellow status
            }
            else
            {
                return 2; //Green status
            }
        }
        #endregion
    }
}

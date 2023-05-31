using BusinessLogic.DTO;
using System.ComponentModel.DataAnnotations;

namespace Utility.DataAttributes
{
    internal class CustomCollateralTypeAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"This type of collateral is not acceptable. Collateral type should be 'Gold', 'Property', InsurancePolicy' or 'Stock'";
        /// <summary>
        /// Custom validation attribute class to ensure that the type of collateral being applied for is one of the allowed types: Gold, InsurancePolicy, Property, or Stock.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Return success if the validation passes</returns>

        #region IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ApplyCollateralDTO localType = (ApplyCollateralDTO)validationContext.ObjectInstance;
            if (!(localType.Type == "Gold" || localType.Type == "InsurancePolicy" || localType.Type == "Property" || localType.Type == "Stock"))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
        #endregion
    }
}

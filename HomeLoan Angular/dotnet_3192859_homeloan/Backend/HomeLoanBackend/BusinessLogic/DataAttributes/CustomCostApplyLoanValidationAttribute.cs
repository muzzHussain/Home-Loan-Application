using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DataAttributes
{
    internal class CustomCostApplyLoanValidationAttribute: ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Loan cost should be in the multiples of 1000 and it should be within the range of 100000 to 900000000";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ApplyLoanDTO loan = (ApplyLoanDTO)validationContext.ObjectInstance;
            if ( loan.Cost%1000 != 0 || loan.Cost < 100000M || loan.Cost > 900000000M )
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}

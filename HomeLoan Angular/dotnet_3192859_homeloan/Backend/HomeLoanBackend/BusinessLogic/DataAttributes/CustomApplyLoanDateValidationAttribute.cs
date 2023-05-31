using BusinessLogic.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DataAttributes
{
    /// <summary>
    /// This is custom validation attribute for ApplyLoanDTO class which checks if the LoanStartDate property is set to the current date.
    /// </summary>
    public class CustomApplyLoanDateValidationAttribute:ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Date should be current date only";

        #region IsValid
        /// <summary>
        /// Implementation of the IsValid method for data validation. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Return success if the validation passes</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ApplyLoanDTO loanDTO = (ApplyLoanDTO)validationContext.ObjectInstance;
            // Check if the LoanStartDate property is set to the current date.
            if (loanDTO.LoanStartDate.Date != DateTime.Now.Date)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
        #endregion
    }
}

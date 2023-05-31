using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DataAttributes
{
    internal class CustomEditLoanDateValidationAttribute:ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Date should be current date only";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            EditLoanApplicationDTO loanDTO = (EditLoanApplicationDTO)validationContext.ObjectInstance;
            if (loanDTO.LoanStartDate.Date != DateTime.Now.Date)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}

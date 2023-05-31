using BusinessLogic.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DataAttributes
{
    /// <summary>
    /// Custom validation attribute class to ensure that the start and end dates of a promotion are within the range of current date and current date + specified number of years.
    /// </summary>
    public class CustomPromotionDateValidationAttribute:ValidationAttribute
    {
        public int _years;
        public CustomPromotionDateValidationAttribute(int years)
        {
            _years = years;
        }

        public string GetErrorMessage() =>
            $"Date should be within the range of current date - current date + 5 years";

        #region IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PromotionsDTO promotionsDTO = (PromotionsDTO)validationContext.ObjectInstance;
            if (!(promotionsDTO.StartDate.Date >= DateTime.Now.Date && promotionsDTO.StartDate.Date<=DateTime.Now.AddYears(_years).Date))
            {
                return new ValidationResult(GetErrorMessage());
            }
            if (!(promotionsDTO.EndDate.Date >= DateTime.Now.Date && promotionsDTO.EndDate.Date <= DateTime.Now.AddYears(_years).Date))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
        #endregion
    }
}

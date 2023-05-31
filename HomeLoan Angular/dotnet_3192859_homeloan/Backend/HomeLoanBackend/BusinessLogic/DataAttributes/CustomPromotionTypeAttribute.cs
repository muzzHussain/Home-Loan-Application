using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DataAttributes
{
    internal class CustomPromotionTypeAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"This type of promotion is not acceptable. Promotion type should be 'A', 'B', 'C' or 'D'";
        /// <summary>
        /// Custom validation attribute class to ensure that the type of Promotion is one of the allowed types: A, B, C, or D.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>

        #region IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PromotionsDTO localType = (PromotionsDTO)validationContext.ObjectInstance;
            if (!(localType.Type == "A" || localType.Type == "B" || localType.Type == "C" || localType.Type == "D"))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
        #endregion
    }
}

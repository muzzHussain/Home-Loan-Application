using BusinessLogic.DTO;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DataAttributes
{
    internal class CustomEditPromotionTypeAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"This type of promotion is not acceptable. Promotion type should be 'A', 'B', 'C' or 'D'";
        /// <summary>
        /// Custom validation attribute class to ensure that Edited type of promotion is one of the allowed types: A, B, C, or D.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Return success if the validation passes</returns>

        #region IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            EditPromotionDTO localType = (EditPromotionDTO)validationContext.ObjectInstance;
            if (!(localType.Type == "A" || localType.Type == "B" || localType.Type == "C" || localType.Type == "D"))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
        #endregion
    }
}




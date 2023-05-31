using BusinessLogic.DataAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class PromotionsDTO
    {
        [Required(ErrorMessage = "Promotion start date is required")]
        [CustomPromotionDateValidation(5,ErrorMessage = "Promotion start date should be in the range from current date - current date + 5 years")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Promotion end date is required")]
        [CustomPromotionDateValidation(5,ErrorMessage = "Promotion end date should be in the range from current date - current date + 5 years")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Promotions type is required")]
        [CustomPromotionType(ErrorMessage = "Promotion type should be 'A', 'B', 'C', 'D'")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Promotions message is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Promtion message max length is 100 characters and min length is 5 character")]
        public string Message { get; set; }
       
    }
}

using BusinessLogic.DataAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class EditPromotionDTO
    {
        [Required(ErrorMessage = "Promotion Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Promotion start date is required")]
        [CustomEditPromotionDateValidation(5, ErrorMessage = "Promotion start date should be in the range from current date - current date + 5 years")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "Promotion end date is required")]
        [CustomEditPromotionDateValidation(5, ErrorMessage = "Promotion end date should be in the range from current date - current date + 5 years")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Promotions type is required")]
        [CustomEditPromotionType(ErrorMessage = "Promotion type should be 'A', 'B', 'C', 'D'")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Promotions message is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Promtion message max length is 100 characters and min length is 5 character")]
        public string Message { get; set; }
    }
}

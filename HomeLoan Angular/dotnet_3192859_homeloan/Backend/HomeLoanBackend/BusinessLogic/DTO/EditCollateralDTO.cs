using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class EditCollateralDTO
    {
        [Required(ErrorMessage = "Collateral Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Collateral value is required")]
        [Range(typeof(decimal), "1000", "990000000", ErrorMessage = "Collateral value should be in the range from 1000 - 990000000")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "User collateral share is required")]
        [Range(typeof(decimal), "1", "100", ErrorMessage = "User collateral share should be in the range from 1 - 100")]
        public decimal Share { get; set; }
    }
}

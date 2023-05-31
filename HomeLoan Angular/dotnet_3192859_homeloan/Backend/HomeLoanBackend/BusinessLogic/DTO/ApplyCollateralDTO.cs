using System;
using System.ComponentModel.DataAnnotations;
using Utility.DataAttributes;

namespace BusinessLogic.DTO
{
    public class ApplyCollateralDTO
    {
        [Required(ErrorMessage = "Collateral Type is required")]
        [CustomCollateralType(ErrorMessage = "Collateral Type should be 'Gold', 'Property', InsurancePolicy' or 'Stock'")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Collateral value is required")]
        [Range(typeof(decimal), "1000", "990000000", ErrorMessage = "Collateral value should be in range 1000 - 990000000")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "Owner share is required")]
        [Range(typeof(decimal), "1", "100", ErrorMessage = "Owner should be in range 1 - 100")]
        public decimal Share { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utility.Enums;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will help to map collateral data in DB
    /// </summary>
    public class Collateral
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public CollateralType Type { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public decimal Share { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map Property to DB
    /// </summary>
    public class Property
    {
        [Required]
        [Key]
        public Guid Id {get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal Size { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public decimal RegistrationCost { get; set; }

    }
}

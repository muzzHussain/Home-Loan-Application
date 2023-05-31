using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map User details to DB
    /// </summary>
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string CityCode { get; set; }
        [Required]
        public string StateCode { get; set; }
        [Required]
        public string CountryCode { get; set; }

    }
}

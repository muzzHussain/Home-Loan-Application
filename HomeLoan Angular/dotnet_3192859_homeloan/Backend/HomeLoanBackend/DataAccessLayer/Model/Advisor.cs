using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// Advisor model this model will map advisor entity to DB
    /// </summary>
    public class Advisor
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

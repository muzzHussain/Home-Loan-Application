using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map static data od country to DB
    /// </summary>
    public class Country
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

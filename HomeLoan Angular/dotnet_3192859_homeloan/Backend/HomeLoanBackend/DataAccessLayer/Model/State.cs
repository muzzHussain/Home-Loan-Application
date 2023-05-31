using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map static data od city to DB
    /// </summary>
    public class State
    {

        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Country")]
        public Guid CountryId { get; set; }
    }
}

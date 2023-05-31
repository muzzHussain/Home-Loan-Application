using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map Personal Income to DB
    /// </summary>
    public class PersonalIncome
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal MonthlyFamilyIncome { get; set; }
        [Required]
        public decimal OtherIncome { get; set; }
    }
}

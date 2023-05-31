using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map Loan Requirement to DB
    /// </summary>
    public class LoanRequirements
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal LoanAmount { get; set; }
        [Required]
        public int LoanDuration { get; set; }
        [Required]
        public DateTime LoanStartDate { get; set; }

    }
}

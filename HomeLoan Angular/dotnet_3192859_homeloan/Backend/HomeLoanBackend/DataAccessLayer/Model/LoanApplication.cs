using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utility.Enums;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will link LoanRequirement, PersonalIncome and Property and map LoanApplication to DB
    /// </summary>
    public class LoanApplication
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("Property")]
        [Required]
        public Guid PropertyId { get; set; }

        [ForeignKey("PersonalIncome")]
        [Required]
        public Guid PersonalIncomeId { get; set; }

        [ForeignKey("LoanRequirements")]
        [Required]
        public Guid LoanRequirementsId { get; set; }
        [Required]
        public LoanApplicationStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}

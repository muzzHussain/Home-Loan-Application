using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// CollateralAndLoanApplication this model will link collateral and loan application and map to DB
    /// </summary>
    public class CollateralAndLoanApplication
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("LoanApplication")]
        public Guid LoanApplicationId { get; set; }
        [Required]
        [ForeignKey("Collateral")]
        public Guid CollateralId { get; set; }
    }
}

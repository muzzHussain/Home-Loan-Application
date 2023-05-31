using System;
using System.ComponentModel.DataAnnotations;
using static Utility.Enums;

namespace DataAccessLayer.Model
{
    /// <summary>
    /// This model will map Promotions to DB
    /// </summary>
    public class Promotions
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public PromotionType Type { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public PromotionState Status { get; set; }
    }
}

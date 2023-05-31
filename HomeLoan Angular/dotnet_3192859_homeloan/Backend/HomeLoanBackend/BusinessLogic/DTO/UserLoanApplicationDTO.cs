using BusinessLogic.DataAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class UserLoanApplicationDTO
    {
        [Required(ErrorMessage = "Loan application id is required")]
        public Guid Id { get; set; }
        //User
        [Required(ErrorMessage = "User email id is required")]
        [EmailAddress]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "User email id format is not correct")]
        public string EmailId { get; set; }

        //Property
        [Required(ErrorMessage = "User property address is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "User address max length is 100 characters and min length is 5 character")]
        public string Address { get; set; }
        [Required(ErrorMessage = "User property size is required")]
        [Range(typeof(decimal), "25", "1000", ErrorMessage = " User property size should be in range from 25 - 1000")]
        public decimal Size { get; set; }
        [Required(ErrorMessage = "User property cost is required")]
        [Range(typeof(decimal), "1000000", "990000000", ErrorMessage = "User property cost should be in the range from 1000000 - 990000000")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "User property registeration cost is required")]
        [Range(typeof(decimal), "100000", "10000000", ErrorMessage = "User property registeration cost should be in range from 100000 - 10000000")]
        public decimal RegistrationCost { get; set; }

        //PersonalIncome
        [Required(ErrorMessage = "User monthly family income is required")]
        [Range(typeof(decimal), "1000", "10000000", ErrorMessage = "User monthly family income should be in range from 1000 - 10000000")]
        public decimal MonthlyFamilyIncome { get; set; }
        [Required(ErrorMessage = "User other income is required")]
        [Range(typeof(decimal), "1000", "10000000", ErrorMessage = "User other income should be in range from 1000 - 10000000")]
        public decimal OtherIncome { get; set; }

        //LoanRequirement
        [Required(ErrorMessage = "Loan amount is required")]
        [Range(typeof(decimal), "10000", "990000000", ErrorMessage = "Loan amount should be in the range from 10000 - 990000000")]
        public decimal LoanAmount { get; set; }
        [Required(ErrorMessage = "Loan duration is required")]
        [Range(typeof(int), "1", "20", ErrorMessage = "Loan duration should be in the range from 1 - 20")]
        public int LoanDuration { get; set; }
        [Required(ErrorMessage = "Loan start date is required")]
        //[CustomDateRange(0, ErrorMessage = "Loan start date should be the current date")]
        [CustomApplyLoanDateValidation(ErrorMessage = "Loan start date should be the current date")]
        public DateTime LoanStartDate { get; set; }
        [Required]
        public string Status { get; set; }

        public string? Notes { get; set; }

    }
}

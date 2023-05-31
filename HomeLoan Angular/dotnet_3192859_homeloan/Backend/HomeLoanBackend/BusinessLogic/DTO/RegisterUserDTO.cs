using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
   public  class RegisterUserDTO
    {
        [Required(ErrorMessage = "User email id is required")]
        [EmailAddress]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "User email id format is not correct")]
        public string EmailId { get; set; }
        //^: first line
        //(?=.*[a-z]) : Should have at least one lower case
        //(?=.*[A-Z]) : Should have at least one upper case
        //(?=.*\d) : Should have at least one number
        //(?=.*[#$^+=!*()@%&] ) : Should have at least one special character
        //.{ 8,} : Minimum 8 characters
        //$ : end line
        [Required(ErrorMessage = "User password is required")]
        [PasswordPropertyText(true)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "User password format is not correct")]
        public string Password { get; set; }
        //phone number, for example, 123-456-7890, (123) 456-7890, 123 456 7890, 123.456.7890.
        [Required(ErrorMessage ="User phone number is required")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage ="User phone number format is not correct")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "City code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "City code max length is 3 characters and min length is 3 character")]
        public string CityCode { get; set; }
        [Required(ErrorMessage = "State code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "State code max length is 3 characters and min length is 3 character")]
        public string StateCode { get; set; }
        [Required(ErrorMessage = "Country code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Country code max length is 3 characters and min length is 3 character")]
        public string CountryCode { get; set; }
    }
}

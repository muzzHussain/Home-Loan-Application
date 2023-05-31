using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class AdvisorRegisterDTO
    {
        [Required(ErrorMessage = "User email id is required")]
        [EmailAddress]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "User email id format is not correct")]
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
    }
}

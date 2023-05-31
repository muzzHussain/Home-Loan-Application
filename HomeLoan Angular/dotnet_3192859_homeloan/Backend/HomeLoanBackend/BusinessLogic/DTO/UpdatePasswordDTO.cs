using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class UpdatePasswordDTO
    {
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
        //^: first line
        //(?=.*[a-z]) : Should have at least one lower case
        //(?=.*[A-Z]) : Should have at least one upper case
        //(?=.*\d) : Should have at least one number
        //(?=.*[#$^+=!*()@%&] ) : Should have at least one special character
        //.{ 8,} : Minimum 8 characters
        //$ : end line
        [Required(ErrorMessage = "User new password is required")]
        [PasswordPropertyText(true)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "User new password format is not correct")]
        public string NewPassword { get; set; }
    }
}

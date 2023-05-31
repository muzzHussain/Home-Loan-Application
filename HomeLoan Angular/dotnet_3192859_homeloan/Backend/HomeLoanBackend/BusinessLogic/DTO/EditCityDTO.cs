using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class EditCityDTO
    {
        [Required(ErrorMessage = "City Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "City code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "City code max length is 3 characters and min length is 2 character")]
        public string Code { get; set; }
        [Required(ErrorMessage = "City name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "City name max length is 20 characters and min length is 2 character")]
        public string Name { get; set; }
        [Required(ErrorMessage = "State code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "State code max length is 3 characters and min length is 2 character")]
        public string StateCode { get; set; }
    }
}

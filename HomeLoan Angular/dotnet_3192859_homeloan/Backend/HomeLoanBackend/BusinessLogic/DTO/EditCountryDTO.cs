using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class EditCountryDTO
    {
        [Required(ErrorMessage = "Country Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Country code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Country code max length is 3 characters and min length is 2 character")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Country name max length is 20 characters and min length is 2 character")]
        public string Name { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class StateDTO
    {
        [Required(ErrorMessage = "State code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "State code max length is 3 characters and min length is 2 character")]
        public string Code { get; set; }
        [Required(ErrorMessage = "State name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "State name max length is 20 characters and min length is 2 character")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Country code max length is 3 characters and min length is 2 character")]
        public string CountryCode { get; set; }
    }
}

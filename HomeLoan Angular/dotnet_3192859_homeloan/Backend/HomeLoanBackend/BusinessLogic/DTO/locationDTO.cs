using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTO
{
    public class locationDTO
    {
        public string countryName { get; set; }
        public string stateName { get; set; }
        public string cityName { get; set; }
        public Guid countryId { get; set; }
        public Guid stateId { get; set; }
        public Guid cityId { get; set; }
        public string countryCode { get; set; }
        public string stateCode { get; set; }
        public string cityCode { get; set; }
    }
}

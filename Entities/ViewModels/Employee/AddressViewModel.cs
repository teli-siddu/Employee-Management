using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Employee
{
    public class AddressViewModel:BaseAddressViewModel
    {
     
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public bool PermnentAddress { get; set; }

        public string EmployeeId { get; set; }

        public string AddressType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Employee
{
    public class AddAddressViewModel: BaseAddressViewModel
    {
       
        public int? CountryMasterId { get; set; }
        public int? StateMasterId { get; set; }
        public int? CityMasterId { get; set; }
        public int? AddressTypeId { get; set; }
    }
}

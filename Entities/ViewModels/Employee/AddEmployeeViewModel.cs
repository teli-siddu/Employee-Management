using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Employee
{
   public class AddEmployeeViewModel:BaseEmployeeViewModel
    {
     
        public int? GenderId { get; set; }

        public int? MaritialStatusId { get; set; }
        public int? NationalityId { get; set; }
        public int AddressTypeId { get; set; }

        public List<MobileViewModel> Mobiles { get; set; } 
        public List<EmailViewModel> Emails { get; set; } 
        public int? DepartmentId { get; set; }
        public BankDetailsViewModel BankDetails { get; set; }
        public List<AddAddressViewModel> Addresses { get; set; }


        public List<RoleViewModel> Roles { get; set; }

        public List<KeyValue<int,string>>  Deapartments { get; set; }
        public List<KeyValue<int,string>> Genders { get; set; }
        public List<KeyValue<int,string>> MaritialStatuses { get; set; }
        public List<KeyValue<int,string>> Countries { get; set; }
        public List<KeyValue<int,string>> Nationalities { get; set; }
    }
}

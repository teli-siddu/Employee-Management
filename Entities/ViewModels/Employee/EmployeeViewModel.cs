using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Employee
{
    public class EmployeeViewModel:BaseEmployeeViewModel
    {
        public string Gender { get; set; }

        public string MaritialStatus { get; set; }
        public string Nationality { get; set; }
        public List<MobileViewModel> Mobiles { get; set; }
        public List<EmailViewModel> Emails { get; set; }

        public List<RoleViewModel> Roles { get; set; }
        public string    Department { get; set; }
        public BankDetailsViewModel BankDetails { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}

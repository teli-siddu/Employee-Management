using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class EmployeeViewModel:BasicEmployeeViewModel
    {
        public int Id { get; set; }
       public string   Nationality { get; set; }

       

        public List<string> Mobiles { get; set; }
        public List<string> Emails { get; set; }
        public string Department { get; set; }
        public BankDetails BankDetails { get; set; }
        
        public List<AddressViewModel> Addresses { get; set; }
     

    }
}

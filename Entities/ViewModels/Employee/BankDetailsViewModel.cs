using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Employee
{
   public class BankDetailsViewModel
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
    }
}

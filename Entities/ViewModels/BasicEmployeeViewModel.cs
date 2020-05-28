using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class BasicEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }

        public string Designation { get; set; }

        public string DateofJoining { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FatherName { get; set; }

        public string PassportNumber { get; set; }
    }
}

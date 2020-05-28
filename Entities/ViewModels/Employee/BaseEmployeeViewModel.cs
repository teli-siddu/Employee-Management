using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.Employee
{
    public class BaseEmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }

        public string Designation { get; set; }

        public string DateofJoining { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FatherName { get; set; }

        public string PassportNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

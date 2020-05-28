using Entities.Helper;
using Entities.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Users
{
    public class AddUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }
        public List<MobileViewModel> Mobiles { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<RoleViewModel> Roles { get; set; }

        public List<KeyValue<int,string>> departments { get; set; }

        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public string RoleId { get; set; }
        public string Token { get; set; }


    }
}

using Entities.ViewModels.Departments;
using Entities.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
   public class UserViewModel
    {

        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Name { get; set; }
       
        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<MobileViewModel> Mobiles { get; set; }
        public List<RoleViewModel> Roles { get; set; }

        public List<DepartmentViewModel> departments { get; set; }

        public int DepartmentId { get; set; }

      

        public string RoleId { get; set; }
        public string Token { get; set; }
       
    }
}

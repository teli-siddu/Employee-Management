using Entities.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
   public class AccessTokenViewModel
    {

        //public string UserName { get; set; }

        //public string Name { get; set; }
        //public List<string> Roles { get; set; }

        //public string ProfilePictureUrl { get; set; }
        public EmployeeViewModel User { get; set; }
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
        public string RefreshToken { get; set; }
    }
}

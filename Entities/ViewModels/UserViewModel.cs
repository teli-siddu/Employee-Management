using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
   public class UserViewModel
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<UserRoleViewModel> Roles { get; set; }
        public string Token { get; set; }
       
    }
}

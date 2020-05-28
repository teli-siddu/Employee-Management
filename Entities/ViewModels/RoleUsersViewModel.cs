using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class RoleUsersViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}

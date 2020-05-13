using System;
using System.Collections.Generic;
using System.Text;

namespace MVCProject.ViewModels
{
    public class UsersViewModel
    {
       public IEnumerable<UserViewModel> Users { get; set; }
    }
}

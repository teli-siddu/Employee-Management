using System;
using System.Collections.Generic;
using System.Text;

namespace MVCProject.ViewModels
{
    public class UserMenuViewModel
    {
        public string RoleName { get; set; }

        public List<MenuViewModel> menuViews { get; set; }
    }
}

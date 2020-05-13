using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class UserMenuViewModel
    {
        public string RoleName { get; set; }

        public List<MenuViewModel> menuViews { get; set; }
    }
}

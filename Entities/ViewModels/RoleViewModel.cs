using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}

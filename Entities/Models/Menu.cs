using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Entities.Models
{
    public class Menu
    {
        
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<RoleMenu> RoleMenus { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }


    }
}

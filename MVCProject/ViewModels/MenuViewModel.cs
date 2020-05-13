using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCProject.ViewModels
{
   public class MenuViewModel
    {
        public string Name { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        
    }
}

using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
   public class MenuViewModel
    {
        //public string Name { get; set; }
        //public List<MenuItem> MenuItems { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? ParentId { get; set; }

        public List<MenuViewModel> Children { get; set; }
    }
}

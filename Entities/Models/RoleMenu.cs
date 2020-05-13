using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class RoleMenu
    {
        [ForeignKey("FK_RoleId")]
        public string    RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        [ForeignKey("FK_MenuId")]
        public string MenuId { get; set; }
        public Menu Menu { get; set; }
        
    }
}

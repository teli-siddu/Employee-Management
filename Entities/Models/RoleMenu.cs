using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class RoleMenu
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("MenuItemId")]
        public MenuItem  MenuItem { get; set; }

        public int MenuItemId { get; set; }


    }
}

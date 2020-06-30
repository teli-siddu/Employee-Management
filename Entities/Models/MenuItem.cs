using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class MenuItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public int ParentId { get; set; }

        public ApplicationRole Role { get; set; }

        [ForeignKey("Fk_RoleId")]
        public string RoleId { get; set; }
        //public Menu Menu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Departments")]
   public class Department
    {
        
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

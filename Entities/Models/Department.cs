using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Departments")]
   public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }

        //public IEnumerable<Employee> Employees { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Departments
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}

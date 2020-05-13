using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FilePath { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Departments Department { get; set; }
    }
}

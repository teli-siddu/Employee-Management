using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class NationalityMaster
    {
        public int Id { get; set; }
        public string Nationality { get; set; }

        public ICollection<Employee> employees { get; set; }
    }
}

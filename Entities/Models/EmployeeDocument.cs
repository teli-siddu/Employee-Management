using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("EmployeeDocuments")]
    public class EmployeeDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Mobile
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }

        [ForeignKey("Employee_UserId")]
        public Employee  Employee { get; set; }

       
        public int Employee_UserId { get; set; }
    }
}

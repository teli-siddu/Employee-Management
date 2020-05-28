using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
   public class Absence
    {
       
        public int Employee_UserId { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("Employee_UserId")]
        public Employee Employee { get; set; }
    }
}

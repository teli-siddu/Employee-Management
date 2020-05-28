using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public   class EmployeeRole:IdentityUserRole<int>
    {
        //public string RoleName { get; set; }

       
        public virtual Employee User { get; set; }

       
        
        public virtual ApplicationRole Role { get; set; }


    }
}

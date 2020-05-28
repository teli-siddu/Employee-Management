using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual ICollection<EmployeeRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

        //public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        // public virtual ICollection<ApplicationUser> Users { get; set; }

        public bool IsActive { get; set; }

    }
}

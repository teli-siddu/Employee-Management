using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

        //public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        // public virtual ICollection<ApplicationUser> Users { get; set; }

        public bool IsActive { get; set; }

    }
}

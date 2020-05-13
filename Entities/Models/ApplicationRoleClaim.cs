using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ApplicationRoleClaim:IdentityRoleClaim<string>
    {
        
        public virtual ApplicationRole Role { get; set; }
    }
}

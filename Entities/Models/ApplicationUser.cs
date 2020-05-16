using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class ApplicationUser:IdentityUser<string>
    {
        public bool IsActive { get; set; }
        public string City { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get 
            {
                return $"{FirstName} {LastName}"; 
            }
        }
    
      
        public DateTime DateofBirth { get; set; }
        public string RefreshToken { get; set; }
        public  ICollection<ApplicationUserClaim> Claims { get; set; }
        public  ICollection<ApplicationUserLogin> Logins { get; set; }
        public  ICollection<ApplicationUserToken> Tokens { get; set; }
        public  ICollection<ApplicationUserRole> UserRoles { get; set; }
        //public virtual ICollection<ApplicationRole> Roles { get; set; }



    }
}

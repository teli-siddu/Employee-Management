using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{

    [Table("Employees")]
   public class Employee: IdentityUser<int>
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime DateOfBirth { get; set; }

     

        [ForeignKey("FK_Employee_Gender")]
        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        [ForeignKey("FK_Employee_Maritial")]
        public int? MaritialStatusId { get; set; }

        public MaritialStatus MaritialStatus { get; set; }

        public string FatherName { get; set; }
        
        public string PassportNumber { get; set; }

        public string EmployeeId { get; set; }

        public string Designation { get; set; }

        public string DateofJoining { get; set; }


        public virtual ICollection<Mobile> Mobiles { get; set; }

        public virtual ICollection<Email> Emails { get; set; }

        public virtual ICollection<ProfilePicture> profilePictures { get; set; }

        [ForeignKey("FK_Employee_Dept")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("FK_Employee_Dept")]
        public int? BankDetailsId { get; set; }
        public BankDetails BankDetails { get; set; }

        //[ForeignKey("FK_Employee_CurrentAddress")]
        //public int? CurrentAddressId { get; set; }
        //public CurrentAddress CurrentAddress { get; set; }

        //[ForeignKey("FK_Employee_PermnentAddress")]
        //public int? PermnentAddressId { get; set; }
        //public PermanentAddress PermnentAddress { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        [ForeignKey("FK_Employee_Nationality")]
        public virtual NationalityMaster Nationality { get; set; }
        public int? NationalityId { get; set; }

        //public ICollection<Absence> Absences { get; set; }
        public ICollection<Leave> Leaves { get; set; }

        public ICollection<EmployeeClaim> Claims { get; set; }
        public ICollection<EmployeeLogin> Logins { get; set; }
        public ICollection<EmployeeToken> Tokens { get; set; }
        public ICollection<EmployeeRole> UserRoles { get; set; }



    }
}

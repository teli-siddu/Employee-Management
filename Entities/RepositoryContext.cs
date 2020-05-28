using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext: IdentityDbContext<Employee,ApplicationRole,int,EmployeeClaim,EmployeeRole,EmployeeLogin,ApplicationRoleClaim,EmployeeToken>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options)
        {

        }

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Owner> Owners { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }


        public DbSet<Employee> Employees { get; set; }

        //public DbSet<CurrentAddress> CurrentAddresses { get; set; }
        //public DbSet<PermanentAddress> PermanentAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType>  AddressTypes { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritialStatus> MaritialStatuses { get; set; }

        public DbSet<CountryMaster> CountryMaster { get; set; }

        public DbSet<NationalityMaster> NationalityMaster { get; set; }

        public DbSet<StateMaster> StateMaster { get; set; }
        public DbSet<CityMaster> CityMaster { get; set; }
        public DbSet<Department> Departments { get; set; }

        //public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        //public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<LeaveTypeMaster> LeaveTypes { get; set; }

        public DbSet<LeaveStatusMaster> LeaveStatuses { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
            builder.Maptables();

        }
    }
}

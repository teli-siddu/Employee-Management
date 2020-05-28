using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class TableMappingExtension
    {
        public static void Maptables(this ModelBuilder builder) 
        {
            builder.Entity<Employee>(b =>
            {
                b.Property(x => x.Id).ValueGeneratedOnAdd();

                //b.HasKey(x => x.Id);
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                //b.HasOne(x => x.Department)

                //.WithMany()
                //.HasForeignKey(x => x.DepartmentId);

                b.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId);

                // Each User can have many entries in the UserRole join table
                //b.HasMany(e => e.UserRoles)
                //    .WithOne()
                //    .HasForeignKey(ur=>new { ur.RoleId,ur.UserId})
                //    .IsRequired();

                //b.HasMany(e => e.UserRoles)
                //   .WithOne()
                //   .HasForeignKey(ur => ur.RoleId)
                //   .IsRequired();
                //b.HasOne(x => x.Department)
                //.WithMany()
                //.HasForeignKey(x => x.DepartmentId)
                //.OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<ApplicationRole>(ar =>
            {
                ar.HasMany(ur => ur.UserRoles)
                   .WithOne(ur => ur.Role)
                   .HasForeignKey(ur => ur.RoleId)
                   .IsRequired();

                ar.HasMany(uc => uc.RoleClaims)
                .WithOne(uc => uc.Role)
                .HasForeignKey(uc => uc.RoleId)
                .IsRequired();
                //ar.HasMany(uc => uc.RoleMenus)
                //.WithOne(uc => uc.Role)
                //.HasForeignKey(uc => uc.RoleId);


            });

            builder.Entity<EmployeeRole>(entity =>
            {
                entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);


            });

            builder.Entity<Department>(entity =>
            {
                entity.HasMany(x => x.Employees)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId)
                ;


            });

            //builder.Entity<RoleMenu>(entity =>
            //{
            //    entity.HasKey(e => new { e.MenuId, e.RoleId });
            //    entity.HasOne(x => x.Role)
            //    .WithMany(x => x.RoleMenus)
            //    .HasForeignKey(x => x.RoleId);

            //    entity.HasOne(x => x.Menu)
            //    .WithMany(x => x.RoleMenus)
            //    .HasForeignKey(x => x.MenuId);
            //});

            builder.Entity<Employee>(entity =>
            {
                entity.HasMany<Mobile>(x => x.Mobiles)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.Employee_UserId);

                entity.HasMany<Email>(x => x.Emails)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.Employee_UserId);

                entity.HasMany<ProfilePicture>(x => x.profilePictures)
               .WithOne(x => x.Employee)
               .HasForeignKey(x => x.Employee_UserId);

                entity.HasOne<Department>(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId);

                // entity.HasOne<PermanentAddress>(x => x.PermnentAddress)
                // .WithMany(x => x.Employees)
                // .HasForeignKey(x => x.PermnentAddressId);

                // entity.HasOne<CurrentAddress>(x => x.CurrentAddress)
                //.WithMany(x => x.Employees)
                //.HasForeignKey(x => x.CurrentAddressId);
                entity.HasMany<Address>(x => x.Addresses)
                 .WithOne(x => x.Employee)
                 .HasForeignKey(x => x.Employee_UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                

                entity.HasOne<BankDetails>(x => x.BankDetails)
               .WithMany(x => x.Employees)
               .HasForeignKey(x => x.BankDetailsId);


            });

            //builder.Entity<PermanentAddress>(entity =>
            //{
            //    entity.HasOne<CountryMaster>(x => x.Country)
            //    .WithMany(x => x.PermanentAddresses)
            //    .HasForeignKey(x => x.CountryMasterId);

            //    entity.HasOne<StateMaster>(x => x.State)
            //   .WithMany(x => x.PermanentAddresses)
            //   .HasForeignKey(x => x.StateMasterId);

            //    entity.HasOne<CityMaster>(x => x.City)
            //  .WithMany(x => x.PermanentAddresses)
            //  .HasForeignKey(x => x.CityMasterId);

            //});

            //builder.Entity<CurrentAddress>(entity =>
            //{
            //  entity.HasOne<CountryMaster>(x => x.Country)
            //  .WithMany(x => x.CurrentAddresses)
            //  .HasForeignKey(x => x.CountryMasterId);

            //  entity.HasOne<StateMaster>(x => x.State)
            // .WithMany(x => x.CurrentAddresses)
            // .HasForeignKey(x => x.StateMasterId);

            //  entity.HasOne<CityMaster>(x => x.City)
            //.WithMany(x => x.CurrentAddresses)
            //.HasForeignKey(x => x.CityMasterId);

            //});


            builder.Entity<Address>(entity =>
            {
                entity.HasOne<CountryMaster>(x => x.Country)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.CountryMasterId);

                entity.HasOne<StateMaster>(x => x.State)
               .WithMany(x => x.Addresses)
               .HasForeignKey(x => x.StateMasterId);

                entity.HasOne<CityMaster>(x => x.City)
              .WithMany(x => x.Addresses)
              .HasForeignKey(x => x.CityMasterId);
            });

            builder.Entity<CountryMaster>(entity =>
            {
                entity.HasMany(x => x.States)
                  .WithOne(x => x.Country)
                  .HasForeignKey(x => x.CountryMasterId);

            });



            builder.Entity<StateMaster>(entity =>
            {
                entity.HasMany(x => x.Cities)
                 .WithOne(x => x.State)
                 .HasForeignKey(x => x.StateMasterId);
            });
            builder.Entity<CityMaster>(entity =>
            {
                entity.HasOne<StateMaster>(x => x.State)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.StateMasterId);
            });


        }
    }
}

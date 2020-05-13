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
    public class RepositoryContext: IdentityDbContext<ApplicationUser,ApplicationRole,string,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin,ApplicationRoleClaim,ApplicationUserToken>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options)
        {

        }

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Owner> Owners { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
       

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        //public DbSet<RoleMenu> RoleMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();

            builder.Entity<ApplicationUser>(b =>
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

                // Each User can have many entries in the UserRole join table
                //b.HasMany(e => e.UserRoles)
                //    .WithOne()
                //    .HasForeignKey(ur=>new { ur.RoleId,ur.UserId})
                //    .IsRequired();

                //b.HasMany(e => e.UserRoles)
                //   .WithOne()
                //   .HasForeignKey(ur => ur.RoleId)
                //   .IsRequired();
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

            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);


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

        }
    }
}

using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class AdminRepository :RepositoryBase<ApplicationUser> ,IAdminRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RepositoryContext _repositoryContext;

        public AdminRepository(RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager,Entities.RepositoryContext repositoryContext):base(repositoryContext)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._repositoryContext = repositoryContext;
        }
        public async Task<IdentityResult> CreateRole(ApplicationRole ApplicationRole)
        {
            
            return  await _roleManager.CreateAsync(ApplicationRole);
        }

        public async Task<ApplicationRole> FindRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public IEnumerable<RoleViewModel> GetRoles()
        {

            var roles= _roleManager.Roles.Select(x => new RoleViewModel
            {
                RoleId = x.Id,
                RoleName = x.Name
            }).ToList();
            return roles;
        }

        public async Task<IEnumerable<string>> GetUsersByRoleName(string roleName)
        {
             var applicationUsers = await _userManager.GetUsersInRoleAsync(roleName);

             return applicationUsers.Select(x => x.UserName);
        }

        public async Task<bool> CheckUserIsMemberofRole(ApplicationUser user,string roleName) 
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<ApplicationRole> GetRoleById(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);

            

        }

        public List<ApplicationUser> GetUsers() 
        {
            return _userManager.Users.ToList();
        }

        public List<UserViewModel> GetUsersRoles() 
        {
            //IQueryable<ApplicationUser> users=  _userManager.Users;

            // IQueryable<ApplicationRole> roles = _roleManager.Roles;
            // IQueryable<ApplicationUserRole> userRoles = _repositoryContext.UserRoles;
            // var usewerss = users.Select(x => new ApplicationUser()
            // {
            //     UserRoles = userRoles
            //                  .Where(ur => ur.UserId == x.Id)
            //                  .Select(ur => new ApplicationUserRole()
            //                  {
            //                      Role = ur.Role
            //                  })
            //                  .ToList(),
            //     UserName = x.UserName,
            //     Email = x.Email
            // })
            //   .Select(x => new ApplicationUser { UserName = x.UserName, UserRoles = x.UserRoles })
            //.ToList();
            //return usewerss;
            IQueryable<ApplicationUser> users= _repositoryContext.Users;
            IQueryable<ApplicationRole> roles= _repositoryContext.Roles;
            IQueryable<ApplicationUserRole> userRoles = _repositoryContext.UserRoles;


            var users1 = users.Select(x=>new UserViewModel() 
                         {
                             Roles = x.UserRoles.Select(x => new UserRoleViewModel {RoleName=x.Role.Name,RoleId=x.RoleId }).ToArray(),
                             UserName =x.UserName,
                             UserId=x.Id
                             

                         })
                         .ToList();
            //var ss = users.Select(x => x.UserRoles).ToList();

            //var x=  users.Join(userRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
            //    .Join(roles, uur => uur.ur.RoleId, r => r.Id, (uur, r) => new { uur, r })
            //    .Select(m => new
            //    {
            //        UserName = m.uur.u.UserName,
            //        Email = m.uur.u.Email,
            //        Roles = m.r.Name
            //    }).ToList();




          
            



            return users1;
        }

        public async Task<IdentityResult> AddRole(ApplicationUser user,string role) 
        {
            IdentityResult result= await _userManager.AddToRoleAsync(user, role);

            return result;


        }
        public async Task<IdentityResult> RemoveRole(ApplicationUser user, string role)
        {
            
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<ApplicationUser> GetUserByUserName(string username) 
        {
            return await _userManager.FindByNameAsync(username);
        }
        
        public async Task<IdentityResult> EditRole(ApplicationRole role) 
        {
           return  await  _roleManager.UpdateAsync(role);

        }

        public async Task<IdentityResult> DeleteUserById(string userid) 
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userid);
            var result=  await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<ApplicationUser> GetUserById(string userId) 
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> DeleteRoleById(string roleId) 
        {
            ApplicationRole role= await _roleManager.FindByIdAsync(roleId);
            return await _roleManager.DeleteAsync(role);
        }



     
           


        //public async Task<string> GetToken(string userName,string password) 
        //{
        //   ApplicationUser usr  await  GetUserByUserName(userName);
        //}

    }
}

using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRepository(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task<IdentityResult> CreateRole(IdentityRole identityRole)
        {
            
            return  await _roleManager.CreateAsync(identityRole);
        }

        public async Task<IdentityRole> FindRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return  _roleManager.Roles;
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

        public async Task<IdentityRole> GetRoleById(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);

        }

        public List<ApplicationUser> GetUsers() 
        {
            return  _userManager.Users.ToList();
        }

        public async Task<IdentityResult> AddRole(ApplicationUser user,string role) 
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
        public async Task<IdentityResult> RemoveRole(ApplicationUser user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<ApplicationUser> GetUserByUserName(string username) 
        {
            return await _userManager.FindByNameAsync(username);
        }
        
        public async Task<IdentityResult> EditRole(IdentityRole role) 
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
            IdentityRole role= await _roleManager.FindByIdAsync(roleId);
            return await _roleManager.DeleteAsync(role);
        }



     
           


        //public async Task<string> GetToken(string userName,string password) 
        //{
        //   ApplicationUser usr  await  GetUserByUserName(userName);
        //}

    }
}

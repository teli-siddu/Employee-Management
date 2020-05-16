using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdminRepository
    {
        //Task<IdentityResult> CreateRole(ApplicationRole ApplicationRole);
        //IEnumerable<RoleViewModel> GetRoles();
        //List<ApplicationUser> GetUsers();
        //Task<ApplicationRole> FindRoleById(string id);

        Task<IEnumerable<string>> GetUsersByRoleName(string roleName);

        //Task<IdentityResult> EditRole(ApplicationRole role);
        //Task<ApplicationRole> GetRoleById(string roleId);
        //List<UserViewModel> GetUsersRoles();
        //Task<bool> CheckUserIsMemberofRole(ApplicationUser user, string roleName);

        //Task<IdentityResult> AddRole(ApplicationUser user, string role);

        //Task<IdentityResult> RemoveRole(ApplicationUser user, string role);
        //Task<ApplicationUser> GetUserByUserName(string username);
        //Task<IdentityResult> DeleteUserById(string userid);
        //Task<ApplicationUser> GetUserById(string userId);
        //Task<IdentityResult> DeleteRoleById(string roleId);



    }
}

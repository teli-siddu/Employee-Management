﻿using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdminRepository
    {
        Task<IdentityResult> CreateRole(IdentityRole identityRole);
        IEnumerable<IdentityRole> GetRoles();

        Task<IdentityRole> FindRoleById(string id);

        Task<IEnumerable<string>> GetUsersByRoleName(string roleName);

        Task<IdentityResult> EditRole(IdentityRole role);
        Task<IdentityRole> GetRoleById(string roleId);
        List<ApplicationUser> GetUsers();
        Task<bool> CheckUserIsMemberofRole(ApplicationUser user, string roleName);

        Task<IdentityResult> AddRole(ApplicationUser user, string role);
        Task<IdentityResult> RemoveRole(ApplicationUser user, string role);
        Task<ApplicationUser> GetUserByUserName(string username);
        Task<IdentityResult> DeleteUserById(string userid);
        Task<ApplicationUser> GetUserById(string userId);
        Task<IdentityResult> DeleteRoleById(string roleId);



    }
}

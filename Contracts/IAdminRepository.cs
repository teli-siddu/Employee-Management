using Entities.Models;
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


    }
}

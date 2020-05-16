using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRolesRepository
    {
        Task<IdentityResult> AddRole(ApplicationRole ApplicationRole);
        
        Task<IdentityResult> DeleteRole(ApplicationRole role);

        Task<IdentityResult> DeleteRoleById(string roleId);
        Task<IdentityResult> EditRole(ApplicationRole role);
        Task<ApplicationRole> GetRoleById(string roleId);
        Task<ApplicationRole> FindRoleById(string id);
        Task<List<RoleViewModel>> GetRoles();
    }
}

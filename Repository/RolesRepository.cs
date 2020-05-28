using Contracts;
using Entities;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly RepositoryContext _repositoryContext;

        public RolesRepository(RoleManager<ApplicationRole> roleManager,RepositoryContext repositoryContext)
        {
            this._roleManager = roleManager;
            this._repositoryContext = repositoryContext;
        }
        public async Task<IdentityResult> AddRole(ApplicationRole ApplicationRole)
        {
            return await _roleManager.CreateAsync(ApplicationRole);
        }

        public async Task<IdentityResult> DeleteRole(ApplicationRole role)
        {
            return await _roleManager.DeleteAsync(role);
           
        }

        public async Task<IdentityResult> DeleteRoleById(int roleId)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityResult> EditRole(ApplicationRole role)
        {
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<ApplicationRole> FindRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<ApplicationRole> GetRoleById(int roleId)
        {
            return await _roleManager.FindByIdAsync(roleId.ToString());
        }

        public async  Task<List<RoleViewModel>> GetRoles()
        {
            var roles =await  _roleManager.Roles.Select(x => new RoleViewModel() 
            {
                RoleId=x.Id,
                RoleName=x.Name
            }).ToListAsync();
            return roles;
        }

       
    }
}

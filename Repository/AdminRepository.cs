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
    public class AdminRepository :RepositoryBase<Employee> ,IAdminRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<Employee> _userManager;
        private readonly RepositoryContext _repositoryContext;

        public AdminRepository(RoleManager<ApplicationRole> roleManager,UserManager<Employee> userManager,Entities.RepositoryContext repositoryContext):base(repositoryContext)
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
             var Employees = await _userManager.GetUsersInRoleAsync(roleName);

             return Employees.Select(x => x.UserName);
        }

        public async Task<bool> CheckUserIsMemberofRole(Employee user,string roleName) 
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<ApplicationRole> GetRoleById(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);

            

        }

        public List<Employee> GetUsers() 
        {
            return _userManager.Users.ToList();
        }

       

        public async Task<IdentityResult> AddRole(Employee user,string role) 
        {
            IdentityResult result= await _userManager.AddToRoleAsync(user, role);

            return result;


        }
       

        public async Task<Employee> GetUserByUserName(string username) 
        {
            return await _userManager.FindByNameAsync(username);
        }
        
        public async Task<IdentityResult> EditRole(ApplicationRole role) 
        {
           return  await  _roleManager.UpdateAsync(role);

        }

        public async Task<IdentityResult> DeleteUserById(string userid) 
        {
            Employee user = await _userManager.FindByIdAsync(userid);
            var result=  await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<Employee> GetUserById(string userId) 
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
        //   Employee usr  await  GetUserByUserName(userName);
        //}

    }
}

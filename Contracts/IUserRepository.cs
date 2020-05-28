using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        //Task<User> Authenticate(string userName, string password);
        //public IEnumerable<User> GetAll();

        //Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IdentityResult> AddUser(Employee user, string password);

        Task<Employee> GetUserByUserName(string username);

        Task<Employee> FindUserById(int userId);
        List<Employee> GetUsers();

        Task<IEnumerable<string>> GetUsersByRoleName(string roleName);
        Task<Employee> GetUserById(int userId);

        Task<IdentityResult> DeleteUserById(int userId);

        Task<IdentityResult> AddRole(Employee user, string role);

        List<UserViewModel> GetUsersRoles();

        Task<bool> CheckUserIsMemberofRole(Employee user, string roleName);

        Task<IdentityResult> RemoveUserRole(Employee user, string role);

        //Task<IdentityResult> EditUser(UserViewModel userView);
        List<RoleViewModel> UserSelectedRoles(Employee user);

        Task<IdentityResult> UpdateUser(Employee user);
        Task<IEnumerable<string>> GetUserRoles(Employee user);
        Task<IdentityResult> RemoveFromRoles(Employee user, IEnumerable<string> roles);
        Task<IdentityResult> AddToRoles(Employee user, IEnumerable<string> roles);
    }
        
}

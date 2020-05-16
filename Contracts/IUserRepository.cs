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
        Task<User> Authenticate(string userName, string password);
        public IEnumerable<User> GetAll();

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IdentityResult> AddUser(ApplicationUser user, string password);

        Task<ApplicationUser> GetUserByUserName(string username);

        List<ApplicationUser> GetUsers();

        Task<IEnumerable<string>> GetUsersByRoleName(string roleName);
        Task<ApplicationUser> GetUserById(string userId);

        Task<IdentityResult> DeleteUserById(string userId);

        Task<IdentityResult> AddRole(ApplicationUser user, string role);

        List<UserViewModel> GetUsersRoles();

        Task<bool> CheckUserIsMemberofRole(ApplicationUser user, string roleName);

        Task<IdentityResult> RemoveUserRole(ApplicationUser user, string role);

        //Task<IdentityResult> EditUser(UserViewModel userView);
        List<RoleViewModel> UserSelectedRoles(ApplicationUser user);

        Task<IdentityResult> UpdateUser(ApplicationUser user);
        Task<IEnumerable<string>> GetUserRoles(ApplicationUser user);
        Task<IdentityResult> RemoveFromRoles(ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> AddToRoles(ApplicationUser user, IEnumerable<string> roles);
    }
        
}

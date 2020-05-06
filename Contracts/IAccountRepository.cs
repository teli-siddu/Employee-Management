using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository 
    {
        Task<IdentityUser> Register(ApplicationUser user);
        Task<IdentityUser> Login(ApplicationUser user);
        Task SignOut();
        Task<SignInResult> SignIn(string userName, string password, bool rememberMe);
        Task<ApplicationUserViewModel> GetSecurityToken(string userName, string password);
    }
}

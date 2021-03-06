﻿using Entities.Models;
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
        Task<IdentityUser> Register(Employee user);
        Task<IdentityUser> Login(Employee user);
        Task SignOut();
        Task<SignInResult> SignIn(string userName, string password, bool rememberMe);
        Task<AccessTokenViewModel> GetSecurityToken(string userName, string password);

        string GenerateRefreshToken();
    }
}

﻿using Contracts;
using Entities;
using Entities.HelperModels;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private AppSettings _appSettings;

        private IMenuRepository _MenuRepository;

        public AccountRepository(SignInManager<Employee> signInManager,UserManager<Employee> userManager,IMenuRepository menuRepository, IOptions<AppSettings> appSettings)
           
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._MenuRepository = menuRepository;
            this._appSettings = appSettings.Value;
        }

        public async Task<IdentityUser> Login(Employee user)
        {
           await  _signInManager.SignInAsync(user, isPersistent: false);
         
            
            throw new NotImplementedException();
        }


        public  async Task<SignInResult> SignIn(string userName,string password,bool rememberMe) 
        {
           
           return await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
        }
        public async Task SignOut()
        {
              await _signInManager.SignOutAsync();
        }

        public Task<IdentityUser> Register(Employee user)
        {
            throw new NotImplementedException();
        }

        public async Task<AccessTokenViewModel> GetSecurityToken(string userName, string password)
        {

          Employee user= await  _userManager.FindByNameAsync(userName);
          bool validUser= await _userManager.CheckPasswordAsync(user, password);
           
            if (!validUser) 
            {
                return null;
            }

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim("Employee_Id",user.Id.ToString())
            };       
            var userRoles = await _userManager.GetRolesAsync(user);
           // var userMenu = _MenuRepository.GetMenu(userRoles[0]);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            //claims.Add(new Claim( "UserMenu", JsonConvert.SerializeObject(userMenu)));
           
            //var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims, "Token"),
               
                Expires = expires,
                SigningCredentials = creds,
              

            };

            var tokenHandler = new JwtSecurityTokenHandler();
           var securityToken=  tokenHandler.CreateToken(securityTokenDescriptor);
           string token= tokenHandler.WriteToken(securityToken);
           string refreshToken = GenerateRefreshToken();
           user.RefreshToken = refreshToken;
           IdentityResult result=   await _userManager.UpdateAsync(user);

            return new AccessTokenViewModel
            {

                Token = token,
                RefreshToken = refreshToken,
                TokenExpiration = securityTokenDescriptor.Expires.ToString()

            };
           

        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

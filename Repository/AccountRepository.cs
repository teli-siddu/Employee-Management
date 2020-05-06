using Contracts;
using Entities;
using Entities.HelperModels;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private AppSettings _appSettings;

        public AccountRepository(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
           
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._appSettings = appSettings.Value;
        }

        public async Task<IdentityUser> Login(ApplicationUser user)
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

        public Task<IdentityUser> Register(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUserViewModel> GetSecurityToken(string userName, string password)
        {

          ApplicationUser user= await  _userManager.FindByNameAsync(userName);
          bool validUser= await _userManager.CheckPasswordAsync(user, password);

            if (!validUser) 
            {
                return null;
            }

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.Id)
            };       
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            //var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims, "Token"),
                Expires = expires,
                SigningCredentials = creds

            };
            var tokenHandler = new JwtSecurityTokenHandler();
           var securityToken=  tokenHandler.CreateToken(securityTokenDescriptor);
           string token= tokenHandler.WriteToken(securityToken);

            return new ApplicationUserViewModel
            {
                ApplicationUser = user,
                Token = token
            };
           

        }
    }
}

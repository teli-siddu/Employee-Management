using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Entities.HelperModels;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public class UserRepository :RepositoryBase<User>,IUserRepository
    {

        private AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserRepository(RepositoryContext repositoryContext,IOptions<AppSettings> appSettings,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)  : base(repositoryContext)
        {
            _appSettings = appSettings.Value;
            this._userManager = userManager;
        }
        //public UserRepository(RepositoryContext repositoryContext, IOptions<AppSettings> appSettings) :this(repositoryContext)
        //{
            
        //    _appSettings = appSettings.Value;
            
        //}
       





        public async  Task<User> Authenticate(string userName, string password)
        {
            User user= await FindByCondition(x => x.Username == userName 
                                            && x.Password == password)
                                            .FirstOrDefaultAsync();

            if (user == null) 
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
              }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() 
        {
            return await FindAll().Select(x => new User()
            {
                FirstName = x.FirstName
            }).ToListAsync();
                        
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> Register(ApplicationUser user,string password)
        {
            var result= await _userManager.CreateAsync(user, password);
            if (result.Succeeded) 
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                
            }
            return result;

        }

       
    }
}

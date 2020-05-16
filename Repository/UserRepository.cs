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
using Entities.ViewModels;

namespace Repository
{
    public class UserRepository :RepositoryBase<User>,IUserRepository
    {

        private AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RepositoryContext _repositoryContext;
        public UserRepository(RepositoryContext repositoryContext,IOptions<AppSettings> appSettings,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager)  : base(repositoryContext)
        {
            _appSettings = appSettings.Value;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._repositoryContext = repositoryContext;
        }
      
       





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
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);

                }
                return result;
            }
            catch (Exception x) 
            {
                return new IdentityResult();
            }
           

        }

        public async Task<IdentityResult> AddUser(ApplicationUser user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);

                }
                return result;
            }
            catch (Exception x)
            {
                return new IdentityResult();
            }
        }

        public List<ApplicationUser> GetUsers()
        {
            return _userManager.Users.OrderBy(x=>x.UserName).ToList();
        }

        public async Task<IEnumerable<string>> GetUsersByRoleName(string roleName)
        {
            var applicationUsers = await _userManager.GetUsersInRoleAsync(roleName);

            return applicationUsers.Select(x => x.UserName);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> DeleteUserById(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<ApplicationUser> GetUserByUserName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        

        public List<UserViewModel> GetUsersRoles()
        {
          
            IQueryable<ApplicationUser> users = _repositoryContext.Users;
            IQueryable<ApplicationRole> roles = _repositoryContext.Roles;
            IQueryable<ApplicationUserRole> userRoles = _repositoryContext.UserRoles;


            var users1 = users.Select(x => new UserViewModel()
            {
                Roles = x.UserRoles.Select(x => new RoleViewModel { RoleName = x.Role.Name, RoleId = x.RoleId }).ToList(),
                UserName = x.UserName,
                UserId = x.Id,
                City = x.City,
                Name = x.FirstName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateofBirth

            }).ToList();

            return users1;
        }

        public async Task<IdentityResult> AddRole(ApplicationUser user, string role)
        {
            IdentityResult result = await _userManager.AddToRoleAsync(user, role);
            

            return result;


        }
        public async Task<IdentityResult> AddRoles(ApplicationUser user,string[] roles) 
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<bool> CheckUserIsMemberofRole(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveUserRole(ApplicationUser user, string role)
        {

            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public  List<RoleViewModel> UserSelectedRoles(ApplicationUser user) 
        {
            var roles =  _userManager.GetRolesAsync(user).Result;
           List<ApplicationRole> allRoles=  _repositoryContext.Roles.ToList();
          List<RoleViewModel> roleView= allRoles.Select(x => new RoleViewModel()
            {
                RoleId = x.Id,
                RoleName = x.Name,
                IsSelected = roles.Any(y => y.ToUpper() == x.NormalizedName)
            }).ToList();

            return roleView;
        }


        public async Task<IdentityResult> UpdateUser(ApplicationUser user) 
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user) 
        {
           return await  _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveFromRoles(ApplicationUser user, IEnumerable<string> roles) 
        {
             return await _userManager.RemoveFromRolesAsync(user,roles);
        }
        public async Task<IdentityResult> AddToRoles(ApplicationUser user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }
      
       
    }
}

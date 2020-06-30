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
using Entities.ViewModels.Employee;

namespace Repository
{
    public class UserRepository :RepositoryBase<Employee>,IUserRepository
    {

        private AppSettings _appSettings;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<Employee> _signInManager;

        private readonly RepositoryContext _repositoryContext;
        public UserRepository(RepositoryContext repositoryContext,IOptions<AppSettings> appSettings,UserManager<Employee> userManager,SignInManager<Employee> signInManager,RoleManager<ApplicationRole> roleManager)  : base(repositoryContext)
        {
            _appSettings = appSettings.Value;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._repositoryContext = repositoryContext;
        }
      
       





        //public async  Task<User> Authenticate(string userName, string password)
        //{
        //    User user= await FindByCondition(x => x.Username == userName 
        //                                    && x.Password == password)
        //                                    .FirstOrDefaultAsync();

        //    if (user == null) 
        //    {
        //        return null;
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //      {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //      }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    user.Token = tokenHandler.WriteToken(token);
        //    user.Password = null;
        //    return user;

        //}

        //public async Task<IEnumerable<User>> GetAllUsersAsync() 
        //{
        //    return await FindAll().Select(x => new User()
        //    {
        //        FirstName = x.FirstName
        //    }).ToListAsync();
                        
        //}

        //public IEnumerable<User> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IdentityResult> Register(Employee user,string password)
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

        public async Task<IdentityResult> AddUser(Employee user, string password)
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

        public List<Employee> GetUsers()
        {
            return _userManager.Users.OrderBy(x=>x.UserName).ToList();
        }

        public async Task<IEnumerable<string>> GetUsersByRoleName(string roleName)
        {
            var Employees = await _userManager.GetUsersInRoleAsync(roleName);

            return Employees.Select(x => x.UserName);
        }


        public async Task<Employee> GetUserById(int userId)
        {
            //return await FindByCondition(x => x.Id == userId).FirstOrDefaultAsync();
            try
            {



            

               var xx= RepositoryContext.Employees.Include(x=>x.Department). Where(x => x.Id == userId).Select(x=>new Employee() 
               {
                   //City=x.City,
                   Department=x.Department,
                   DateOfBirth=x.DateOfBirth,
                   FirstName=x.FirstName,
                   Email=x.Email,
                   IsActive=x.IsActive,
                   LastName=x.LastName,
                   PhoneNumber=x.PhoneNumber,
                   UserName=x.UserName,
                   UserRoles=x.UserRoles,
                   RefreshToken=x.RefreshToken,
                   
               }).FirstOrDefault();
                var z = _userManager.FindByIdAsync(userId.ToString()).Result;
                return await _userManager.FindByIdAsync(userId.ToString());
            }
            catch (Exception x) 
            {
                return null;
            }
     
        }

        public async Task<Employee> FindUserById(int userId) 
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        public async Task<IdentityResult> DeleteUserById(int userId)
        {
            Employee user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<Employee> GetUserByUserName(string username)
        {
            return await _userManager.FindByNameAsync(username);
            
        }

        

        public List<UserViewModel> GetUsersRoles()
        {
          
            //IQueryable<Employee> users = _repositoryContext
            //                            .Employees
            //                            .Where(employee=>employee.UserRoles.Any(roles=>roles.Role.Name.ToLower()=="user"));
            //IQueryable<ApplicationRole> roles = _repositoryContext.Roles;
            //IQueryable<EmployeeRole> userRoles = _repositoryContext.UserRoles;



            var users1=  FindAll().Include(x => x.UserRoles)
                //.Where(emp => emp.UserRoles.Any(role => role.Role.NormalizedName == "USER"))
                .Select(x => new UserViewModel
                            {
                                UserName = x.UserName,
                                UserId = x.Id,
                                //City = x.City,
                                Name = x.FirstName,
                                Email = x.Email,
                                PhoneNumber = x.PhoneNumber,
                                Mobiles=x.Mobiles.Select(x=>new MobileViewModel 
                                {
                                    MobileNumber=x.MobileNumber
                                }).ToList(),
                                DateOfBirth = x.DateOfBirth,
                                Roles=x.UserRoles.Select(x=>new RoleViewModel 
                                {
                                    RoleId=x.RoleId,
                                    RoleName=x.Role.Name
                                }).ToList()

                            }
                ).ToList();

            //var users1 = users
            //    .Where(x=>x.UserRoles.Any(x=>x.Role.Name.ToLower()=="user"))
            //    .Select(x => new UserViewModel()
            //    {
            //        Roles = x.UserRoles
            //                    .Select(x => new RoleViewModel 
            //                                         {
            //                                            RoleName = x.Role.Name, RoleId = x.RoleId 
            //                                          })
            //                    .ToList(),
            //        UserName = x.UserName,
            //        UserId = x.Id,
            //        //City = x.City,
            //        Name = x.FirstName,
            //        Email = x.Email,
            //        PhoneNumber = x.PhoneNumber,
            //        DateOfBirth = x.DateOfBirth,
            //        //Department=new DepartmentViewModel 
            //        //{
            //        //    Id=x.Department.Id,
            //        //    Name=x.Department.Name
            //        //} 
            //    }).ToList();

            return users1;
        }

        public async Task<IdentityResult> AddRole(Employee user, string role)
        {
            IdentityResult result = await _userManager.AddToRoleAsync(user, role);
            

            return result;


        }
        public async Task<IdentityResult> AddRoles(Employee user,string[] roles) 
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<bool> CheckUserIsMemberofRole(Employee user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveUserRole(Employee user, string role)
        {

            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public  List<RoleViewModel> UserSelectedRoles(Employee user) 
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


        public async Task<IdentityResult> UpdateUser(Employee user) 
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<string>> GetUserRoles(Employee user) 
        {
           return await  _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveFromRoles(Employee user, IEnumerable<string> roles) 
        {
             return await _userManager.RemoveFromRolesAsync(user,roles);
        }
        public async Task<IdentityResult> AddToRoles(Employee user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }
      
       
    }
}

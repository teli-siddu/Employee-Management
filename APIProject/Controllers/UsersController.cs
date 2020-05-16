using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using APIProject.Models;
using Contracts;
using Entities.Helper;
using Entities.HelperModels;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private AppSettings _appSettings;

        public UsersController(IUserRepository userRepository, IOptions<AppSettings> appSettings, IRolesRepository rolesRepository)
        {
            //_repositoryWrapper = repositoryWrapper;
            _userRepository = userRepository;
            this._rolesRepository = rolesRepository;
            _appSettings = appSettings.Value;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userRepository.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRegisterViewModel user)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var applicationUser = new ApplicationUser
                {
                    Email = user.Email,
                    UserName = user.Email,
                    City = user.City,
                    DateofBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName

                };
                IdentityResult result = await _userRepository.AddUser(applicationUser, user.Password);
                if (result.Succeeded) 
                {
                     result = await _userRepository.AddToRoles(applicationUser,user.Roles.Where(x=>x.IsSelected).Select(x=>x.RoleName));
                }
             
                return Ok(result);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                ApplicationUser applicationUser = await _userRepository.GetUserById(id);

                if (applicationUser is null)

                {

                    return NotFound($"No user found with id {id}");

                    // return StatusCode(StatusCodes.Status200OK,"No user found with id {id}");
                }

                IdentityResult result = await _userRepository.DeleteUserById(id);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }
        //[HttpPost("DeleteUser")]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    try
        //    {
        //        ApplicationUser applicationUser = await _userRepository.GetUserById(id);

        //        if (applicationUser is null)

        //        {

        //            return NotFound($"No user found with id {id}");

        //            // return StatusCode(StatusCodes.Status200OK,"No user found with id {id}");
        //        }

        //        IdentityResult result = await _userRepository.DeleteUserById(id);
        //        return Ok(result);
        //    }
        //    catch (Exception x)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }


        //}

        [HttpGet("Users")]
        public UsersViewModel Users()
        {

            //return _adminRepository.GetUsers();

            IEnumerable<UserViewModel> users = _userRepository.GetUsersRoles();
            UsersViewModel usersView = new UsersViewModel
            {
                Users = users
            };
            return usersView;
        }

        [HttpPost("UpdateUsersInRole")]
        public async Task<IActionResult> EditUsersInRole(EditRoleVieModel RoleVieModel)
        {
            try
            {
                var role = await _rolesRepository.GetRoleById(RoleVieModel.Id);

                List<ApiResponse> results = new List<ApiResponse>();
                foreach (var user in RoleVieModel.Users)
                {
                    IdentityResult identityResult = null;
                    try
                    {
                        ApplicationUser applicationUser = await _userRepository.GetUserByUserName(user.UserName);



                        if (user.IsSelected && !(await _userRepository.CheckUserIsMemberofRole(applicationUser, role.Name)))
                        {
                            identityResult = await _userRepository.AddRole(applicationUser, role.Name);
                        }
                        else if (!user.IsSelected && (await _userRepository.CheckUserIsMemberofRole(applicationUser, role.Name)))
                        {
                            identityResult = await _userRepository.RemoveUserRole(applicationUser, role.Name);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (Exception x)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }


                    //results.Add(identityResult);
                }

                return Ok(new ApiResponse()
                {
                    Succeeded = true
                });

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpGet("EditUsersInRole/{Id}")]
        public async Task<IActionResult> EditUsersInRole(string Id)
        {
            try
            {


                var role = await _rolesRepository.GetRoleById(Id);

                var Users = _userRepository.GetUsers();

                List<RoleUsersViewModel> lstRoleUsersViewModel = new List<RoleUsersViewModel>();

                foreach (var user in Users)
                {
                    RoleUsersViewModel roleUsersView = new RoleUsersViewModel();
                    if (await _userRepository.CheckUserIsMemberofRole(user, role.Name))
                    {
                        roleUsersView = new RoleUsersViewModel()
                        {
                            IsSelected = true,
                            UserId = user.Id,
                            UserName = user.UserName
                        };
                    }
                    else
                    {
                        roleUsersView = new RoleUsersViewModel()
                        {
                            IsSelected = false,
                            UserId = user.Id,
                            UserName = user.UserName
                        };
                    }
                    lstRoleUsersViewModel.Add(roleUsersView);
                }

                return Ok(new EditRoleVieModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    Users = lstRoleUsersViewModel
                });



            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }


        [HttpGet("EditUser/{Id}")]
        public async Task<IActionResult> EditUser(string Id)
        {
            ApplicationUser user = await _userRepository.GetUserById(Id);
            List<RoleViewModel> roles = _userRepository.UserSelectedRoles(user);

            UserViewModel viewModel = new UserViewModel()
            {
                City = user.City,
                DateOfBirth = user.DateofBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles,
                UserName=user.UserName,
                PhoneNumber=user.PhoneNumber,
               
            };
            return Ok(viewModel);

        }
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(UserViewModel userView)
        {
            ApiResponse response;
            try
            {
                ApplicationUser user = _userRepository.GetUserByUserName(userView.UserName).Result;

                user.FirstName = userView.FirstName;
                user.LastName = userView.LastName;
                user.City = userView.City;
                user.Email = userView.Email;
                user.DateofBirth = userView.DateOfBirth;
                user.UserName = userView.UserName;
                user.PhoneNumber = userView.PhoneNumber;

                IdentityResult result = await _userRepository.UpdateUser(user);
                if (result.Succeeded) 
                {
                    var SelectedRoles = userView.Roles.Where(x => x.IsSelected).Select(x => x.RoleName).ToArray();
                   
                    var roles = await _userRepository.GetUserRoles(user);
                    result = await _userRepository.RemoveFromRoles(user, roles);
                    result = await _userRepository.AddToRoles(user, SelectedRoles);
                }

                 response = new ApiResponse
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.Select(x => new APIError() { Code = x.Code, Description = x.Description }).ToArray()
                };

                return Ok(response);
            }
            catch(Exception x)
            {
                response = new ApiResponse()
                {
                    Succeeded = false,
                    Errors = new[]
                    {
                       new  APIError
                       {
                           Code=x.Message,
                           Description=x.StackTrace
                       }
                   }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, response);
                
            }

           
        
        }











    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace APIProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
 
    public class AdminController : ControllerBase
    {

        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {

            this._adminRepository = adminRepository;
        }
 
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel RoleModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = RoleModel.RoleName
                };
                IdentityResult identityResult = await _adminRepository.CreateRole(identityRole);
                if (identityResult.Succeeded)
                {
                    return Ok(identityResult);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, identityResult);
                }


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }




        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            try
            {
                IEnumerable<IdentityRole> roles = _adminRepository.GetRoles();
                return Ok(roles);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpGet("EditRole")]
        public async Task<IActionResult> EditRole(string id)
        {
            try
            {
                IdentityRole role = await _adminRepository.FindRoleById(id);
                IEnumerable<string> users = await _adminRepository.GetUsersByRoleName(role.Name);

                EditRoleVieModel roleVieModel = new EditRoleVieModel
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    Users = users
                };

                return Ok(roleVieModel);
            }
            catch (Exception x) 
            {
                return  StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole(EditRoleVieModel roleVieModel)
        {
            if (ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                IdentityRole role = await _adminRepository.FindRoleById(roleVieModel.Id);
                if (role == null) 
                {
                    return Ok("Role not exist...");
                }
               
               role.Name = roleVieModel.RoleName;

               IdentityResult result=   await _adminRepository.EditRole(role);
                //if (result.Succeeded) 
                //{
                //    return Ok(result);

                //}
                //else 
                //{
                //    return Ok("Not able to edit role");
                //}
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("EditUsersInRole/{RoleId}")]
        public async Task<IActionResult> EditUsersInRole(string roleId) 
        {
            try
            {
                IdentityRole role = await _adminRepository.GetRoleById(roleId);
                if (role == null)
                {
                    return Ok("Role not exist...");
                }

                List<RoleUsersViewModel> users = new List<RoleUsersViewModel>();
                _adminRepository.GetUsers().Select(async (x) => new RoleUsersViewModel()
                {
                    IsSelected = await _adminRepository.CheckUserIsMemberofRole(x, role.Name),
                     UserId = x.Id,
                     UserName = x.UserName,
                });
                foreach (var user in _adminRepository.GetUsers())
                {
                    users.Add(
                    new RoleUsersViewModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        IsSelected = await _adminRepository.CheckUserIsMemberofRole(user, role.Name)
                    }
                    );
                }

                return Ok(users);

            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpPost("UpdateUsersInRole/{roleId}")]
        public async Task<IActionResult> EditUsersInRole(List<RoleUsersViewModel> users, string roleId) 
        {
            try
            {
                var role = await _adminRepository.GetRoleById(roleId);
                List<IdentityResult> results = new List<IdentityResult>();
                foreach (var user in users)
                {
                    IdentityResult identityResult = null;
                    try
                    {
                        ApplicationUser applicationUser = await _adminRepository.GetUserByUserName(user.UserName);



                        if (user.IsSelected && !(await _adminRepository.CheckUserIsMemberofRole(applicationUser, role.Name)))
                        {
                            identityResult = await _adminRepository.AddRole(applicationUser, role.Name);
                        }
                        else if (!(user.IsSelected && !(await _adminRepository.CheckUserIsMemberofRole(applicationUser, role.Name))))
                        {
                            identityResult = await _adminRepository.RemoveRole(applicationUser, role.Name);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (Exception x)
                    {
                        results.Add(identityResult);
                    }

                    results.Add(identityResult);
                }

                return Ok(results);

            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
          

        }

        [HttpGet("Users")]
        public  List<ApplicationUser> Users() 
        {
            return _adminRepository.GetUsers();
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                ApplicationUser applicationUser = await _adminRepository.GetUserById(id);

                if (applicationUser is null)

                {

                    return NotFound($"No user found with id {id}");
                   
                   // return StatusCode(StatusCodes.Status200OK,"No user found with id {id}");
                }

                IdentityResult result = await _adminRepository.DeleteUserById(id);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string id) 
        {
            try 
            {
                IdentityRole role = await _adminRepository.GetRoleById(id);
                if (role is null)
                {
                    return NotFound($"No user found with id {id}");
                }
                IdentityResult result = await _adminRepository.DeleteRoleById(id);
                return Ok(result);
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        ///sadsadsasad








    }
}
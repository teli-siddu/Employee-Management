using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IUserRepository _userRepository;

        public RolesController(IRolesRepository rolesRepository,IUserRepository userRepository)
        {
            this._rolesRepository = rolesRepository;
            this._userRepository = userRepository;
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
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = RoleModel.RoleName
                };
                IdentityResult identityResult = await _rolesRepository.AddRole(applicationRole);
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
        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole(CreateRoleViewModel roleVieModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ApplicationRole role = await _rolesRepository.FindRoleById(roleVieModel.RoleId);
                if (role == null)
                {
                    return Ok("Role not exist...");
                }

                role.Name = roleVieModel.RoleName;

                IdentityResult result = await _rolesRepository.EditRole(role);
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


        [HttpGet("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                ApplicationRole role = await _rolesRepository.GetRoleById(id);
                if (role is null)
                {
                    return NotFound($"No user found with id {id}");
                }
                IdentityResult result = await _rolesRepository.DeleteRoleById(id);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                List<RoleViewModel> roles = await _rolesRepository.GetRoles();
                return Ok(roles);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpGet("EditRole/{Id}")]
        public async Task<IActionResult> EditRole(int Id)
        {
            try
            {
                ApplicationRole role = await _rolesRepository.FindRoleById(Id);
                IEnumerable<string> users = await _userRepository.GetUsersByRoleName(role.Name);

                CreateRoleViewModel roleVieModel = new CreateRoleViewModel
                {

                    RoleName = role.Name,
                    RoleId = role.Id

                };

                return Ok(roleVieModel);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using APIProject.Models;
using Contracts;
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
        private IRepositoryWrapper _repositoryWrapper;
        private AppSettings _appSettings;
        private UserManager<ApplicationUser> _userManager;
        public UsersController(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            //_repositoryWrapper = repositoryWrapper;
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user =  await _userRepository.Authenticate(model.UserName, model.Password);

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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterViewModel user) 
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
                    UserName = user.Email
                };
                IdentityResult result = await _userRepository.Register(applicationUser, user.Password);
               return Ok(result);
              
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);         
            }
        }
    }
}
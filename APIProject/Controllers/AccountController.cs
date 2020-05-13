using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]LoginViewModel loginViewModel) 
        {
           Microsoft.AspNetCore.Identity.SignInResult signInResult= await _accountRepository.SignIn(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe);

            if (signInResult.Succeeded) 
            {
                var apiResponse = new ApiResponse(signInResult.Succeeded, new string[] { });
              
                return Ok(apiResponse);
            }
            else 
            {
                return BadRequest("Invalid user name or password");
            }
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            try
            {

                await _accountRepository.SignOut();
                return Ok();
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {

            var token= await _accountRepository.GetSecurityToken(model.UserName, model.Password);
            if (token == null) 
            {
                return BadRequest("invalid user name or password");
            }
            return Ok(token);
           

        }

    }
}

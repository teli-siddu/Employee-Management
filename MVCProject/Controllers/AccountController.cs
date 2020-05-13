using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
          

            
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public  IActionResult Login(LoginViewModel loginView) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            var json = JsonConvert.SerializeObject(loginView);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "Application/json");
            HttpResponseMessage response=  httpClient.PostAsync ("api/Account/Authenticate", stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var x = JsonConvert.DeserializeObject<UserViewModel>(jsonData);
             
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(x.Token);
                var tokenS = handler.ReadToken(x.Token) as JwtSecurityToken;

                var xx= tokenS.Claims.First(x => x.Type == "UserMenu").Value;
                HttpContext.Session.SetString("token",x.Token);
                return RedirectToAction("index","home");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel user)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "Application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Users/Register", stringContent).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var x = JsonConvert.DeserializeObject(jsonData);
            return Ok(x);
          
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



            public IActionResult LogOut() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
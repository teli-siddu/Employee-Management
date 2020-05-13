using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
//using MVCProject.ViewModels;
using Entities.ViewModels;
namespace MVCProject.Controllers
{
  
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {

            return View();
        }


        public IActionResult Users() 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Admin/users").Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            UsersViewModel usersView = JsonConvert.DeserializeObject<UsersViewModel>(jsonResult);

            return View(usersView);
        }
        [HttpGet]
        public IActionResult Roles()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Admin/GetRoles").Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonResult);
            return View(jsonResonse);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();

        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel roleViewModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string strRoleViewModel = JsonConvert.SerializeObject(roleViewModel);
            StringContent content = new StringContent(strRoleViewModel, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Admin/CreateRole", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject(jsonResult);
            return RedirectToAction("Roles");

        }


        //[HttpGet("EditRole/{Id}")]
        [HttpGet]
        //[Route("{Id}")]
        public IActionResult EditRole(string Id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Admin/EditUsersInRole/" + Id).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<EditRoleVieModel>(jsonResult);

            return View(jsonResonse);
        }

        [HttpPost("Admin/EditRole")]
        public IActionResult EditRole(EditRoleVieModel RoleVieModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string strCreateRoleView = JsonConvert.SerializeObject(RoleVieModel);
            StringContent content = new StringContent(strCreateRoleView, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Admin/UpdateUsersInRole", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ApiResponse>(jsonResult);
            
            
            
                return RedirectToAction("Roles");
            

           
        }


        [HttpGet("{Id}")]
        public IActionResult DeleteRole(string Id) 
        
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Admin/DeleteRole/" + Id).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ApiResponse>(jsonResult);
            if (jsonResonse.Succeeded) 
            {
                return RedirectToAction("roles");
            }
            foreach(var error in jsonResonse.Errors) 
            {
                ModelState.AddModelError("Error", error.Description);
            }
          
            return View("Roles");
        }

        public IActionResult EditUser(string Id) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
           
          
            HttpResponseMessage response = httpClient.GetAsync("api/Admin/EditUser").Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<UserRoleViewModel>(jsonResult);
            return RedirectToAction("Roles");
        }
    }
}
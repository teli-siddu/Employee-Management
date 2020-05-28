using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.ViewModels;
using Entities.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        
        public IActionResult Index()
        {
            return View();
        }

  
        [HttpGet]
        public IActionResult EditUser(string Id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Users/EditUser/"+Id).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var jsonResonse = JsonConvert.DeserializeObject<AddUserViewModel>(jsonResult);
            return View("AddUser",jsonResonse);
        }

   
        [HttpPost]
        public IActionResult EditUser(AddUserViewModel userView) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string jsonString= JsonConvert.SerializeObject(userView);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "Application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Users/EditUser",stringContent).Result;
            
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;
                return StatusCode(statusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var jsonResonse = JsonConvert.DeserializeObject<Entities.Helper.ApiResponse>(jsonResult);
            return RedirectToAction("GetUsers");
        }

        public IActionResult GetUsers()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Users/users").Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            UsersViewModel usersView = JsonConvert.DeserializeObject<UsersViewModel>(jsonResult);

            return View(usersView);
        }

        public async Task<IActionResult> DeleteUser(string Id) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
          
           
            HttpResponseMessage response = await httpClient.GetAsync("api/Users/DeleteUser/"+Id);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;
                return StatusCode(statusCode);
            }
            return RedirectToAction("GetUsers");
        }

        public IActionResult AddUser(AddUserViewModel  user)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "Application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Users/AddUser", stringContent).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var x = JsonConvert.DeserializeObject(jsonData);
            return RedirectToAction("Getusers");

        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            

             HttpResponseMessage response =await httpClient.GetAsync("api/Roles/GetRoles");
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonData);

             response = await httpClient.GetAsync("api/Dropdowns/Deapartments");
             jsonData = response.Content.ReadAsStringAsync().Result;
            var departments = JsonConvert.DeserializeObject<List<KeyValue<int,string>>>(jsonData);

            AddUserViewModel userViewModel = new AddUserViewModel();

            userViewModel.Roles = roles;
            userViewModel.departments = departments;

            return View(userViewModel);
        }

        //public IActionResult UserDetails(string Id) 
        //{

        //}




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    public class RolesController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public RolesController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetRoles()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Roles/GetRoles").Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonResult);
            return View(jsonResonse);
        }

        [HttpGet]
        public IActionResult AddRole()
        {

            return View();

        }

        [HttpPost]
        public IActionResult AddRole(CreateRoleViewModel roleViewModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string strRoleViewModel = JsonConvert.SerializeObject(roleViewModel);
            StringContent content = new StringContent(strRoleViewModel, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Roles/CreateRole", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject(jsonResult);
            return RedirectToAction("GetRoles");

        }


        //[HttpGet("EditRole/{Id}")]
        [HttpGet]
        //[Route("{Id}")]
        public IActionResult EditRole(string Id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Users/EditUsersInRole/" + Id).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<EditRoleVieModel>(jsonResult);

            return View(jsonResonse);
        }

        [HttpPost("Roles/EditRole")]
        public IActionResult EditRole(EditRoleVieModel RoleVieModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string strCreateRoleView = JsonConvert.SerializeObject(RoleVieModel);
            StringContent content = new StringContent(strCreateRoleView, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/Users/UpdateUsersInRole", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ApiResponse>(jsonResult);



            return RedirectToAction("GetRoles");



        }


        [HttpGet]
        public IActionResult DeleteRole(string Id)

        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Roles/DeleteRole/" + Id).Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ApiResponse>(jsonResult);
            if (jsonResonse.Succeeded)
            {
                return RedirectToAction("Getroles");
            }
            foreach (var error in jsonResonse.Errors)
            {
                ModelState.AddModelError("Error", error.Description);
            }

            return View("GetRoles");
        }

    }
}
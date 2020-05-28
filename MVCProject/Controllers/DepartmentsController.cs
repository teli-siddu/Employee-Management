using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public DepartmentsController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Departments() 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = await httpClient.GetAsync("api/Departments/Departments");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(jsonResult);
            return View(jsonResonse);
        }

        public IActionResult AddDepartment() 
        {
            return View();
             
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentViewModel department)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string strDepartment = JsonConvert.SerializeObject(department);
            StringContent content = new StringContent(strDepartment, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Departments/AddDepartment",content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
            if (jsonResonse.Succeeded)
            {
                return RedirectToAction("Departments");
            }
            else 
            {
                return View(department);
            }
           

        }

        public async Task<IActionResult> UpdateDepartment(int Id) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
           
           
            HttpResponseMessage response = await httpClient.GetAsync("api/Departments/GetDepartmentById/"+Id);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<DepartmentViewModel>(jsonResult);
            return View("AddDepartment",jsonResonse);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(DepartmentViewModel department)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string strDepartment = JsonConvert.SerializeObject(department);
            StringContent content = new StringContent(strDepartment, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Departments/UpdateDepartment", content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
            return RedirectToAction ("Departments");

        }

        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());


            HttpResponseMessage response = await httpClient.GetAsync("api/Departments/DeleteDepartment/" + Id);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
            return RedirectToAction("Departments");
        }


    }
}
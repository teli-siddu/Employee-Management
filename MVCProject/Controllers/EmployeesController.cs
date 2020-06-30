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
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
  
    public class EmployeesController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeesController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public  async Task<IActionResult> Employees()
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                HttpResponseMessage response = await httpClient.GetAsync("api/Employees/Employees");
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(jsonResult);
                return View(jsonResonse);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                HttpResponseMessage response = await httpClient.GetAsync("api/Dropdowns/GetAddEmployeeDropdowns");
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<AddEmployeeDropdowns>(jsonResult);

                HttpResponseMessage response1 = await httpClient.GetAsync("api/Roles/GetRoles");
                string jsonData = response1.Content.ReadAsStringAsync().Result;
                var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonData);
                //ViewBag.AddEmployeeDropdowns = jsonResonse;

                AddEmployeeViewModel addEmployeeView = new AddEmployeeViewModel
                {
                    Deapartments = jsonResonse.Departments,
                    Countries = jsonResonse.Countries,
                    MaritialStatuses = jsonResonse.MaritialStatuses,
                    Genders = jsonResonse.Genders,
                    Nationalities = jsonResonse.Nationalities,
                    Roles=roles
                };

                return View(addEmployeeView);


            }
            catch (Exception x)
            {

            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel employee)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                string strJson = JsonConvert.SerializeObject(employee);
                StringContent stringContent = new StringContent(strJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response =  httpClient.PostAsync("api/Employees/AddEmployee",stringContent).Result;
                if (!response.IsSuccessStatusCode && !((int)response.StatusCode==400))
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
                if (jsonResonse.Succeeded) 
                {
                    return RedirectToAction("Employees");
                }


                 
                response = await httpClient.GetAsync("api/Dropdowns/GetAddEmployeeDropdowns");
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                 jsonResult = response.Content.ReadAsStringAsync().Result;

                AddEmployeeDropdowns jsonResonseAddEmployeeDropdowns = JsonConvert.DeserializeObject<AddEmployeeDropdowns>(jsonResult);

                HttpResponseMessage response1 = await httpClient.GetAsync("api/Roles/GetRoles");
                string jsonData = response1.Content.ReadAsStringAsync().Result;
                var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonData);
                //ViewBag.AddEmployeeDropdowns = jsonResonse;

                employee.Deapartments = jsonResonseAddEmployeeDropdowns.Departments;
                employee.Countries = jsonResonseAddEmployeeDropdowns.Countries;
                employee.MaritialStatuses = jsonResonseAddEmployeeDropdowns.MaritialStatuses;
                employee.Genders = jsonResonseAddEmployeeDropdowns.Genders;
                employee.Nationalities = jsonResonseAddEmployeeDropdowns.Nationalities;
                employee.Roles = roles;
                


                return View(employee);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                HttpResponseMessage response = await httpClient.GetAsync("api/Employees/EditEmployee/"+id);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<AddEmployeeViewModel>(jsonResult);



                //string[] emails = jsonResonse.Emails.Where(x=>x!=null).ToArray();
                //string[] mobiles = jsonResonse.Mobiles.Where(x => x != null).ToArray();

                //jsonResonse.Emails = new string[2];
                //jsonResonse.Mobiles = new string[2];
                //emails.CopyTo(jsonResonse.Emails, 0);
                //mobiles.CopyTo(jsonResonse.Mobiles, 0);

                //List<MobileViewModel> mobiles = new List<MobileViewModel>(2);
                //List<EmailViewModel> emails = new List<EmailViewModel>(2);
                //List<AddAddressViewModel> addresses = new List<AddAddressViewModel>(2);
                // mobiles.AddRange(jsonResonse.Mobiles);
                // emails.AddRange(jsonResonse.Emails);
                // addresses.AddRange(jsonResonse.Addresses);
                //jsonResonse.Mobiles = mobiles;
                //jsonResonse.Emails = emails;
                //jsonResonse.Addresses = addresses;



                return View("AddEmployee",jsonResonse);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }

        public async Task<IActionResult> EditEmployee(AddEmployeeViewModel addEmployeeView)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                string strJson = JsonConvert.SerializeObject(addEmployeeView);
                StringContent stringContent = new StringContent(strJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync("api/Employees/Updatemployee", stringContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
                if (jsonResonse.Succeeded)
                {
                    return RedirectToAction("Employees");
                }
                return View(addEmployeeView);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                HttpResponseMessage response = await httpClient.DeleteAsync("api/Employees/DeleteEmployee/"+Id);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
                return RedirectToAction("Employees");

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
                HttpResponseMessage response = await httpClient.GetAsync("api/Employees/EmployeeDetails/"+id);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }
                var jsonResult = response.Content.ReadAsStringAsync().Result;

                var jsonResonse = JsonConvert.DeserializeObject<EmployeeViewModel>(jsonResult);
                return View(jsonResonse);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }
    }
}
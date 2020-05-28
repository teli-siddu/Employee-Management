using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.ViewModels.Leave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public LeavesController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            
            this._logger = logger;
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Leaves() 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Leaves/Leaves").Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<List<LeaveViewModel>>(jsonResult);
            return View(jsonResonse);
        }

        [HttpGet]
        public IActionResult AddLeave()
        {

            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = httpClient.GetAsync("api/Dropdowns/LeaveTypes").Result;
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(jsonResult);
            AddLeaveViewModel addLeaveView = new AddLeaveViewModel
            {
                LeaveTypes = jsonResonse
            };
            return View(addLeaveView);
        }

        [HttpPost]
        public IActionResult AddLeave(AddLeaveViewModel addLeaveView)
        {

            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());

            string strJson = JsonConvert.SerializeObject(addLeaveView);

            StringContent stringContent = new StringContent(strJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PostAsync("api/Leaves/AddLeave",stringContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
          
            return RedirectToAction("Leaves");
        }

        [HttpPut]
        public IActionResult UpdateLeave(AddLeaveViewModel addLeaveView)
        {

            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());

            string strJson = JsonConvert.SerializeObject(addLeaveView);

            StringContent stringContent = new StringContent(strJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PutAsync("api/Leaves/UpdateLeave", stringContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);

            return RedirectToAction("Leaves");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());

            HttpResponseMessage response = httpClient.DeleteAsync("api/Leaves/UpdateLeave/"+id).Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);

            return RedirectToAction("Leaves");
        }
    }
}
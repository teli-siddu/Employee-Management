using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCProject.Models;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response=  httpClient.GetAsync("api/Admin/users").Result;
           
            if (!response.IsSuccessStatusCode) 
            {
                return StatusCode((int)response.StatusCode);
            }
            var jsonResult= response.Content.ReadAsStringAsync().Result;
            UsersViewModel usersView = JsonConvert.DeserializeObject<UsersViewModel>(jsonResult);
           
           return View(usersView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

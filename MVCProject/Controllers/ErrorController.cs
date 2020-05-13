using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ErrorController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int code) 
        {
            ViewBag.StatusCode = code;
            return View();
        }
    }
}
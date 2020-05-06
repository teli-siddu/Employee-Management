using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
      
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;

        }


        [Authorize]
        [HttpGet]
        public async  Task<IEnumerable<WeatherForecast>>  Get()
        {
           
            _logger.LogTrace("here is logtrace message from the controller.");
            _logger.LogDebug("here is debug message from the controller.");
            _logger.LogInformation("here is info message from the controller.");
            _logger.LogWarning("here is warning message from the controller.");
            _logger.LogError("here is error message from the controller.");
            _logger.LogCritical("here is critical message from the controller.");
            //IEnumerable<User> owners= await  _repositoryWrapper.User.GetAllUsersAsync();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

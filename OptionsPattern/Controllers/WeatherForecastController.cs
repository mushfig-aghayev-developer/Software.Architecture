using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionsPattern.Config;

namespace OptionsPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AccountServiceConfig accountServiceConfig;
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];



        ///<summary>
        ///For the Singleton service injection via IOptionsMonitor pattern
        ///</summary>
        /*
           public WeatherForecastController(IOptionsMonitor<AccountServiceConfig> accountServiceConfiguration, ILogger<WeatherForecastController> logger)
          {
              accountServiceConfig = accountServiceConfiguration.CurrentValue;
              _logger = logger;
          }
         */


        /// <summary>
        /// Singleton service injection via IOptions pattern
        /// </summary>
        /*
         public WeatherForecastController(IOptions<AccountServiceConfig> accountServiceConfiguration, ILogger<WeatherForecastController> logger)
         {
             accountServiceConfig = accountServiceConfiguration.Value;
             _logger = logger;
         }
        */


        /// <summary>
        /// If We need Change Configuration after application started we can use IOptionsSnapshot
        /// </summary>
        public WeatherForecastController(IOptionsSnapshot<AccountServiceConfig> accountServiceConfiguration, ILogger<WeatherForecastController> logger)
        {
            accountServiceConfig = accountServiceConfiguration.Value;
            _logger = logger;
        }

        [HttpGet("/")]
        public IEnumerable<WeatherForecast> Get()
        {
            var data = $"Url: {accountServiceConfig.Url}, UserName: {accountServiceConfig.UserName}, Password: {accountServiceConfig.Password}";

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

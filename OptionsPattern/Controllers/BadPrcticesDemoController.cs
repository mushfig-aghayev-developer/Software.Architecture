//using Microsoft.AspNetCore.Mvc;

//namespace OptionsPattern.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class BadPrcticesDemoController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<WeatherForecastController> _logger;

//        private static readonly string[] Summaries =
//        [
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        ];


//        public BadPrcticesDemoController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
//        {
//            _logger = logger;
//            _configuration = configuration;
//        }

//        [HttpGet("/")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            string? url = _configuration.GetSection("AccountSettings")["url"];

//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}

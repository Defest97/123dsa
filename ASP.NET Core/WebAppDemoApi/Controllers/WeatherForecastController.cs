using Microsoft.AspNetCore.Mvc;
using WebAppDemoApi.Services;

namespace WebAppDemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly FileStoreService _fileStoreService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,FileStoreService fileStoreService)
        {
            _logger = logger;
            _fileStoreService = fileStoreService;
        }
        // /weatherforecast/erer
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("File")]
        public string GetFile([FromHeader]string? fileName)
        {
            if (fileName == null)
            {
                return _fileStoreService.ReadFile("demo.txt");
            }
            else
            return _fileStoreService.ReadFile(fileName);
        }
        
        [HttpPost("File")]
        public void PostFile(string fileName,string text)
        {
            _fileStoreService.SaveFile(fileName,text);
        }



    }
}
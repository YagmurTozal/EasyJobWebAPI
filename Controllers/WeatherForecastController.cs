using Microsoft.AspNetCore.Mvc;
using EasyJobWebAPI.Model.WeatherAPIModel;

namespace EasyJobWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()

    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "GetForecastWithID")]

    public WeatherForecast? GetByID(int ID)
    {
        if (ID<0)
            return null;

        
        var forecast = new WeatherForecast()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(ID)),
            Summary = Summaries[ID],
            TemperatureC = Random.Shared.Next(-10, 15)
        };

        return forecast;
    }

    
}

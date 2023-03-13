namespace GoogleAuthService.Controllers
{
  //using Google.Apis.Auth.AspNetCore3;
  //using Google.Apis.Services;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
  [Route("[controller]")]
  public class AuthWeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<AuthWeatherForecastController> _logger;

    public AuthWeatherForecastController(ILogger<AuthWeatherForecastController> logger)
    {
      _logger = logger;
    }

    //https://www.googleapis.com/auth/userinfo.email
    [HttpGet]
    //[GoogleScopedAuthorize("https://www.googleapis.com/auth/userinfo.email")]
    //[GoogleScopedAuthorize(Google.Apis.PeopleService.v1.PeopleServiceService.ScopeConstants.UserinfoEmail)]
    [Authorize]
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
  }
}
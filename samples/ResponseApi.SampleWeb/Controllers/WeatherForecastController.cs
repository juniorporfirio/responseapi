using JuniorPorfirio.ResponseApi.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResponseApi.SampleWeb.Services;
using System.Collections.Generic;

namespace ResponseApi.SampleWeb.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{


		private readonly ILogger<WeatherForecastController> _logger;
		private readonly WeatherForecastService _service;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService service)
		{
			_logger = logger;
			_service = service;
		}

		[HttpGet]
		public ActionResult<IEnumerable<WeatherForecast>> Get() =>
		    this.ToActionResult(_service.GetWeather());
	}
}

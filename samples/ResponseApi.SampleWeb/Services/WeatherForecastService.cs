using JuniorPorfirio.ResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResponseApi.SampleWeb.Services
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public ResponseApi<IEnumerable<WeatherForecast>> GetWeather()
        {
            var rng = new Random();
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return ResponseApi<IEnumerable<WeatherForecast>>
                .Against(data)
                .IsAny();
        }
    }
}

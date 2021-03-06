using System;
using System.Linq;
using System.Threading.Tasks;
using Reactive_Fluxor.Shared;

namespace Reactive_Fluxor.Services
{
	public interface IWeatherForecastService
	{
		Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
	}

	public class WeatherForecastService : IWeatherForecastService
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
		{
			await Task.Delay(1000);
			var rng = new Random();
			return Enumerable.Range(1, 2).Select(index => new WeatherForecast
				{
					Date = startDate.AddDays(index),
					TemperatureC = rng.Next(-20, 55),
					Summary = Summaries[rng.Next(Summaries.Length)]
				})
				.ToArray();
		}
	}
}

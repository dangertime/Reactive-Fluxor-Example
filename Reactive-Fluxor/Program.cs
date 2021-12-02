using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using CounterEventStream;
using Reactive_Fluxor.Services;

namespace Reactive_Fluxor
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();
			services.AddScoped<App>();
			services.AddScoped<IWeatherForecastService, WeatherForecastService>();
			services.AddSingleton(new CounterEventStream.CounterEventStream());
			services.AddFluxor(o => o
				.ScanAssemblies(typeof(Program).Assembly));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			var app = serviceProvider.GetRequiredService<App>();
			app.Run();
		}
	}
}

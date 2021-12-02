using Fluxor;
using System;
using System.Linq;
using Reactive_Fluxor.Client.Store.WeatherUseCase;
using Reactive_Fluxor.Shared;
using Reactive_Fluxor.Store.CounterUseCase;

namespace Reactive_Fluxor
{
	public class App
	{
		private readonly IStore Store;
		public readonly IDispatcher Dispatcher;
		public readonly IState<CounterState> CounterState;
		private readonly IState<WeatherState> WeatherState;

		public App(IDispatcher dispatcher, IStore store, IState<CounterState> counterState, IState<WeatherState> weatherState)
		{
			Store = store;
			Dispatcher = dispatcher;
			CounterState = counterState;
			CounterState.StateChanged += CounterState_StateChanged;
			WeatherState = weatherState;
			WeatherState.StateChanged += WeatherState_StateChanged;
		}

		private void CounterState_StateChanged(object sender, CounterState e)
		{
			Console.WriteLine("");
			Console.WriteLine("==========================> CounterState");
			Console.WriteLine("ClickCount is " + CounterState.Value.ClickCount);
			Console.WriteLine("<========================== CounterState");
			Console.WriteLine("");
		}

		private void WeatherState_StateChanged(object sender, WeatherState e)
		{
			Console.WriteLine("");
			Console.WriteLine("=========================> WeatherState");
			Console.WriteLine("IsLoading: " + WeatherState.Value.IsLoading);
			if (!WeatherState.Value.Forecasts.Any())
			{
				Console.WriteLine("--- No weather forecasts");
			}
			else
			{
				Console.WriteLine("Temp C\tTemp F\tSummary");
				foreach (WeatherForecast forecast in WeatherState.Value.Forecasts)
					Console.WriteLine($"{forecast.TemperatureC}\t{forecast.TemperatureF}\t{forecast.Summary}");
			}
			Console.WriteLine("<========================== WeatherState");
			Console.WriteLine("");
		}

		public void Run()
		{
			Console.Clear();
			Console.WriteLine("Initializing store");
			Store.InitializeAsync().Wait();
			string input = "";
			do
			{
				Console.WriteLine("1: Increment counter");
				Console.WriteLine("2: Fetch data");
				Console.WriteLine("3: Subscribe to counter stream");
				Console.WriteLine("4: Unsubscribe to counter stream");
				Console.WriteLine("x: Exit");
				Console.Write("> ");
				input = Console.ReadLine();

				switch(input.ToLowerInvariant())
				{
					case "1":
						var incrementCounterAction = new IncrementCounterAction(){Amount = 1};
						Dispatcher.Dispatch(incrementCounterAction);
						break;
					
					case "2":
						var fetchDataAction = new FetchDataAction();
						Dispatcher.Dispatch(fetchDataAction);
						break;
					case "3":
						Dispatcher.Dispatch(new SubscribeToCounterEventStreamAction());
						break;
					case "4":
						Dispatcher.Dispatch(new UnSubscribeToCounterEventStreamAction());
						break;
					case "x":
						Console.WriteLine("Program terminated");
						return;
				}

			} while (true);
		}
	}
}

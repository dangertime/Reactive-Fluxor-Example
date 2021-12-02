using Fluxor;
using Reactive_Fluxor.Client.Store.WeatherUseCase;
using Reactive_Fluxor.Services;

namespace Reactive_Fluxor.Store.CounterUseCase;

public class Effects
{
    private readonly CounterEventStream.CounterEventStream _counterEventStream;
    private IDisposable subscription;

    public Effects(CounterEventStream.CounterEventStream eventStream)
    {
        _counterEventStream = eventStream;
    }

    [EffectMethod]
    public async Task HandleSubscribeAction(SubscribeToCounterEventStreamAction action, IDispatcher dispatcher)
    {
        subscription = _counterEventStream.CounterIncrementAmount.Subscribe( e =>
        {
            var counterAction = new IncrementCounterAction() {Amount = e};
            dispatcher.Dispatch(counterAction);
        });
    }

    [EffectMethod]
    public async Task HandleUnsubscribeAction(UnSubscribeToCounterEventStreamAction action, IDispatcher _)
    {
        subscription.Dispose();
    }
}
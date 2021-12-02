//
// using Fluxor;
// using Reactive_Fluxor.Store.CounterUseCase;
//
// namespace Reactive_Fluxor.Services
// {
//     public interface IReactiveCounterService
//     {
//     }
//     
//     public class ReactiveCounterService : IReactiveCounterService, IDisposable
//     {
//         private readonly CounterEventStream.CounterEventStream _eventStream;
//         private readonly IDispatcher _dispatcher;
//         private readonly IDisposable _eventSubscription;
//
//         public ReactiveCounterService(CounterEventStream.CounterEventStream eventStream, IDispatcher dispatcher)
//         {
//             _eventStream = eventStream;
//             _dispatcher = dispatcher;
//             _eventSubscription = SubscribeToCounterEvents(eventStream);
//         }
//
//         private IDisposable SubscribeToCounterEvents(CounterEventStream.CounterEventStream eventStream)
//         {
//             return eventStream.CounterIncrementAmount.Subscribe( e =>
//             {
//                 var counterAction = new IncrementCounterAction() {Amount = e};
//                 _dispatcher.Dispatch(counterAction);
//             });
//         }
//
//         public void Dispose()
//         {
//             _eventSubscription.Dispose();
//         }
//     }
// }
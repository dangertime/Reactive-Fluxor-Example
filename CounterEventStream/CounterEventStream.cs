using System.Reactive.Subjects;

namespace CounterEventStream;

public class CounterEventStream
{
    public BehaviorSubject<int> CounterIncrementAmount { get; }

    public CounterEventStream()
    {
        CounterIncrementAmount = new BehaviorSubject<int>(0);
        GenerateEvents();
    }

    private void GenerateEvents()
    {
        // start a task that generates an event stream to increment a counter by 1
        Task.Run(() => {
            while (true)
            {
                CounterIncrementAmount.OnNext(1);
                Thread.Sleep(5000);
            }
        });
    }
}
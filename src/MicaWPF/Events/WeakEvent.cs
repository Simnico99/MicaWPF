using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Events;
internal class WeakEvent<T> : IWeakEvent<T>
{
    protected readonly object _locker = new();
    protected readonly List<(Type EventType, Delegate MethodToCall)> _eventRegistrations = new();

    public virtual ISubscription Subscribe(Action<T> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        _eventRegistrations.Add((typeof(T), action));

        return new Subscription(() =>
        {
            lock (_locker)
            {
                _eventRegistrations.Remove((typeof(T), action));
            }
        });
    }

    public virtual ConfiguredTaskAwaitable PublishAsync(T data, bool configureAwait = false)
    {
        return Task.Run(() =>
        {
            lock (_locker)
            {
                foreach (var (EventType, MethodToCall) in _eventRegistrations)
                {
                    if (EventType == typeof(T))
                    {
                        ((Action<T>)MethodToCall)(data);
                    }
                }
            }
        }).ConfigureAwait(configureAwait);
    }

    public void Publish(T data)
    {
        _ = Task.Run(() =>
        {
            lock (_locker)
            {
                foreach (var (EventType, MethodToCall) in _eventRegistrations)
                {
                    if (EventType == typeof(T))
                    {
                        ((Action<T>)MethodToCall)(data);
                    }
                }
            }
        });
    }
}

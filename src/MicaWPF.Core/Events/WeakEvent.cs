// <copyright file="WeakEvent.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Runtime.CompilerServices;

namespace MicaWPF.Core.Events;

/// <summary>
/// Represents a weak event that is less prone to memory leaks.
/// </summary>
/// <typeparam name="T">The type of the argument that will be passed to the <see cref="ISubscription"/>.</typeparam>
internal sealed class WeakEvent<T> : IWeakEvent<T>
{
    private readonly object _locker = new();
    private readonly List<(Type EventType, Delegate MethodToCall)> _eventRegistrations = new();

    /// <inheritdoc/>
    public ISubscription Subscribe(Action<T> action)
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
                _ = _eventRegistrations.Remove((typeof(T), action));
            }
        });
    }

    /// <inheritdoc/>
    public ConfiguredTaskAwaitable PublishAsync(T data, bool configureAwait = false)
    {
        return Task.Run(() =>
        {
            lock (_locker)
            {
                foreach (var (eventType, methodToCall) in _eventRegistrations)
                {
                    if (eventType == typeof(T))
                    {
                        ((Action<T>)methodToCall)(data);
                    }
                }
            }
        }).ConfigureAwait(configureAwait);
    }

    /// <inheritdoc/>
    public void Publish(T data)
    {
        _ = Task.Run(() =>
        {
            lock (_locker)
            {
                foreach (var (eventType, methodToCall) in _eventRegistrations)
                {
                    if (eventType == typeof(T))
                    {
                        ((Action<T>)methodToCall)(data);
                    }
                }
            }
        });
    }
}

// <copyright file="IWeakEvent.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Runtime.CompilerServices;

namespace MicaWPF.Core.Events;

/// <summary>
/// Represents a weak event that is less prone to memory leaks.
/// </summary>
/// <typeparam name="T">The type of the argument that will be passed to the <see cref="ISubscription"/>.</typeparam>
public interface IWeakEvent<T>
{
    /// <summary>
    /// Asynchronously publishes the specified data to all subscribers.
    /// </summary>
    /// <param name="data">The data to publish.</param>
    /// <param name="awaitAllCalls">If set to true, this method will not complete until all event handlers have been invoked. Default is false.</param>
    /// <returns>A <see cref="ConfiguredTaskAwaitable"/> that can be awaited until all event handlers have completed.</returns>
    ConfiguredTaskAwaitable PublishAsync(T data, bool awaitAllCalls = false);

    /// <summary>
    /// Publishes the specified data to all subscribers.
    /// </summary>
    /// <param name="data">The data to publish.</param>
    void Publish(T data);

    /// <summary>
    /// Subscribes the specified action to this event.
    /// </summary>
    /// <param name="action">The action to be executed when the event is published.</param>
    /// <returns>An <see cref="ISubscription"/> which can be used to unsubscribe the action from the event.</returns>
    ISubscription Subscribe(Action<T> action);
}
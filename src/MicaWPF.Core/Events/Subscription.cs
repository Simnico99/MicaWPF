// <copyright file="Subscription.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Events;

/// <summary>
/// The action to perform when this subscription is disposed.
/// </summary>
internal sealed class Subscription : ISubscription
{
    private readonly Action _removeMethod;

    /// <summary>
    /// Initializes a new instance of the <see cref="Subscription"/> class.
    /// </summary>
    /// <param name="removeMethod">The action to perform when this subscription is disposed.</param>
    public Subscription(Action removeMethod)
    {
        _removeMethod = removeMethod;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_removeMethod is not null)
        {
            _removeMethod();
        }
    }
}

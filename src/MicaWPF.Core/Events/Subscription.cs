// <copyright file="Subscription.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF.Core.Events;

internal sealed class Subscription : ISubscription
{
    private readonly Action _removeMethod;

    public Subscription(Action removeMethod)
    {
        _removeMethod = removeMethod;
    }

    public void Dispose()
    {
        if (_removeMethod is not null)
        {
            _removeMethod();
        }
    }
}

// <copyright file="ISubscription.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Events;

/// <summary>
/// A IWeakEvent subscription.
/// </summary>
public interface ISubscription
{
    void Dispose();
}

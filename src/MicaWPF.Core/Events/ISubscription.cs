// <copyright file="ISubscription.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Events;

/// <summary>
/// A IWeakEvent subscription.
/// </summary>
public interface ISubscription
{
    /// <summary>
    /// Dispose the Subsription.
    /// </summary>
    void Dispose();
}

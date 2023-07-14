// <copyright file="EventIdentifier.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Models;

/// <summary>
/// Represents an event identifier with a unique timestamp-based value.
/// </summary>
public sealed class EventIdentifier
{
    /// <summary>
    /// Gets the current event identifier value.
    /// </summary>
    public long Current { get; internal set; } = 0;

    /// <summary>
    /// Updates the event identifier and returns the updated value.
    /// The updated value is based on the current date and time.
    /// </summary>
    /// <returns>The updated event identifier value.</returns>
    public long GetNext()
    {
        UpdateIdentifier();

        return Current;
    }

    /// <summary>
    /// Checks if the provided identifier is equal to the current identifier.
    /// </summary>
    /// <param name="storedId">The identifier to compare with the current identifier.</param>
    /// <returns>True if the provided identifier is equal to the current identifier; otherwise, false.</returns>
    public bool IsEqual(long storedId)
    {
        return Current == storedId;
    }

    /// <summary>
    /// Updates the event identifier with the current timestamp in Unix format.
    /// </summary>
    private void UpdateIdentifier()
    {
        Current = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).Ticks / (TimeSpan.TicksPerMillisecond / 1000);
    }
}

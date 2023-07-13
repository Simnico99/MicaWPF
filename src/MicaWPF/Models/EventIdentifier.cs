// <copyright file="EventIdentifier.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF.Models;

internal sealed class EventIdentifier
{
    public long Current { get; internal set; } = 0;

    public long GetNext()
    {
        UpdateIdentifier();

        return Current;
    }

    public bool IsEqual(long storedId)
    {
        return Current == storedId;
    }

    private void UpdateIdentifier()
    {
        Current = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).Ticks / (TimeSpan.TicksPerMillisecond / 1000);
    }
}

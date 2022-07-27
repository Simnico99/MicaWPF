using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Models;
internal class EventIdentifier
{
    public long Current { get; internal set; } = 0;
    public long GetNext()
    {
        UpdateIdentifier();

        return Current;
    }

    public bool IsEqual(long storedId) => Current == storedId;

    private void UpdateIdentifier() => Current = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).Ticks / (TimeSpan.TicksPerMillisecond / 1000);
}

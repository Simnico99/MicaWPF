using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Events;
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

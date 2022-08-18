using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Extensions;
public static class FrameworkElementExtension
{
    public static void RefreshStyle(this FrameworkElement element)
    {
        lock (element)
        {
            var savedStyle = element.Style;
            element.Style = new();

            element.UpdateDefaultStyle();

            element.Style = savedStyle;
        }
    }
}

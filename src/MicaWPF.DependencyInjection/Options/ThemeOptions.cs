using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.DependencyInjection.Options;
public class ThemeOptions
{
    public bool IsThemeAware { get; set; } = true;
    public WindowsTheme Theme { get; set; } = WindowsTheme.Auto;
}

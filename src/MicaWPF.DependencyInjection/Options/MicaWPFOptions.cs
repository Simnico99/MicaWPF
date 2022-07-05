using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MicaWPF.DependencyInjection.Options;
public class MicaWPFOptions
{
    public bool IsThemeAware { get; set; } = true;
    public bool UpdateAccentFromWindows { get; set; } = true;
    public WindowsTheme Theme { get; set; } = WindowsTheme.Auto;
    public Color AccentColor { get; set; } = default;
}

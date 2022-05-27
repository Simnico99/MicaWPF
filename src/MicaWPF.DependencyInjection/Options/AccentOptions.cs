using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MicaWPF.DependencyInjection.Options;
public class AccentOptions
{
    public bool UpdateAccentFromWindows { get; set; } = true;
    public Color AccentColor { get; set; } = default;
}

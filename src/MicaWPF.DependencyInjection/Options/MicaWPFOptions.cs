using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.DependencyInjection.Options;
public class MicaWPFOptions
{
    public ThemeOptions ThemeOptions { get; set; } = new();
    public AccentOptions AccentOptions { get; set; } = new();
}

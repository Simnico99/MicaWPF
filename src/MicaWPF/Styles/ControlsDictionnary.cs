using System.Windows.Markup;

namespace MicaWPF.Styles;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public sealed class ControlsDictionnary : ResourceDictionary
{
    public ControlsDictionnary()
    {
        Source = new($"pack://application:,,,/MicaWPF;component/Styles/MicaWPF.xaml", UriKind.Absolute);
    }
}

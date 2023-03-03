using System.Windows.Markup;

namespace MicaWPF.Styles;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public sealed class ThemeDictionnary : ResourceDictionary
{
    public WindowsTheme Theme
    {
        set
        {
            var themeName = value switch
            {
                WindowsTheme.Dark => "MicaDark",
                _ => "MicaLight"
            };

            //MergedDictionaries.Add(new ResourceDictionary() { Source = new($"pack://application:,,,/MicaWPF;component/Styles/Themes/{themeName}.xaml", UriKind.Absolute) });
            Source = new($"pack://application:,,,/MicaWPF;component/Styles/Themes/{themeName}.xaml", UriKind.Absolute);
        } 
    }
}

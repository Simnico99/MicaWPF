// <copyright file="ThemeDictionary.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Windows.Markup;
using MicaWPF.Core.Enums;

namespace MicaWPF.Styles;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public sealed class ThemeDictionary : ResourceDictionary
{
    public WindowsTheme Theme
    {
        set
        {
            var themeName = value switch
            {
                WindowsTheme.Dark => "MicaDark",
                _ => "MicaLight",
            };

            // MergedDictionaries.Add(new ResourceDictionary() { Source = new($"pack://application:,,,/MicaWPF;component/Styles/Themes/{themeName}.xaml", UriKind.Absolute) });
            Source = new($"pack://application:,,,/MicaWPF;component/Styles/Themes/{themeName}.xaml", UriKind.Absolute);
        }
    }
}

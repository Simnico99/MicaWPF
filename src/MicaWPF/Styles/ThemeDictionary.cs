// <copyright file="ThemeDictionary.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
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

            Source = new($"pack://application:,,,/MicaWPF;component/Styles/Themes/{themeName}.xaml", UriKind.Absolute);
        }
    }
}

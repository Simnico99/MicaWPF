// <copyright file="ControlsDictionary.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Markup;

namespace MicaWPF.Lite.Styles;

[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public sealed class ControlsDictionary : ResourceDictionary
{
    public ControlsDictionary()
    {
        Source = new($"pack://application:,,,/MicaWPF.Lite;component/Styles/MicaWPF.Lite.xaml", UriKind.Absolute);
    }
}

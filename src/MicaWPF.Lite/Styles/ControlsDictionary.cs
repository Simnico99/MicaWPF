﻿// <copyright file="ControlsDictionary.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

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
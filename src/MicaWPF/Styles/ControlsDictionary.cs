﻿// <copyright file="ControlsDictionary.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Markup;

namespace MicaWPF.Styles;

[Ambient]
[Localizability(LocalizationCategory.Ignore)]
[UsableDuringInitialization(true)]
public sealed class ControlsDictionary : ResourceDictionary
{
    public ControlsDictionary()
    {
        Source = new($"pack://application:,,,/MicaWPF;component/Styles/MicaWPF.xaml", UriKind.Absolute);
    }
}

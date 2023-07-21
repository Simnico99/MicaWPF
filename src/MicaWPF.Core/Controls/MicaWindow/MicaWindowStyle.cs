// <copyright file="MicaWindowStyle.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Services;

namespace MicaWPF.Core.Controls.MicaWindow;

public class MicaWindowStyle : Window
{
    static MicaWindowStyle()
    {
        if (OsHelper.IsWindows11_OrGreater)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaWindowStyle), new FrameworkPropertyMetadata(typeof(MicaWindowStyle)));
        }
    }

    public MicaWindowStyle()
    {
        var myResourceDictionary = new ResourceDictionary
        {
            Source = new Uri($"MicaWPF.Core;component/Styles/MicaWindow.xaml", UriKind.RelativeOrAbsolute),
        };

        Style = OsHelper.IsWindows11_OrGreater
            ? myResourceDictionary[$"MicaWPF.Core.Styles.Default.MicaWindow.Windows11"] as Style
            : myResourceDictionary[$"MicaWPF.Core.Styles.Default.MicaWindow.Windows10"] as Style;
    }
}

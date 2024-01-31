// <copyright file="ThemeDictionaryBase.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Markup;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Helpers;

namespace MicaWPF.Core.Styles;

[Localizability(LocalizationCategory.Ignore)]
[UsableDuringInitialization(true)]
public abstract class ThemeDictionaryBase : ResourceDictionary
{
    private WindowsTheme? _designModeTheme = null;

    public ThemeDictionaryBase()
    {
        if (DesignTimeHelper.IsDesignMode && !_designModeTheme.HasValue)
        {
            SetTheme(Enums.WindowsTheme.Light);
            return;
        }

        if(!WindowsTheme.HasValue)
        {
            SetTheme(Enums.WindowsTheme.Auto);
        }
    }

    public WindowsTheme? WindowsTheme { get; private set; } = null;

    public virtual string SourceLocation { get; } = string.Empty;

    public WindowsTheme Theme
    {
        get => WindowsTheme!.Value;
        set
        {
            WindowsTheme = value;
            if (!DesignTimeHelper.IsDesignMode)
            {
                SetTheme(value);
            }
        }
    }

    public WindowsTheme DesignModeTheme
    {
        get => _designModeTheme!.Value;
        set
        {
            _designModeTheme = value;
            if (DesignTimeHelper.IsDesignMode)
            {
                SetTheme(value);
            }
        }
    }

    protected void SetTheme(WindowsTheme value)
    {
        var themeName = value switch
        {
            Enums.WindowsTheme.Dark => "MicaDark",
            _ => "MicaLight",
        };

        Source = new($"{SourceLocation}/{themeName}.xaml", UriKind.Absolute);
    }
}

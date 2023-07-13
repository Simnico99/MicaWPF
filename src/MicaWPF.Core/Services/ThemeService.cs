// <copyright file="ThemeService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Windows.Interop;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Events;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Interop;
using Microsoft.Win32;

namespace MicaWPF.Core.Services;

/// <summary>
/// Service that manages the theme of the application.
/// </summary>
public sealed class ThemeService : IThemeService
{
    private IAccentColorService? _accentColorService;
    private WindowsTheme _currentTheme;
    private bool _isCheckingTheme;

    static ThemeService()
    {
        Current = new ThemeService();
        _ = Current.ChangeTheme(WindowsTheme.Auto);
    }

    private ThemeService()
    {
    }

    /// <summary>
    /// Gets the current instance of the <see cref="ThemeService"/> class but as the interface <see cref="IThemeService"/>.
    /// </summary>
    public static IThemeService Current { get; }

    public IWeakEvent<WindowsTheme> ThemeChanged { get; } = new WeakEvent<WindowsTheme>();

    public List<BackdropEnabledWindow> BackdropEnabledWindows { get; private set; } = new List<BackdropEnabledWindow>();

    public WindowsTheme CurrentTheme { get => GetTheme(); private set => _currentTheme = value; }

    public bool IsThemeAware { get; private set; }

    public void SetAccentColorService(IAccentColorService accentColorService)
    {
        _accentColorService = accentColorService;
    }

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        IsThemeAware = windowsTheme == WindowsTheme.Auto;
        CurrentTheme = windowsTheme == WindowsTheme.Auto ? WindowsThemeHelper.GetCurrentWindowsTheme() : windowsTheme;

        _accentColorService?.RefreshAccentsColors();
        ThemeDictionaryService.Current.ThemeSource = WindowsThemeHelper.WindowsThemeToResourceTheme(CurrentTheme);

        lock (BackdropEnabledWindows)
        {
#if NET5_0_OR_GREATER
            foreach (var backdropEnabledWindow in CollectionsMarshal.AsSpan(BackdropEnabledWindows))
            {
                SetWindowBackdrop(backdropEnabledWindow.Window, backdropEnabledWindow.BackdropType);

                // Force the title bar to refresh.
                var style = backdropEnabledWindow.Window.WindowStyle;
                backdropEnabledWindow.Window.WindowStyle = WindowStyle.None;
                backdropEnabledWindow.Window.WindowStyle = style;
            }
#else
            foreach (var backdropEnabledWindow in BackdropEnabledWindows)
            {
                SetWindowBackdrop(backdropEnabledWindow.Window, backdropEnabledWindow.BackdropType);

                // Force the title bar to refresh.
                var style = backdropEnabledWindow.Window.WindowStyle;
                backdropEnabledWindow.Window.WindowStyle = WindowStyle.None;
                backdropEnabledWindow.Window.WindowStyle = style;
            }
#endif
        }

        ThemeChanged.Publish(CurrentTheme);

        return CurrentTheme;
    }

    public void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _ = _accentColorService;
        SetWindowBackdrop(window, micaType);

        lock (BackdropEnabledWindows)
        {
            BackdropEnabledWindows.Add(new BackdropEnabledWindow(window, micaType));
        }
    }

    private WindowsTheme GetTheme()
    {
        return _currentTheme == WindowsTheme.Auto ? WindowsThemeHelper.GetCurrentWindowsTheme() : _currentTheme;
    }

    private void SetWindowBackdrop(Window window, BackdropType micaType)
    {
        if (!OsHelper.IsWindows11_OrGreater)
        {
            return;
        }

        window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        var windowHandle = new WindowInteropHelper(window).Handle;

        if (CurrentTheme == WindowsTheme.Dark)
        {
            InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.True);
        }
        else
        {
            InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.False);
        }

        if (OsHelper.IsWindows11_22523_OrGreater)
        {
            InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, (int)micaType);
        }
        else
        {
            InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, InteropValues.DwmValues.True);
        }
    }

    private void SetThemeAware()
    {
        if (IsThemeAware && !_isCheckingTheme)
        {
            _isCheckingTheme = true;
            if (OsHelper.IsWindows10_OrGreater)
            {
                SystemEvents.UserPreferenceChanged += SystemEventsUserPreferenceChanged;
            }
        }
        else if (!IsThemeAware && _isCheckingTheme)
        {
            SystemEvents.UserPreferenceChanged -= SystemEventsUserPreferenceChanged;
            _isCheckingTheme = false;
        }
    }

    private void SystemEventsUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category != UserPreferenceCategory.General || !IsThemeAware)
        {
            return;
        }

        _ = Application.Current.Dispatcher.Invoke(() => ChangeTheme(WindowsTheme.Auto));
        SetThemeAware();
    }
}
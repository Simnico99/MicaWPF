using MicaWPF.Events;

namespace MicaWPF.Services;
public sealed class ThemeService : IThemeService
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string _registryValueName = "AppsUseLightTheme";

    private WindowsTheme _currentTheme;
    private bool _isCheckingTheme;

    public static ThemeService Current { get; }
    public IWeakEvent<WindowsTheme> ThemeChanged { get; } = new WeakEvent<WindowsTheme>();
    public List<MicaEnabledWindow> MicaEnabledWindows { get; private set; } = new List<MicaEnabledWindow>();
    public WindowsTheme CurrentTheme { get => GetTheme(); private set => _currentTheme = value; }
    public bool IsThemeAware { get; private set; }

    static ThemeService()
    {
        Current = new();
        _ = Current.ChangeTheme(WindowsTheme.Auto);
    }

    private WindowsTheme GetTheme()
    {
        return _currentTheme == WindowsTheme.Auto ? GetWindowsTheme() : _currentTheme;
    }

    private void UpdateAccent()
    {
        _ = Task.Run(() =>
        {
            if (AccentColorService.Current.AccentUpdateFromWindows)
            {
                AccentColorService.Current.UpdateAccentsFromWindows();
            }
            else
            {
                AccentColorService.Current.UpdateAccents(AccentColorService.Current.AccentColors.SystemAccentColor);
            }
        });
    }

    private void SetThemeAware()
    {
        if (IsThemeAware && !_isCheckingTheme)
        {
            _isCheckingTheme = true;
            if (OsHelper.IsWindows10_OrGreater && IsThemeAware)
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
        switch (e.Category)
        {
            case UserPreferenceCategory.General:
                if (IsThemeAware)
                {
                    UpdateAccent();
                    _ = Application.Current.Dispatcher.Invoke(() => ChangeTheme(WindowsTheme.Auto));
                    SetThemeAware();
                }
                break;
        }
    }

    private void SetWindowBackdrop(Window window, BackdropType micaType)
    {
        if (OsHelper.IsWindows11_OrGreater)
        {
            window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            var windowHandle = new WindowInteropHelper(window).Handle;

            if (CurrentTheme == WindowsTheme.Dark)
            {
                _ = InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.True);
            }
            else if (OsHelper.IsWindows11_OrGreater)
            {
                _ = InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.False);
            }

            _ = OsHelper.IsWindows11_22523_OrGreater
                ? InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, (int)micaType)
                : InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, InteropValues.DwmValues.True);
        }
    }

    public static WindowsTheme GetWindowsTheme()
    {
        using var key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        var registryValueObject = key?.GetValue(_registryValueName);

        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
    }

    public static Uri WindowsThemeToResourceTheme(WindowsTheme windowsTheme)
    {
        return windowsTheme == WindowsTheme.Dark
            ? new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaDark.xaml")
            : new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaLight.xaml");
    }

    public static void RefreshTheme()
    {
        ThemeDictionaryService.Current.ThemeSource = ThemeDictionaryService.Current.ThemeSource;
    }

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        IsThemeAware = windowsTheme == WindowsTheme.Auto;
        CurrentTheme = windowsTheme == WindowsTheme.Auto ? GetWindowsTheme() : windowsTheme;

        UpdateAccent();
        ThemeDictionaryService.Current.ThemeSource = WindowsThemeToResourceTheme(CurrentTheme);

        lock (MicaEnabledWindows)
        {
#if NET5_0_OR_GREATER
            foreach (var micaEnabledWindow in CollectionsMarshal.AsSpan(MicaEnabledWindows))
            {
                SetWindowBackdrop(micaEnabledWindow.Window, micaEnabledWindow.BackdropType);
                //Force the title bar to refresh.
                var style = micaEnabledWindow.Window.WindowStyle;
                micaEnabledWindow.Window.WindowStyle = WindowStyle.None;
                micaEnabledWindow.Window.WindowStyle = style;
            }
#else
            foreach (var micaEnabledWindow in MicaEnabledWindows)
            {
                SetWindowBackdrop(micaEnabledWindow.Window, micaEnabledWindow.BackdropType);
                //Force the title bar to refresh.
                var style = micaEnabledWindow.Window.WindowStyle;
                micaEnabledWindow.Window.WindowStyle = WindowStyle.None;
                micaEnabledWindow.Window.WindowStyle = style;
            }
#endif
        }

        ThemeChanged.Publish(CurrentTheme);

        return CurrentTheme;
    }

    public void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _ = AccentColorService.Current;
        SetWindowBackdrop(window, micaType);

        lock (MicaEnabledWindows)
        {
            MicaEnabledWindows.Add(new MicaEnabledWindow(window, micaType));
        }
    }
}
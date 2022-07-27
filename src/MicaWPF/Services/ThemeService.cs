namespace MicaWPF.Services;
public class ThemeService : IThemeService
{
    private static readonly ThemeService _themeService = new();
    private static readonly ThemeDictionaryService _themeManager = ThemeDictionaryService.GetCurrent();
    private readonly AccentColorService _accentColorService = AccentColorService.GetCurrent();

    private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string RegistryValueName = "AppsUseLightTheme";

    private WindowsTheme _currentTheme;
    private bool _isThemeAware;
    private bool _IsCheckingTheme;

    public ICollection<MicaEnabledWindow> MicaEnabledWindows { get; private set; } = new List<MicaEnabledWindow>();
    public WindowsTheme CurrentTheme { get => GetTheme(); private set => _currentTheme = value; }
    public bool IsThemeAware { get => _isThemeAware; set => SetThemeAware(value); }

    private ThemeService()
    {
        CurrentTheme = WindowsTheme.Auto;
        IsThemeAware = true;
    }

    private WindowsTheme GetTheme()
    {
        if (_currentTheme == WindowsTheme.Auto)
        {
            return GetWindowsTheme();
        }

        return _currentTheme;
    }

    private void UpdateAccent()
    {
        Task.Run(() =>
        {
            if (_accentColorService.AccentUpdateFromWindows)
            {
                _accentColorService.UpdateAccentsFromWindows();
            }
            else
            {
                _accentColorService.UpdateAccents(_accentColorService.SystemAccentColor);
            }
        });
    }

    private void SetThemeAware(bool isThemeAware)
    {
        _isThemeAware = isThemeAware;

        if (IsThemeAware && !_IsCheckingTheme)
        {
            _IsCheckingTheme = true;
            if (OsHelper.GlobalOsVersion is not OsVersion.WindowsOld && IsThemeAware)
            {
                SystemEvents.UserPreferenceChanged += SystemEventsUserPreferenceChanged;
            }
        }
        else if (!IsThemeAware && _IsCheckingTheme)
        {
            SystemEvents.UserPreferenceChanged -= SystemEventsUserPreferenceChanged;
            _IsCheckingTheme = false;
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
                    Application.Current.Dispatcher.Invoke(() => ChangeTheme(WindowsTheme.Auto));
                    SetThemeAware(IsThemeAware);
                }
                break;
        }
    }

    private void SetWindowMica(Window window, BackdropType micaType)
    {
        if (OsHelper.GlobalOsVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
        {
            window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            var windowHandle = new WindowInteropHelper(window).Handle;

            if (CurrentTheme == WindowsTheme.Dark)
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.True);
            }
            else if (OsHelper.GlobalOsVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.False);
            }

            if (OsHelper.GlobalOsVersion == OsVersion.Windows11After22523)
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, (int)micaType);
            }
            else
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, InteropValues.DwmValues.True);
            }
        }
    }

    public static ThemeService GetCurrent()
    {
        return _themeService;
    }

    public static WindowsTheme GetWindowsTheme()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        var registryValueObject = key?.GetValue(RegistryValueName);

        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
    }

    public static Uri WindowsThemeToResourceTheme(WindowsTheme windowsTheme)
    {
        if (windowsTheme == WindowsTheme.Dark)
        {
            return new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaDark.xaml");
        }
        else
        {
            return new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaLight.xaml");
        }
    }

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        if (windowsTheme == WindowsTheme.Auto)
        {
            CurrentTheme = GetWindowsTheme();
        }
        else
        {
            CurrentTheme = windowsTheme;
        }

        UpdateAccent();
        _themeManager.ThemeSource = WindowsThemeToResourceTheme(CurrentTheme);

        foreach (var micaEnabledWindow in MicaEnabledWindows)
        {
            SetWindowMica(micaEnabledWindow.Window, micaEnabledWindow.BackdropType);
            //Force the title bar to refresh.
            var style = micaEnabledWindow.Window.WindowStyle;
            micaEnabledWindow.Window.WindowStyle = WindowStyle.None;
            micaEnabledWindow.Window.WindowStyle = style;
        }

        return CurrentTheme;
    }

    public void EnableMica(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _accentColorService.Init();
        SetWindowMica(window, micaType);
        MicaEnabledWindows.Add(new MicaEnabledWindow(window, micaType));
    }
}

using MicaWPF.Controls;

namespace MicaWPF.Services;
internal class DynamicThemeService
{
    private readonly AccentColorService _accentColorService = AccentColorService.GetCurrent();
    private readonly OsVersion _currentOsVersion;
    private readonly Window _window;
    private CancellationTokenSource _waitForDynamicThemeCancellationToken = new();
    private bool _isWaitingForThemeChange = false;
    private bool _isThemeAware = false;

    public DynamicThemeService(Window window)
    {
        _window = window;
        _currentOsVersion = OsHelper.GlobalOsVersion;
    }

    private void UpdateAccent()
    {
        Task.Run(() =>
        {
            if (_window is MicaWindow micaWindow)
            {
                if (_accentColorService.AccentUpdateFromWindows)
                {
                    _accentColorService.UpdateAccentsFromWindows(micaWindow.Theme);
                }
                else
                {
                    _accentColorService.SetAccents(_accentColorService.SystemAccentColor, micaWindow.Theme);
                }
            }
        });
    }

    public void SetThemeAware(bool isThemeAware, BackdropType micaType = BackdropType.Mica)
    {
        if (!_isThemeAware && isThemeAware)
        {
            _isThemeAware = true;
            if (_currentOsVersion is not OsVersion.WindowsOld && isThemeAware)
            {
                SystemEvents.UserPreferenceChanged += (s, e) =>
                {
                    switch (e.Category)
                    {
                        case UserPreferenceCategory.General:
                            UpdateAccent();
                            Application.Current.Dispatcher.Invoke(() => MicaHelper.EnableMica(_window, WindowsTheme.Auto, micaType, -1));
                            SetThemeAware(isThemeAware, micaType);
                            break;
                    }
                };
            }
        }
        else if (_isThemeAware && !isThemeAware)
        {
            _isThemeAware = false;
            var handler = (UserPreferenceChangedEventHandler)Delegate.CreateDelegate(typeof(UserPreferenceChangedEventHandler), this, "UserPreferenceChangedHandler");
            SystemEvents.UserPreferenceChanged -= handler;
        }
    }

    public void AwaitManualThemeChange(bool awaitChange, BackdropType micaType = BackdropType.Mica)
    {
        if (!_isWaitingForThemeChange && awaitChange)
        {
            _waitForDynamicThemeCancellationToken = new();
            if (_window is MicaWindow micaWindow)
            {
                var oldTheme = micaWindow.Theme;
                _ = Task.Run(async () =>
                {
                    while (oldTheme == micaWindow.Theme && !_waitForDynamicThemeCancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(500);
                    }

                    if (!_waitForDynamicThemeCancellationToken.IsCancellationRequested)
                    {
                        UpdateAccent();
                        Application.Current.Dispatcher.Invoke(() => MicaHelper.EnableMica(_window, micaWindow.Theme, micaType, -1));
                        AwaitManualThemeChange(awaitChange, micaType);
                    }
                    else
                    {
                        _isWaitingForThemeChange = false;
                    }
                });
            }
        }
        else if (_isWaitingForThemeChange && !awaitChange)
        {
            _waitForDynamicThemeCancellationToken.Cancel();
        }
    }
}

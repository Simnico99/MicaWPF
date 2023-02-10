using MicaWPF.Extensions;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace MicaWPF.Controls;

/// <summary>
/// A window where custom backdrops are enabled.
/// </summary>
public class MicaWindow : Window
{
    #region SnapLayout
    private const int _hTMAXBUTTON = 9;
    private const string _buttonMaxName = "Maximize";
    private const string _buttonRestoreName = "Restore";
    private System.Windows.Controls.Button? _buttonMax;
    private System.Windows.Controls.Button? _buttonRestore;
    #endregion

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Is a property.")]
    internal static readonly DependencyProperty MarginMaximizedProperty = DependencyProperty.Register(nameof(MarginMaximized), typeof(Thickness), typeof(MicaWindow));
    public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register(nameof(TitleBarContent), typeof(UIElement), typeof(MicaWindow));
    public static readonly DependencyProperty UseAccentOnTitleBarAndBorderProperty = DependencyProperty.Register(nameof(UseAccentOnTitleBarAndBorder), typeof(bool), typeof(MicaWindow), new UIPropertyMetadata(AccentColorService.Current.IsTitleBarAndWindowsBorderColored));
    public static readonly DependencyProperty ChangeTitleColorWhenInactiveProperty = DependencyProperty.Register(nameof(ChangeTitleColorWhenInactive), typeof(bool), typeof(MicaWindow), new UIPropertyMetadata(true));
    public static readonly DependencyProperty TitleBarHeightProperty = DependencyProperty.Register(nameof(TitleBarHeight), typeof(int), typeof(MicaWindow), new UIPropertyMetadata(34));
    public static readonly DependencyProperty TitleBarTypeProperty = DependencyProperty.Register(nameof(TitleBarType), typeof(TitleBarType), typeof(MicaWindow), new UIPropertyMetadata(TitleBarType.Win32));

    internal Thickness? MarginMaximized
    {
        get => (Thickness)GetValue(MarginMaximizedProperty);
        set => SetValue(MarginMaximizedProperty, value);
    }

    /// <summary>
    /// The current type of backdrop the window is using.
    /// </summary>
    public BackdropType SystemBackdropType { get; set; } = BackdropType.Mica;

    /// <summary>
    /// The height of the title bar.
    /// </summary>
    public int TitleBarHeight
    {
        get => (int)GetValue(TitleBarHeightProperty);
        set => SetValue(TitleBarHeightProperty, value);
    }

    /// <summary>
    /// Is showing the accented border.
    /// </summary>
    public bool UseAccentOnTitleBarAndBorder
    {
        get => (bool)GetValue(UseAccentOnTitleBarAndBorderProperty);
        set => SetValue(UseAccentOnTitleBarAndBorderProperty, value);
    }

    /// <summary>
    /// The type of title bar used.
    /// </summary>
    public TitleBarType TitleBarType
    {
        get => (TitleBarType)GetValue(TitleBarTypeProperty);
        set => SetValue(TitleBarTypeProperty, value);
    }

    /// <summary>
    /// Should the title color change when the window is inactive.
    /// </summary>
    public bool ChangeTitleColorWhenInactive
    {
        get => (bool)GetValue(MarginMaximizedProperty);
        set => SetValue(MarginMaximizedProperty, value);
    }

    /// <summary>
    /// Custom <see cref="UIElement"/> that are embeded in the title bar.
    /// </summary>
    public UIElement TitleBarContent
    {
        get => (UIElement)GetValue(TitleBarContentProperty);
        set => SetValue(TitleBarContentProperty, value);
    }

    static MicaWindow()
    {
        if (OsHelper.IsWindows11_OrGreater)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaWindow), new FrameworkPropertyMetadata(typeof(MicaWindow)));
        }
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        if (OsHelper.IsWindows11_OrGreater && TitleBarType == TitleBarType.WinUI)
        {
            HwndSource.FromHwnd(new WindowInteropHelper(this).EnsureHandle())?.AddHook(HwndSourceHook);
            InteropMethods.HideAllWindowButton(new WindowInteropHelper(this).Handle);
        }
    }

    private void AddPadding(WindowState windowsState)
    {
        MarginMaximized = windowsState == WindowState.Maximized ? new Thickness(6) : new Thickness(0);
    }

    private void ApplyResizeBorderThickness(WindowState windowsState)
    {
        if (windowsState == WindowState.Maximized || ResizeMode == ResizeMode.NoResize)
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome()
            {
                CaptionHeight = TitleBarHeight - 7,
                CornerRadius = new CornerRadius(8),
                GlassFrameThickness = new Thickness(-1),
                ResizeBorderThickness = new Thickness(0)
            });
        }
        else
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome()
            {
                CaptionHeight = TitleBarHeight - 7,
                CornerRadius = new CornerRadius(8),
                GlassFrameThickness = new Thickness(-1),
                ResizeBorderThickness = new Thickness(6)
            });
        }
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property.Name is nameof(WindowState))
        {
            AddPadding((WindowState)e.NewValue);
            ApplyResizeBorderThickness((WindowState)e.NewValue);
        }

        base.OnPropertyChanged(e);
    }

    public override void OnApplyTemplate()
    {
        _buttonMax = GetTemplateChild(_buttonMaxName) as System.Windows.Controls.Button;
        _buttonRestore = GetTemplateChild(_buttonRestoreName) as System.Windows.Controls.Button;

        base.OnApplyTemplate();
    }

    protected override void OnActivated(EventArgs e)
    {
        this.EnableBackdrop(SystemBackdropType);
        base.OnActivated(e);
    }

    public override void EndInit()
    {
        AddPadding(WindowState);
        ApplyResizeBorderThickness(WindowState);

        base.EndInit();
    }

    public MicaWindow()
    {
        var myResourceDictionary = new ResourceDictionary
        {
            Source = new Uri("/MicaWPF;component/Styles/Controls/MicaWindow.xaml", UriKind.RelativeOrAbsolute)
        };

        _ = CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
        _ = CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
        _ = CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
        _ = CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));

        Style = OsHelper.IsWindows11_OrGreater
            ? myResourceDictionary["MicaWPF.Styles.Default.MicaWindow.Windows11"] as Style
            : myResourceDictionary["MicaWPF.Styles.Default.MicaWindow.Windows10"] as Style;
    }

    private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = ResizeMode is ResizeMode.CanResize or ResizeMode.CanResizeWithGrip;
    }

    private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = ResizeMode != ResizeMode.NoResize;
    }

    private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
    {
        SystemCommands.CloseWindow(this);
    }

    private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
    {
        SystemCommands.MaximizeWindow(this);
        if (_buttonMax is not null)
        {
            _buttonMax.Background = Brushes.Transparent;
        }
    }

    private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
    {
        SystemCommands.MinimizeWindow(this);
    }

    private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
    {
        SystemCommands.RestoreWindow(this);
    }

    private nint ShowSnapLayout(nint lparam, ref bool handled)
    {
        var x = lparam.ToInt32() & 0xffff;
        var y = lparam.ToInt32() >> 16;
        var point = new Point(x, y);
        var DPI_SCALE = DpiHelper.LogicalToDeviceUnitsScalingFactorX;
        var _button = WindowState == WindowState.Maximized ? _buttonRestore : _buttonMax;

        if (_button != null)
        {
            var buttonSize = new Size(_button.ActualWidth * DPI_SCALE, _button.ActualHeight * DPI_SCALE);
            var buttonLocation = _button.PointToScreen(new Point());
            var rect = new Rect(buttonLocation, buttonSize);

            handled = rect.Contains(point);
            if (handled)
            {
                var color = TryFindResource("MicaWPF.GradientBrushes.ControlElevationBorder") as LinearGradientBrush ?? new LinearGradientBrush();
                _button.Background = color;
            }
            else
            {
                _button.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }

            return PtrHelper.Create(_hTMAXBUTTON);
        }

        return PtrHelper.Zero;
    }

    private void HideMaximiseAndMinimiseButton(nint lparam, ref bool handled)
    {
        var x = lparam.ToInt32() & 0xffff;
        var y = lparam.ToInt32() >> 16;

        var DPI_SCALE = DpiHelper.LogicalToDeviceUnitsScalingFactorX;
        var _button = WindowState == WindowState.Maximized ? _buttonRestore : _buttonMax;
        if (_button == null || !_button.IsVisible)
        {
            return;
        }

        var rect = new Rect(_button.PointToScreen(
        new Point()),
        new Size(_button.ActualWidth * DPI_SCALE, _button.ActualHeight * DPI_SCALE));
        if (!rect.Contains(new Point(x, y)))
        {
            return;
        }

        handled = true;
        var invokeProv = new ButtonAutomationPeer(_button).GetPattern(PatternInterface.Invoke) as IInvokeProvider;
        invokeProv?.Invoke();
    }

    private nint HwndSourceHook(nint hwnd, int msg, nint _, nint lparam, ref bool handled)
    {
        if (ResizeMode is ResizeMode.NoResize or ResizeMode.CanMinimize)
        {
            return PtrHelper.Zero;
        }

        switch (msg)
        {
            case InteropValues.HwndSourceMessages.WM_NCHITTEST:
                return ShowSnapLayout(lparam, ref handled);
            case InteropValues.HwndSourceMessages.WM_NCLBUTTONDOWN:
                HideMaximiseAndMinimiseButton(lparam, ref handled);
                break;
                //case InteropValues.HwndSourceMessages.WM_GETMINMAXINFO:
                //    WmGetMinMaxInfo(hwnd, lparam);
                //    break;
        }

        return PtrHelper.Zero;
    }
}

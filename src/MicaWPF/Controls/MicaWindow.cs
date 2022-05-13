using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using MicaWPF.Extensions;

namespace MicaWPF.Controls;
public class MicaWindow : Window
{
    #region SnapLayout
    private const int HTMAXBUTTON = 9;
    private const string ButtonMax = "Maximize";
    private const string ButtonRestore = "Restore";
    private Button? _ButtonMax;
    private Button? _ButtonRestore;
    #endregion

    public static readonly DependencyProperty MarginMaximizedProperty = DependencyProperty.Register(nameof(MarginMaximized), typeof(Thickness), typeof(MicaWindow));
    public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register(nameof(TitleBarContent), typeof(UIElement), typeof(MicaWindow));
    public static readonly DependencyProperty ChangeTitleColorWhenInactiveProperty = DependencyProperty.Register(nameof(ChangeTitleColorWhenInactive), typeof(bool), typeof(MicaWindow), new UIPropertyMetadata(true));

    public BackdropType SystemBackdropType { get; set; } = BackdropType.Mica;
    public int CaptionHeight { get; set; } = 20;

    public Thickness? MarginMaximized
    {
        get => (Thickness)GetValue(MarginMaximizedProperty);
        set => SetValue(MarginMaximizedProperty, value);
    }

    public bool ChangeTitleColorWhenInactive
    {
        get => (bool)GetValue(MarginMaximizedProperty);
        set => SetValue(MarginMaximizedProperty, value);
    }

    public UIElement TitleBarContent
    {
        get => (UIElement)GetValue(TitleBarContentProperty);
        set => SetValue(TitleBarContentProperty, value);
    }

    static MicaWindow()
    {
        if (OsHelper.GlobalOsVersion is not OsVersion.Windows11Before22523 and not OsVersion.Windows11After22523)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaWindow), new FrameworkPropertyMetadata(typeof(MicaWindow)));
        }
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        HwndSource.FromHwnd(new WindowInteropHelper(this).EnsureHandle())?.AddHook(HwndSourceHook);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property.Name is nameof(WindowState))
        {
            if ((WindowState)e.NewValue == WindowState.Maximized)
            {
                MarginMaximized = new Thickness(6);
            }
            else
            {
                MarginMaximized = new Thickness(0);
            }
        }
        base.OnPropertyChanged(e);
    }

    public override void OnApplyTemplate()
    {
        _ButtonMax = GetTemplateChild(ButtonMax) as Button;
        _ButtonRestore = GetTemplateChild(ButtonRestore) as Button;
        this.EnableMica(SystemBackdropType, CaptionHeight);
        base.OnApplyTemplate();
    }

    public MicaWindow()
    {
        CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));

        if (OsHelper.GlobalOsVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
        {
            var myResourceDictionary = new ResourceDictionary
            {
                Source = new Uri("/MicaWPF;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
            };

            Style = myResourceDictionary["MicaWindow11"] as Style;
        }
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
    }

    private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
    {
        SystemCommands.MinimizeWindow(this);
    }

    private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
    {
        SystemCommands.RestoreWindow(this);
    }

    private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
    {
        switch (msg)
        {
            case InteropValues.WM_NCHITTEST:
                try
                {
                    if (OsHelper.GlobalOsVersion is OsVersion.Windows11After22523 or OsVersion.Windows11Before22523 && ResizeMode is not ResizeMode.NoResize and not ResizeMode.CanMinimize)
                    {
                        var x = lparam.ToInt32() & 0xffff;
                        var y = lparam.ToInt32() >> 16;
                        var DPI_SCALE = DpiHelper.LogicalToDeviceUnitsScalingFactorX;
                        var _button = WindowState == WindowState.Maximized ? _ButtonRestore : _ButtonMax;
                        if (_button != null)
                        {
                            var rect = new Rect(_button.PointToScreen(
                            new Point()),
                            new Size(_button.ActualWidth * DPI_SCALE, _button.ActualHeight * DPI_SCALE));

                            if (rect.Contains(new Point(x, y)))
                            {
                                var color = (LinearGradientBrush)TryFindResource("MicaWPF.GradientBrushes.ControlElevationBorder") ?? new LinearGradientBrush();
                                _button.Background = color;
                                handled = true;
                            }
                            else
                            {
                                _button.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
                            }
                            return new IntPtr(HTMAXBUTTON);
                        }
                    }
                }
                catch (OverflowException)
                {
                    handled = true;
                }
                break;
            case InteropValues.WM_NCLBUTTONDOWN:
                if (OsHelper.GlobalOsVersion is OsVersion.Windows11After22523 or OsVersion.Windows11Before22523 && ResizeMode is not ResizeMode.NoResize and not ResizeMode.CanMinimize)
                {
                    var x = lparam.ToInt32() & 0xffff;
                    var y = lparam.ToInt32() >> 16;
                    var DPI_SCALE = DpiHelper.LogicalToDeviceUnitsScalingFactorX;
                    var _button = WindowState == WindowState.Maximized ? _ButtonRestore : _ButtonMax;
                    if (_button != null)
                    {
                        var rect = new Rect(_button.PointToScreen(
                        new Point()),
                        new Size(_button.ActualWidth * DPI_SCALE, _button.ActualHeight * DPI_SCALE));
                        if (rect.Contains(new Point(x, y)))
                        {
                            handled = true;
                            var invokeProv = new ButtonAutomationPeer(_button).GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            invokeProv?.Invoke();
                        }
                    }
                }
                break;
            default:
                handled = false;
                break;
        }
        return IntPtr.Zero;
    }
}


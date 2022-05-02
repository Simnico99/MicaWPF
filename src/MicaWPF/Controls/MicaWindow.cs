namespace MicaWPF.Controls;
public class MicaWindow : Window
{
    private readonly DynamicThemeService _dynamicThemeService;

    public static readonly DependencyProperty AccentProperty = DependencyProperty.Register(
        "Accent", typeof(SolidColorBrush),
        typeof(MicaWindow)
        );

    public bool IsThemeAware { get; set; } = true;
    public bool UseSystemAccent { get; set; } = true;
    public bool IsWaitingForManualThemeChange { get; set; } = false;
    public WindowsTheme Theme { get; set; } = WindowsTheme.Auto;
    public BackdropType SystemBackdropType { get; set; } = BackdropType.Mica;
    public int CaptionHeight { get; set; } = 20;

    public SolidColorBrush? Accent
    {
        get => (SolidColorBrush)GetValue(AccentProperty);
        set => SetValue(AccentProperty, value);
    }

    static MicaWindow()
    {
        if (OsHelper.GlobalOsVersion is not OsVersion.Windows11Before22523 and not OsVersion.Windows11After22523)
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaWindow), new FrameworkPropertyMetadata(typeof(MicaWindow)));
        }
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property.Name is nameof(IsThemeAware))
        {
            _dynamicThemeService.SetThemeAware(IsThemeAware, SystemBackdropType);
        }

        if (e.Property.Name is nameof(IsWaitingForManualThemeChange))
        {
            _dynamicThemeService.AwaitManualThemeChange(IsWaitingForManualThemeChange, SystemBackdropType);
        }

        if (e.Property.Name is nameof(Theme) or nameof(SystemBackdropType) or nameof(CaptionHeight))
        {
            this.EnableMica(Theme, SystemBackdropType, CaptionHeight);
        }
    }

    public void UpdateTheme()
    {
        if (Accent is null)
        {
            Accent = new SolidColorBrush((Color)FindResource("MicaWPF.Colors.SystemAccentColor"));
        }

        this.EnableMica(Theme, SystemBackdropType, CaptionHeight, UseSystemAccent);
    }

    public override void OnApplyTemplate()
    {
        UpdateTheme();
        _dynamicThemeService.SetThemeAware(IsThemeAware, SystemBackdropType, UseSystemAccent);
        _dynamicThemeService.AwaitManualThemeChange(IsWaitingForManualThemeChange, SystemBackdropType, UseSystemAccent);
        base.OnApplyTemplate();
    }

    public MicaWindow()
    {
        _dynamicThemeService = new(this);
        CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
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
}

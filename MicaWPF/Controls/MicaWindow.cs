using System.Windows.Input;

namespace MicaWPF.Controls;

/// <summary>
/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
///
/// Step 1a) Using this custom control in a XAML file that exists in the current project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is 
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:MicaWPF.Controls"
///
///
/// Step 1b) Using this custom control in a XAML file that exists in a different project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is 
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:MicaWPF.Controls;assembly=MicaWPF.Controls"
///
/// You will also need to add a project reference from the project where the XAML file lives
/// to this project and Rebuild to avoid compilation errors:
///
///     Right click on the target project in the Solution Explorer and
///     "Add Reference"->"Projects"->[Browse to and select this project]
///
///
/// Step 2)
/// Go ahead and use your control in the XAML file.
///
///     <MyNamespace:MicaWindow/>
///
/// </summary>
public class MicaWindow : Window
{
    public bool IsThemeAware { get; set; } = true;

    public WindowsTheme Theme { get; set; } = WindowsTheme.Auto;

    public BackdropType SystemBackdropType { get; set; } = BackdropType.Mica;

    public int CaptionHeight { get; set; } = 20;

    public Color BackgroundColor { set { Resources.Remove("MicaBackgroundColor"); Resources.Add("MicaBackgroundColor", value); } }
    public Color HighLightColor { set { Resources.Remove("MicaHighLightColor"); Resources.Add("MicaHighLightColor", value); } }
    public Color ForegroundColor { set { Resources.Remove("ForegroundColor"); Resources.Add("ForegroundColor", value); } }

    static MicaWindow()
    {
        var osVersion =  OsHelper.GetOsVersion();
        if (osVersion is not OsVersion.Windows11Before22523 and not OsVersion.Windows11After22523 )
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaWindow), new FrameworkPropertyMetadata(typeof(MicaWindow)));
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Loaded += MicaWindow_Loaded;
    }

    private void MicaWindow_Loaded(object sender, RoutedEventArgs e)
    {
        this.EnableMica(Theme, IsThemeAware, SystemBackdropType, CaptionHeight);
    }

    public MicaWindow()
    {
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

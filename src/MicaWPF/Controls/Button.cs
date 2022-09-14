using System.ComponentModel;
using MicaWPF.Symbols;

namespace MicaWPF.Controls;

/// <summary>
/// A WinUI button based on <see cref="System.Windows.Controls.Button"/>
/// </summary>
[ToolboxItem(true)]
public class Button : System.Windows.Controls.Button
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FluentSystemIcons.Regular), typeof(Button), new PropertyMetadata(FluentSystemIcons.Regular.Empty));
    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled), typeof(bool), typeof(Button), new PropertyMetadata(false));
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Button));

    /// <summary>
    /// An icon to show in the button.
    /// </summary>
    public FluentSystemIcons.Regular Icon
    {
        get => (FluentSystemIcons.Regular)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Is the icon filled or not.
    /// </summary>
    public bool IconFilled
    {
        get => (bool)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }

    /// <summary>
    /// The CornerRadius of the button.
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}

using MicaWPF.Symbols;

namespace MicaWPF.Controls;
public class Button : System.Windows.Controls.Button
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FluentSystemIcons.Regular), typeof(Button), new PropertyMetadata(FluentSystemIcons.Regular.Empty));
    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled), typeof(bool), typeof(Button), new PropertyMetadata(false));
    public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(nameof(HoverBackground), typeof(SolidColorBrush), typeof(Button));
    public static readonly DependencyProperty MouseButtonPressedBackgroundProperty = DependencyProperty.Register(nameof(MouseButtonPressedBackground), typeof(SolidColorBrush), typeof(Button));
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Button));

    public FluentSystemIcons.Regular Icon
    {
        get => (FluentSystemIcons.Regular)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IconFilled
    {
        get => (bool)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }

    public SolidColorBrush HoverBackground
    {
        get => (SolidColorBrush)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public SolidColorBrush MouseButtonPressedBackground
    {
        get => (SolidColorBrush)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }
}

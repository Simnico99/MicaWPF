using MicaWPF.Extension.Symbols;

namespace MicaWPF.Extension.Controls;
public class MicaButton : Button
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FluentSystemIcons.Regular), typeof(MicaButton), new PropertyMetadata(FluentSystemIcons.Regular.Empty));
    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled), typeof(bool), typeof(MicaButton), new PropertyMetadata(false));
    public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(nameof(HoverBackground), typeof(SolidColorBrush), typeof(MicaButton));
    public static readonly DependencyProperty MouseButtonPressedBackgroundProperty = DependencyProperty.Register(nameof(MouseButtonPressedBackground), typeof(SolidColorBrush), typeof(MicaButton));

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

    public SolidColorBrush MouseButtonPressedBackground
    {
        get => (SolidColorBrush)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }


    static MicaButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaButton), new FrameworkPropertyMetadata(typeof(MicaButton)));
    }
}

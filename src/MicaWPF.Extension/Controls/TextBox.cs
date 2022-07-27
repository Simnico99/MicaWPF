using MicaWPF.Expansion.Symbols;

namespace MicaWPF.Expansion.Controls;
public class TextBox : System.Windows.Controls.TextBox
{
    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(TextBox));
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FluentSystemIcons.Regular), typeof(System.Windows.Controls.TextBox), new PropertyMetadata(FluentSystemIcons.Regular.Empty));
    public static readonly DependencyProperty IconPositionProperty = DependencyProperty.Register(nameof(IconPosition), typeof(ElementPosition), typeof(TextBox), new PropertyMetadata(ElementPosition.Right));
    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled), typeof(bool), typeof(TextBox), new PropertyMetadata(false));
    public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.RegisterAttached(nameof(IconForeground), typeof(Brush), typeof(TextBox), new FrameworkPropertyMetadata(Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender |
                FrameworkPropertyMetadataOptions.Inherits));

    public FluentSystemIcons.Regular Icon
    {
        get => (FluentSystemIcons.Regular)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public ElementPosition IconPosition
    {
        get => (ElementPosition)GetValue(IconPositionProperty);
        set => SetValue(IconPositionProperty, value);
    }

    public bool IconFilled
    {
        get => (bool)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }

    public System.Windows.Media.Brush IconForeground
    {
        get => (System.Windows.Media.Brush)GetValue(IconForegroundProperty);
        set => SetValue(IconForegroundProperty, value);
    }

    public string? Watermark
    {
        get => (string)GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }
}

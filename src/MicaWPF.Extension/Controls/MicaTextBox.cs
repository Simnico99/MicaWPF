using MicaWPF.Extension.Symbols;

namespace MicaWPF.Extension.Controls;
public class MicaTextBox : TextBox
{
    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(MicaTextBox));
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FluentSystemIcons.Regular), typeof(TextBox), new PropertyMetadata(FluentSystemIcons.Regular.Empty));
    public static readonly DependencyProperty IconPositionProperty = DependencyProperty.Register(nameof(IconPosition), typeof(ElementPosition), typeof(MicaTextBox), new PropertyMetadata(ElementPosition.Right));
    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled), typeof(bool), typeof(MicaTextBox), new PropertyMetadata(false));
    public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.RegisterAttached(nameof(IconForeground), typeof(Brush), typeof(MicaTextBox), new FrameworkPropertyMetadata(Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender |
                FrameworkPropertyMetadataOptions.Inherits));
    static MicaTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaTextBox), new FrameworkPropertyMetadata(typeof(MicaTextBox)));
    }

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

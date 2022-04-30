using System.Drawing;
using MicaWPF.Symbols;

namespace MicaWPF.Controls;
public class MicaTextBox : TextBox
{

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon),
        typeof(FluentSystemIcons.Regular), typeof(TextBox),
        new PropertyMetadata(FluentSystemIcons.Regular.Empty));

    public static readonly DependencyProperty IconPositionProperty = DependencyProperty.Register(
        nameof(IconPosition),
        typeof(ElementPosition), typeof(MicaTextBox),
        new PropertyMetadata(ElementPosition.Left));

    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled),
        typeof(bool), typeof(MicaTextBox), new PropertyMetadata(false));

    public static readonly DependencyProperty IconForegroundProperty =
        DependencyProperty.RegisterAttached(
            nameof(IconForeground),
            typeof(System.Windows.Media.Brush),
            typeof(MicaTextBox),
            new FrameworkPropertyMetadata(
                System.Windows.Media.Brushes.Black,
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

    /// <inheritdoc />
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

    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(MicaTextBox));

    public string? Watermark
    {
        get => (string)GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }
    static MicaTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaTextBox), new FrameworkPropertyMetadata(typeof(MicaTextBox)));
    }
}

namespace MicaWPF.Controls;
public class MicaTextBox : TextBox
{
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

namespace MicaWPF.Extension.Controls;
public class MicaRichTextBox : RichTextBox
{
    static MicaRichTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaRichTextBox), new FrameworkPropertyMetadata(typeof(MicaRichTextBox)));
    }
}

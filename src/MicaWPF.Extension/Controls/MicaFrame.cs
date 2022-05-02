namespace MicaWPF.Extension.Controls;
public class MicaFrame : Frame
{
    static MicaFrame()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaFrame), new FrameworkPropertyMetadata(typeof(MicaFrame)));
    }
}

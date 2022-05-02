namespace MicaWPF.Extension.Controls;
public class MicaProgressBar : ProgressBar
{
    static MicaProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaProgressBar), new FrameworkPropertyMetadata(typeof(MicaProgressBar)));
    }
}

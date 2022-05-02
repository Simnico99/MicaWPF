using System.Windows.Controls;

namespace MicaWPF.Controls;
public class MicaButton : Button
{
    static MicaButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaButton), new FrameworkPropertyMetadata(typeof(MicaButton)));
    }
}

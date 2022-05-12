namespace MicaWPF.Models;
public class MicaEnabledWindow
{
    public Window Window { get; }
    public BackdropType BackdropType { get; }
    public int CaptionHeight { get; }

    public MicaEnabledWindow(Window window, BackdropType backdropType, int captionHeight)
    {
        Window = window;
        CaptionHeight = captionHeight;
        BackdropType = backdropType;
    }
}

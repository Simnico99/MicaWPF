using MicaWPF.Controls;

namespace MicaWPF.Extensions;
public static class WindowExtension
{
    public static void EnableBackdrop(this Window window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.GetCurrent().EnableBackdrop(window, backdropType);
    }

    public static void EnableBackdrop(this MicaWindow window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.GetCurrent().EnableBackdrop(window, backdropType);
    }

    public static async Task RefreshContentAsync(this Window window) 
    {
       await Application.Current.Dispatcher.InvokeAsync(() => {
               foreach (var element in window.FindLogicalChildrens<FrameworkElement>())
               {
                   var savedStyle = element.Style;
                   element.Style = null;

                   element.UpdateDefaultStyle();

                   element.Style = savedStyle;
               }
       });
    }
}

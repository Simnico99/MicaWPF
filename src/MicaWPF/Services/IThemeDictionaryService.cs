using System.ComponentModel;

namespace MicaWPF.Services;
public interface IThemeDictionaryService
{
    Uri? ThemeSource { set; }

    event PropertyChangedEventHandler? PropertyChanged;

    void SetThemeSource(Uri source);
}

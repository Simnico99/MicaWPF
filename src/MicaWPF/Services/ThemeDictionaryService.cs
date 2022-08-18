using System.ComponentModel;
using System.Runtime.CompilerServices;
using MicaWPF.Extensions;

namespace MicaWPF.Services;

public class ThemeDictionaryService : INotifyPropertyChanged, IThemeDictionaryService
{
    private static readonly ThemeDictionaryService _themeManager = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private ThemeDictionaryService()
    {
    }

    public static ThemeDictionaryService GetCurrent()
    {
        return _themeManager;
    }

    /// <summary>
    /// get current theme resource dictionary
    /// </summary>
    /// <returns></returns>
    private static List<ResourceDictionary?> GetThemeResourceDictionary()
    {
        return (from dictionary in Application.Current.Resources.MergedDictionaries
                where dictionary.Contains("MicaWPF.Colors.ApplicationBackgroundColor")
                select dictionary).ToList();
    }

    /// <summary>
    /// set the current theme source
    /// </summary>
    /// <param name="source"></param>
    public void SetThemeSource(Uri source)
    {
        var oldThemes = GetThemeResourceDictionary();
        var dictionaries = Application.Current.Resources.MergedDictionaries;

        foreach (var MicaEnabledWindow in ThemeService.GetCurrent().MicaEnabledWindows)
        {
            foreach (var element in MicaEnabledWindow.Window.FindVisualChildrens<FrameworkElement>())
            {
                var savedStyle = element.Style;
                element.Style = null;

                element.UpdateDefaultStyle();

                element.Style = savedStyle;
            }
        }

        dictionaries.Add(new ResourceDictionary
        {
            Source = source
        });

        foreach (var oldTheme in oldThemes)
        {
            dictionaries.Remove(oldTheme);
        }
    }

    /// <summary>
    /// current theme source
    /// </summary>
    public Uri? ThemeSource
    {
        set
        {
            if (value != null)
            {
                SetThemeSource(value);
                OnPropertyChanged();
            }
        }
    }
}

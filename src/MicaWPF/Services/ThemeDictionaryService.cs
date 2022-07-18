using System.ComponentModel;
using System.Runtime.CompilerServices;

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
    private static ResourceDictionary? GetThemeResourceDictionary()
    {
        return (from dictionary in Application.Current.Resources.MergedDictionaries
                where dictionary.Contains("MicaWPF.Colors.ApplicationBackgroundColor")
                select dictionary).FirstOrDefault();
    }

    /// <summary>
    /// get source uri of current theme resource 
    /// </summary>
    /// <returns>resource uri</returns>
    private static Uri? GetThemeSource()
    {
        var theme = GetThemeResourceDictionary();
        if (theme == null)
            return null;
        return theme.Source;
    }

    /// <summary>
    /// set the current theme source
    /// </summary>
    /// <param name="source"></param>
    public void SetThemeSource(Uri source)
    {
        var oldTheme = GetThemeResourceDictionary();
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        dictionaries.Add(new ResourceDictionary
        {
            Source = source
        });
        if (oldTheme != null)
        {
            dictionaries.Remove(oldTheme);
        }
    }

    /// <summary>
    /// current theme source
    /// </summary>
    public Uri? ThemeSource
    {
        get => ThemeDictionaryService.GetThemeSource();
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

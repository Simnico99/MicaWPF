// <copyright file="ThemeDictionaryService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MicaWPF.Services;

/// <summary>
/// Service that manages the theme dictionnaries from MicaWPF.
/// </summary>
public sealed class ThemeDictionaryService : INotifyPropertyChanged, IThemeDictionaryService
{
    private static Uri? _currentThemeSource;

    private ThemeDictionaryService()
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets the current instance of <see cref="ThemeDictionaryService"/> but as the interface <see cref="IThemeDictionaryService"/>.
    /// </summary>
    public static IThemeDictionaryService Current { get; } = new ThemeDictionaryService();

    public Uri? ThemeSource
    {
        get => _currentThemeSource;
        set
        {
            if (value != null)
            {
                SetThemeSource(value);
                OnPropertyChanged();
            }
        }
    }

    public void SetThemeSource(Uri source)
    {
        lock (source)
        {
            _currentThemeSource = source;

            var oldThemes = GetThemeResourceDictionary();
            var dictionaries = Application.Current.Resources.MergedDictionaries;

            WindowHelper.RefreshAllWindowsContents();

            dictionaries.Add(new ResourceDictionary
            {
                Source = source,
            });

            foreach (var oldTheme in oldThemes)
            {
                _ = dictionaries.Remove(oldTheme);
            }
        }
    }

    public void RefreshThemeSource()
    {
        if (_currentThemeSource is not null)
        {
            SetThemeSource(_currentThemeSource);
            OnPropertyChanged();
        }
    }

    private static List<ResourceDictionary?> GetThemeResourceDictionary()
    {
        return (from dictionary in Application.Current.Resources.MergedDictionaries
                where dictionary.Contains("MicaWPF.Colors.ApplicationBackgroundColor")
                select dictionary).ToList();
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
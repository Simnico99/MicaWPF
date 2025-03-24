﻿// <copyright file="ThemeDictionaryService.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Styles;

namespace MicaWPF.Core.Services;

/// <summary>
/// Service that manages the theme dictionnaries from MicaWPF.
/// </summary>
public class ThemeDictionaryService : INotifyPropertyChanged, IThemeDictionaryService
{
    private static Uri? _currentThemeSource;

    public event PropertyChangedEventHandler? PropertyChanged;

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

    public WindowsTheme GetCurrentResourcesTheme()
    {
        var dictionnaries = Application.Current.Resources.MergedDictionaries;
        foreach (var dictionary in dictionnaries)
        {
            if (dictionary is not ThemeDictionaryBase themeDictionary)
            {
                continue;
            }

            return themeDictionary.Theme;
        }

        return WindowsTheme.Auto;
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
        var list = new List<ResourceDictionary?>();
        foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
        {
            if (dictionary.GetType().Name == "ThemeDictionary")
            {
                list.Add(dictionary);
            }
        }

        return list;
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
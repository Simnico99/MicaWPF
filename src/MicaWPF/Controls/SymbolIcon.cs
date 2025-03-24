﻿// <copyright file="SymbolIcon.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MicaWPF.Core.Extensions;
using MicaWPF.Core.Symbols;

namespace MicaWPF.Controls;

/// <summary>
/// A <see cref="FluentSystemIcons"/>.
/// </summary>
[ToolboxItem(true)]
public class SymbolIcon : Label
{
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(FluentSystemIcons.Regular), typeof(SymbolIcon), new PropertyMetadata(FluentSystemIcons.Regular.Empty, OnGlyphChanged));
    public static readonly DependencyProperty RawSymbolProperty = DependencyProperty.Register(nameof(RawSymbol), typeof(string), typeof(SymbolIcon), new PropertyMetadata("\uEA01"));
    public static readonly DependencyProperty FilledProperty = DependencyProperty.Register(nameof(Filled), typeof(bool), typeof(SymbolIcon), new PropertyMetadata(false, OnGlyphChanged));

    /// <summary>
    /// Gets or sets the current Symbol.
    /// </summary>
    public FluentSystemIcons.Regular Symbol
    {
        get => (FluentSystemIcons.Regular)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// Gets the current symbole as a <see cref="string"/>.
    /// </summary>
    public string RawSymbol => (string)GetValue(RawSymbolProperty);

    /// <summary>
    /// Gets or sets a value indicating whether is it a filled icon or not.
    /// </summary>
    public bool Filled
    {
        get => (bool)GetValue(FilledProperty);
        set => SetValue(FilledProperty, value);
    }

    private static void OnGlyphChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependency is not SymbolIcon control)
        {
            return;
        }

        if ((bool)control.GetValue(FilledProperty))
        {
            control.SetValue(RawSymbolProperty, control.Symbol.Swap().GetString());
        }
        else
        {
            control.SetValue(RawSymbolProperty, control.Symbol.GetString());
        }
    }
}

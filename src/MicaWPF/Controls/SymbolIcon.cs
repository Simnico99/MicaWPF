using System.ComponentModel;
using System.Text;
using MicaWPF.Extensions;
using MicaWPF.Symbols;

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
    /// The current Symbol.
    /// </summary>
    public FluentSystemIcons.Regular Symbol
    {
        get => (FluentSystemIcons.Regular)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// The current symbole as a <see cref="string"/>.
    /// </summary>
    public string RawSymbol => (string)GetValue(RawSymbolProperty);

    /// <summary>
    /// Is it a filled icon or not.
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

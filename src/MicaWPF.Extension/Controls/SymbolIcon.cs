using System.Text;
using MicaWPF.Expansion.InternalExtensions;
using MicaWPF.Expansion.Symbols;

namespace MicaWPF.Expansion.Controls;
public class SymbolIcon : Label
{
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(FluentSystemIcons.Regular), typeof(SymbolIcon), new PropertyMetadata(FluentSystemIcons.Regular.Empty, OnGlyphChanged));
    public static readonly DependencyProperty RawSymbolProperty = DependencyProperty.Register(nameof(RawSymbol), typeof(string), typeof(SymbolIcon), new PropertyMetadata("\uEA01"));
    public static readonly DependencyProperty FilledProperty = DependencyProperty.Register(nameof(Filled), typeof(bool), typeof(SymbolIcon), new PropertyMetadata(false, OnGlyphChanged));

    public FluentSystemIcons.Regular Symbol
    {
        get => (FluentSystemIcons.Regular)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    public string RawSymbol => (string)GetValue(RawSymbolProperty);

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

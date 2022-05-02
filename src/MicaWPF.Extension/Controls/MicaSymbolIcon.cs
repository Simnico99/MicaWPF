using System.Text;
using MicaWPF.Extension.InternalExtensions;
using MicaWPF.Extension.Symbols;

namespace MicaWPF.Extension.Controls;
public class MicaSymbolIcon : Label
{
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(FluentSystemIcons.Regular), typeof(MicaSymbolIcon), new PropertyMetadata(FluentSystemIcons.Regular.Empty, OnGlyphChanged));

    public static readonly DependencyProperty RawSymbolProperty = DependencyProperty.Register(nameof(RawSymbol), typeof(string), typeof(MicaSymbolIcon), new PropertyMetadata("\uEA01"));

    public static readonly DependencyProperty FilledProperty = DependencyProperty.Register(nameof(Filled), typeof(bool), typeof(MicaSymbolIcon), new PropertyMetadata(false, OnGlyphChanged));

    static MicaSymbolIcon() 
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaSymbolIcon), new FrameworkPropertyMetadata(typeof(MicaSymbolIcon)));
    }

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
        if (dependency is not MicaSymbolIcon control)
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

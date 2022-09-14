using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Controls;

/// <summary>
/// A border that has a gradient based on the current theme.
/// </summary>
[ToolboxItem(true)]
public class GradientBorder : UserControl
{
    public static readonly DependencyProperty BottomBorderBrushProperty = DependencyProperty.Register(nameof(BottomBorderBrush), typeof(Brush), typeof(GradientBorder));
    public static readonly DependencyProperty TopBorderBrushProperty = DependencyProperty.Register(nameof(TopBorderBrush), typeof(Brush), typeof(GradientBorder));
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(GradientBorder));

    public Brush? TopBorderBrush
    {
        get => (Brush)GetValue(BottomBorderBrushProperty);
        set => SetValue(BottomBorderBrushProperty, value);
    }

    public Brush? BottomBorderBrush
    {
        get => (Brush)GetValue(BottomBorderBrushProperty);
        set => SetValue(BottomBorderBrushProperty, value);
    }

    public CornerRadius? CornerRadius
    {
        get => (CornerRadius)GetValue(BottomBorderBrushProperty);
        set => SetValue(BottomBorderBrushProperty, value);
    }
}

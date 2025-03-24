// <copyright file="ColumnDefinitionExtended.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Controls;

namespace MicaWPF.Controls;

/// <summary>
/// Extended ColumnDefinition.
/// </summary>
internal sealed class ColumnDefinitionExtended : ColumnDefinition
{
    private static readonly DependencyProperty _visibleProperty;

    static ColumnDefinitionExtended()
    {
        _visibleProperty = DependencyProperty.Register("Visible", typeof(bool), typeof(ColumnDefinitionExtended), new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));
        WidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null, new CoerceValueCallback(CoerceWidth)));
        MinWidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended), new FrameworkPropertyMetadata(0D, null, new CoerceValueCallback(CoerceMinWidth)));
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column is visible.
    /// </summary>
    public bool Visible
    {
        get => (bool)GetValue(_visibleProperty);
        set => SetValue(_visibleProperty, value);
    }

    /// <summary>
    /// Sets the Visible property for a given DependencyObject.
    /// </summary>
    /// <param name="obj">The DependencyObject whose property is set.</param>
    /// <param name="nVisible">The value to set.</param>
    public static void SetVisible(DependencyObject obj, bool nVisible)
    {
        obj.SetValue(_visibleProperty, nVisible);
    }

    /// <summary>
    /// Gets the Visible property for a given DependencyObject.
    /// </summary>
    /// <param name="obj">The DependencyObject whose property is retrieved.</param>
    /// <returns>The value of the Visible property.</returns>
    public static bool GetVisible(DependencyObject obj)
    {
        return (bool)obj.GetValue(_visibleProperty);
    }

    private static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        obj.CoerceValue(WidthProperty);
        obj.CoerceValue(MinWidthProperty);
    }

    private static object CoerceWidth(DependencyObject obj, object nValue)
    {
        return ((ColumnDefinitionExtended)obj).Visible ? nValue : new GridLength(0);
    }

    private static object CoerceMinWidth(DependencyObject obj, object nValue)
    {
        return ((ColumnDefinitionExtended)obj).Visible ? nValue : 0D;
    }
}

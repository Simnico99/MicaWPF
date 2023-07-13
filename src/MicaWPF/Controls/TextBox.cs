// <copyright file="TextBox.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.ComponentModel;
using MicaWPF.Symbols;

namespace MicaWPF.Controls;

/// <summary>
/// A WinUI TextBox based on <see cref="System.Windows.Controls.TextBox"/>.
/// </summary>
[ToolboxItem(true)]
public class TextBox : System.Windows.Controls.TextBox
{
    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(TextBox));
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FluentSystemIcons.Regular), typeof(System.Windows.Controls.TextBox), new PropertyMetadata(FluentSystemIcons.Regular.Empty));
    public static readonly DependencyProperty IconPositionProperty = DependencyProperty.Register(nameof(IconPosition), typeof(ElementPosition), typeof(TextBox), new PropertyMetadata(ElementPosition.Right));
    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled), typeof(bool), typeof(TextBox), new PropertyMetadata(false));
    public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.RegisterAttached(nameof(IconForeground), typeof(Brush), typeof(TextBox), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets or sets the icon shown in the TextBox button.
    /// </summary>
    public FluentSystemIcons.Regular Icon
    {
        get => (FluentSystemIcons.Regular)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets icon is on the right or left.
    /// </summary>
    public ElementPosition IconPosition
    {
        get => (ElementPosition)GetValue(IconPositionProperty);
        set => SetValue(IconPositionProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether is the Icon filled or not.
    /// </summary>
    public bool IconFilled
    {
        get => (bool)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }

    /// <summary>
    /// Gets or sets the foreground of the icon.
    /// </summary>
    public Brush IconForeground
    {
        get => (Brush)GetValue(IconForegroundProperty);
        set => SetValue(IconForegroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a placeholder text for the TextBox.
    /// </summary>
    public string? Watermark
    {
        get => (string)GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }
}

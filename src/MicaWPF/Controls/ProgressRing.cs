// <copyright file="ProgressRing.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.ComponentModel;
using System.Windows.Controls;

namespace MicaWPF.Controls;

/// <summary>
/// A WinUI <see cref="ProgressRing"/>.
/// </summary>
[ToolboxItem(true)]
public class ProgressRing : Control
{
    public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(nameof(Progress), typeof(double), typeof(ProgressRing), new PropertyMetadata(50d, PropertyChangedCallback));

    public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(nameof(IsIndeterminate), typeof(bool), typeof(ProgressRing), new PropertyMetadata(false));

    public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(ProgressRing), new PropertyMetadata(180.0d));

    public static readonly DependencyProperty IndeterminateAngleProperty = DependencyProperty.Register(nameof(IndeterminateAngle), typeof(double), typeof(ProgressRing), new PropertyMetadata(180.0d));

    public static readonly DependencyProperty CoverRingStrokeProperty = DependencyProperty.RegisterAttached(nameof(CoverRingStroke), typeof(Brush), typeof(ProgressRing), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits));

    public static readonly DependencyProperty CoverRingVisibilityProperty = DependencyProperty.Register(nameof(CoverRingVisibility), typeof(Visibility), typeof(ProgressRing), new PropertyMetadata(Visibility.Visible));

    /// <summary>
    /// Gets or sets the current progress.
    /// </summary>
    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether is inderteminate.
    /// </summary>
    public bool IsIndeterminate
    {
        get => (bool)GetValue(IsIndeterminateProperty);
        set => SetValue(IsIndeterminateProperty, value);
    }

    /// <summary>
    /// Gets or sets the end angle.
    /// </summary>
    public double EndAngle
    {
        get => (double)GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }

    /// <summary>
    /// Gets the angle when using the inderteminate progress.
    /// </summary>
    public double IndeterminateAngle
    {
        get => (double)GetValue(IndeterminateAngleProperty);
        internal set => SetValue(IndeterminateAngleProperty, value);
    }

    /// <summary>
    /// Gets the background of the ring.
    /// </summary>
    public Brush CoverRingStroke
    {
        get => (Brush)GetValue(CoverRingStrokeProperty);
        internal set => SetValue(CoverRingStrokeProperty, value);
    }

    /// <summary>
    /// Gets is the background ring visible.
    /// </summary>
    public Visibility CoverRingVisibility
    {
        get => (Visibility)GetValue(CoverRingVisibilityProperty);
        internal set => SetValue(CoverRingVisibilityProperty, value);
    }

    protected static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ProgressRing control)
        {
            return;
        }

        control.UpdateProgressAngle();
    }

    protected void UpdateProgressAngle()
    {
        var percentage = Progress;

        if (percentage > 100)
        {
            percentage = 100;
        }

        if (percentage < 0)
        {
            percentage = 0;
        }

        var endAngle = 3.6d * percentage;

        if (endAngle >= 360)
        {
            endAngle = 359;
        }

        EndAngle = endAngle;
    }
}

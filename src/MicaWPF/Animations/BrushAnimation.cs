// <copyright file="BrushAnimation.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Windows.Media.Animation;

namespace MicaWPF.Animations;

/// <summary>
/// Interpolation animation between 2 brushes.
/// </summary>
public sealed class BrushAnimation : AnimationTimeline
{
    public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Brush), typeof(BrushAnimation));
    public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Brush), typeof(BrushAnimation));

    /// <summary>
    /// Gets or sets brush to start the animation from.
    /// </summary>
    public Brush From
    {
        get => (Brush)GetValue(FromProperty);
        set => SetValue(FromProperty, value);
    }

    /// <summary>
    /// Gets or sets brush to end the animation to.
    /// </summary>
    public Brush To
    {
        get => (Brush)GetValue(ToProperty);
        set => SetValue(ToProperty, value);
    }

    public override Type TargetPropertyType => typeof(Brush);

    public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
    {
        return GetCurrentValue((Brush)defaultOriginValue, (Brush)defaultDestinationValue, animationClock);
    }

    /// <summary>
    /// Gets the current value of the animation.
    /// </summary>
    /// <param name="defaultOriginValue">Origin brush.</param>
    /// <param name="defaultDestinationValue">Destination brush.</param>
    /// <param name="animationClock">Animation Clock.</param>
    /// <returns>The value this animation believes should be the current value for the property.</returns>
    public object GetCurrentValue(Brush defaultOriginValue, Brush defaultDestinationValue, AnimationClock animationClock)
    {
        if (!animationClock.CurrentProgress.HasValue)
        {
            return Brushes.Transparent;
        }

        defaultOriginValue = From ?? defaultOriginValue;
        defaultDestinationValue = To ?? defaultDestinationValue;

        if (animationClock.CurrentProgress.Value == 0)
        {
            return defaultOriginValue;
        }

        if (animationClock.CurrentProgress.Value == 1)
        {
            return defaultDestinationValue;
        }

        var colorOriginValue = ((SolidColorBrush)defaultOriginValue).Color;
        var colorDestinationValue = ((SolidColorBrush)defaultDestinationValue).Color;

        return new SolidColorBrush(ColorHelper.InterpolateBetween(colorOriginValue, colorDestinationValue, animationClock.CurrentProgress.Value));
    }

    protected override Freezable CreateInstanceCore()
    {
        return new BrushAnimation();
    }
}

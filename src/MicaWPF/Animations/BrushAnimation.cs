using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MicaWPF.Animations;

/// <summary>
/// Interpolation animation between 2 brushes.
/// </summary>
public sealed class BrushAnimation : AnimationTimeline
{
    public override Type TargetPropertyType
    {
        get
        {
            return typeof(Brush);
        }
    }

    public override object GetCurrentValue(object defaultOriginValue,
                                           object defaultDestinationValue,
                                           AnimationClock animationClock)
    {
        return GetCurrentValue((Brush)defaultOriginValue,
                               (Brush)defaultDestinationValue,
                               animationClock);
    }

    /// <summary>
    /// Gets the current value of the animation.
    /// </summary>
    /// <param name="defaultOriginValue">Origin brush</param>
    /// <param name="defaultDestinationValue">Destination brush</param>
    /// <param name="animationClock">Animation Clock</param>
    /// <returns>The value this animation believes should be the current value for the property</returns>
    public object GetCurrentValue(Brush defaultOriginValue,
                                  Brush defaultDestinationValue,
                                  AnimationClock animationClock)
    {
        if (!animationClock.CurrentProgress.HasValue)
        {
            return Brushes.Transparent;
        }

        defaultOriginValue = this.From ?? defaultOriginValue;
        defaultDestinationValue = this.To ?? defaultDestinationValue;

        if (animationClock.CurrentProgress.Value == 0)
        {
            return defaultOriginValue;
        }

        if (animationClock.CurrentProgress.Value == 1)
        {
            return defaultDestinationValue;
        }

        var ColorOriginValue = ((SolidColorBrush)defaultOriginValue).Color;
        var ColorDestinationValue = ((SolidColorBrush)defaultDestinationValue).Color;

        return new SolidColorBrush(ColorHelper.InterpolateBetween(ColorOriginValue, ColorDestinationValue, animationClock.CurrentProgress.Value));
    }

    protected override Freezable CreateInstanceCore()
    {
        return new BrushAnimation();
    }

    //we must define From and To, AnimationTimeline does not have this properties
    /// <summary>
    /// Brush to start the animation from.
    /// </summary>
    public Brush From
    {
        get { return (Brush)GetValue(FromProperty); }
        set { SetValue(FromProperty, value); }
    }

    /// <summary>
    /// Brush to end the animation to.
    /// </summary>
    public Brush To
    {
        get { return (Brush)GetValue(ToProperty); }
        set { SetValue(ToProperty, value); }
    }

    public static readonly DependencyProperty FromProperty =
        DependencyProperty.Register("From", typeof(Brush), typeof(BrushAnimation));
    public static readonly DependencyProperty ToProperty =
        DependencyProperty.Register("To", typeof(Brush), typeof(BrushAnimation));
}

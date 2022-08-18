using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MicaWPF.Animations;
public class BrushAnimation : AnimationTimeline
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
    public object GetCurrentValue(Brush defaultOriginValue,
                                  Brush defaultDestinationValue,
                                  AnimationClock animationClock)
    {
        if (!animationClock.CurrentProgress.HasValue)
            return Brushes.Transparent;

        //use the standard values if From and To are not set 
        //(it is the value of the given property)
        defaultOriginValue = this.From ?? defaultOriginValue;
        defaultDestinationValue = this.To ?? defaultDestinationValue;

        if (animationClock.CurrentProgress.Value == 0)
            return defaultOriginValue;
        if (animationClock.CurrentProgress.Value == 1)
            return defaultDestinationValue;

        var ColorOriginValue = ((SolidColorBrush)defaultOriginValue).Color;
        var ColorDestinationValue = ((SolidColorBrush)defaultDestinationValue).Color;

        return new VisualBrush(new Border()
        {
            Width = 1,
            Height = 1,
            //Background = defaultOriginValue,
            Background = new SolidColorBrush(ColorHelper.InterpolateBetween(ColorOriginValue, ColorDestinationValue, animationClock.CurrentProgress.Value)),
            //Child = new Border()
            //{
            //    Background = defaultDestinationValue,
            //    Opacity = ((ColorDestinationValue.A / 255.0F) * animationClock.CurrentProgress.Value),
            //}
        });
    }

    protected override Freezable CreateInstanceCore()
    {
        return new BrushAnimation();
    }

    //we must define From and To, AnimationTimeline does not have this properties
    public Brush From
    {
        get { return (Brush)GetValue(FromProperty); }
        set { SetValue(FromProperty, value); }
    }
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

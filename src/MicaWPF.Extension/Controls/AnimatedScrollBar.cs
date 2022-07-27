using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using MicaWPF.Extension.Models;
using System.Windows.Input;

namespace MicaWPF.Extension.Controls;
public class AnimatedScrollBar : ScrollBar
{
    private bool _isScrolling = false;

    private bool _isInteracted = false;

    private readonly EventIdentifier _interactiveIdentifier = new();

    public static readonly DependencyProperty IsScrollingProperty = DependencyProperty.Register(nameof(IsScrolling),
        typeof(bool), typeof(AnimatedScrollBar), new PropertyMetadata(false, IsScrollingProperty_OnChange));

    public static readonly DependencyProperty IsInteractedProperty = DependencyProperty.Register(
        nameof(IsInteracted),
        typeof(bool), typeof(AnimatedScrollBar), new PropertyMetadata(false, IsInteractedProperty_OnChange));

    public static readonly DependencyProperty TimeoutProperty = DependencyProperty.Register(nameof(Timeout),
        typeof(int), typeof(AnimatedScrollBar), new PropertyMetadata(1000));

    public bool IsScrolling
    {
        get => (bool)GetValue(IsScrollingProperty);
        set => SetValue(IsScrollingProperty, value);
    }

    public bool IsInteracted
    {
        get => (bool)GetValue(IsInteractedProperty);
        set
        {
            if ((bool)GetValue(IsInteractedProperty) != value)
                SetValue(IsInteractedProperty, value);
        }
    }

    public int Timeout
    {
        get => (int)GetValue(TimeoutProperty);
        set => SetValue(TimeoutProperty, value);
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);

        UpdateScroll().GetAwaiter();
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);

        UpdateScroll().GetAwaiter();
    }

    private async Task UpdateScroll()
    {
        var currentEvent = _interactiveIdentifier.GetNext();
        var shouldScroll = IsMouseOver || _isScrolling;

        if (shouldScroll == _isInteracted)
            return;

        if (!shouldScroll)
            await Task.Delay(Timeout);

        if (!_interactiveIdentifier.IsEqual(currentEvent))
            return;

        IsInteracted = shouldScroll;
    }

    private static void IsScrollingProperty_OnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnimatedScrollBar bar)
            return;

        if (bar._isScrolling == bar.IsScrolling)
            return;

        bar._isScrolling = !bar._isScrolling;

        bar.UpdateScroll().GetAwaiter();
    }

    private static void IsInteractedProperty_OnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnimatedScrollBar bar)
            return;

        if (bar._isInteracted == bar.IsInteracted)
            return;

        bar._isInteracted = !bar._isInteracted;

        bar.UpdateScroll().GetAwaiter();
    }
}

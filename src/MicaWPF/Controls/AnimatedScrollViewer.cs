namespace MicaWPF.Controls;
public class AnimatedScrollViewer : ScrollViewer
{
    private readonly EventIdentifier _verticalIdentifier = new();

    private readonly EventIdentifier _horizontalIdentifier = new();

    private bool _scrollingVertically = false;

    private bool _scrollingHorizontally = false;

    private int _timeout = 1200;

    private double _minimalChange = 40d;

    public static readonly DependencyProperty IsScrollingVerticallyProperty = DependencyProperty.Register(
        nameof(IsScrollingVertically),
        typeof(bool), typeof(AnimatedScrollViewer),
        new PropertyMetadata(false, IsScrollingVerticallyProperty_OnChanged));

    public static readonly DependencyProperty IsScrollingHorizontallyProperty = DependencyProperty.Register(
        nameof(IsScrollingHorizontally),
        typeof(bool), typeof(AnimatedScrollViewer), new PropertyMetadata(false, IsScrollingHorizontally_OnChanged));

    public static readonly DependencyProperty MinimalChangeProperty = DependencyProperty.Register(
        nameof(MinimalChange),
        typeof(double), typeof(AnimatedScrollViewer), new PropertyMetadata(40d, MinimalChangeProperty_OnChanged));

    public static readonly DependencyProperty TimeoutProperty = DependencyProperty.Register(nameof(Timeout),
        typeof(int), typeof(AnimatedScrollViewer), new PropertyMetadata(1200, TimeoutProperty_OnChanged));

    public bool IsScrollingVertically
    {
        get => (bool)GetValue(IsScrollingVerticallyProperty);
        set => SetValue(IsScrollingVerticallyProperty, value);
    }

    public bool IsScrollingHorizontally
    {
        get => (bool)GetValue(IsScrollingHorizontallyProperty);
        set => SetValue(IsScrollingHorizontallyProperty, value);
    }

    public double MinimalChange
    {
        get => (double)GetValue(MinimalChangeProperty);
        set => SetValue(MinimalChangeProperty, value);
    }

    public int Timeout
    {
        get => (int)GetValue(TimeoutProperty);
        set => SetValue(TimeoutProperty, value);
    }

    protected override void OnScrollChanged(ScrollChangedEventArgs e)
    {
        base.OnScrollChanged(e);

        if (e.HorizontalChange > _minimalChange || e.HorizontalChange < -_minimalChange)
            UpdateHorizontalScrollingState();

        if (e.VerticalChange > _minimalChange || e.VerticalChange < -_minimalChange)
            UpdateVerticalScrollingState();
    }

    private async void UpdateVerticalScrollingState()
    {
        var currentEvent = _verticalIdentifier.GetNext();

        if (!_scrollingVertically)
            IsScrollingVertically = true;

        if (_timeout > -1)
            await Task.Delay(_timeout < 10000 ? _timeout : 1000);

        if (_verticalIdentifier.IsEqual(currentEvent) && _scrollingVertically)
            IsScrollingVertically = false;
    }

    private async void UpdateHorizontalScrollingState()
    {
        var currentEvent = _horizontalIdentifier.GetNext();

        if (!_scrollingHorizontally)
            IsScrollingHorizontally = true;

        await Task.Delay(Timeout < 10000 ? Timeout : 1000);

        if (_horizontalIdentifier.IsEqual(currentEvent) && _scrollingHorizontally)
            IsScrollingHorizontally = false;
    }

    private static void IsScrollingVerticallyProperty_OnChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnimatedScrollViewer scroll)
            return;

        scroll._scrollingVertically = scroll.IsScrollingVertically;
    }

    private static void IsScrollingHorizontally_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnimatedScrollViewer scroll)
            return;

        scroll._scrollingHorizontally = scroll.IsScrollingHorizontally;
    }

    private static void MinimalChangeProperty_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnimatedScrollViewer scroll)
            return;

        scroll._minimalChange = scroll.MinimalChange;
    }

    private static void TimeoutProperty_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnimatedScrollViewer scroll)
            return;

        scroll._timeout = scroll.Timeout;
    }
}

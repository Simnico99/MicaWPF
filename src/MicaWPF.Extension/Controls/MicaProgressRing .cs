namespace MicaWPF.Extension.Controls;
public class MicaProgressRing : Control
{
    public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(nameof(Progress),
        typeof(double), typeof(MicaProgressRing),
        new PropertyMetadata(50d, PropertyChangedCallback));

    public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
        nameof(IsIndeterminate),
        typeof(bool), typeof(MicaProgressRing),
        new PropertyMetadata(false));

    public static readonly DependencyProperty EngAngleProperty = DependencyProperty.Register(nameof(EngAngle),
        typeof(double), typeof(MicaProgressRing),
        new PropertyMetadata(180.0d));

    public static readonly DependencyProperty IndeterminateAngleProperty = DependencyProperty.Register(
        nameof(IndeterminateAngle),
        typeof(double), typeof(MicaProgressRing),
        new PropertyMetadata(180.0d));

    public static readonly DependencyProperty CoverRingStrokeProperty =
        DependencyProperty.RegisterAttached(
            nameof(CoverRingStroke),
            typeof(Brush),
            typeof(MicaProgressRing),
            new FrameworkPropertyMetadata(
                Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender |
                FrameworkPropertyMetadataOptions.Inherits));

    public static readonly DependencyProperty CoverRingVisibilityProperty = DependencyProperty.Register(
        nameof(CoverRingVisibility),
        typeof(System.Windows.Visibility), typeof(MicaProgressRing),
        new PropertyMetadata(System.Windows.Visibility.Visible));

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public bool IsIndeterminate
    {
        get => (bool)GetValue(IsIndeterminateProperty);
        set => SetValue(IsIndeterminateProperty, value);
    }

    public double EngAngle
    {
        get => (double)GetValue(EngAngleProperty);
        set => SetValue(EngAngleProperty, value);
    }

    public double IndeterminateAngle
    {
        get => (double)GetValue(IndeterminateAngleProperty);
        internal set => SetValue(IndeterminateAngleProperty, value);
    }

    public Brush CoverRingStroke
    {
        get => (Brush)GetValue(CoverRingStrokeProperty);
        internal set => SetValue(CoverRingStrokeProperty, value);
    }

    public System.Windows.Visibility CoverRingVisibility
    {
        get => (System.Windows.Visibility)GetValue(CoverRingVisibilityProperty);
        internal set => SetValue(CoverRingVisibilityProperty, value);
    }

    protected void UpdateProgressAngle()
    {
        var percentage = Progress;

        if (percentage > 100)
            percentage = 100;

        if (percentage < 0)
            percentage = 0;

        var endAngle = 3.6d * percentage;

        if (endAngle >= 360)
            endAngle = 359;

        EngAngle = endAngle;
    }

    protected static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MicaProgressRing control)
            return;

        control.UpdateProgressAngle();
    }
}

﻿using System.Windows.Shapes;

namespace MicaWPF.Extension.Controls;
public class MicaArc : Shape
{
    public static readonly DependencyProperty StartAngleProperty =
        DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(MicaArc),
            new PropertyMetadata(0.0d, PropertyChangedCallback));

    public static readonly DependencyProperty EndAngleProperty =
        DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(MicaArc),
            new PropertyMetadata(0.0d, PropertyChangedCallback));

    public double StartAngle
    {
        get => (double)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    public double EndAngle
    {
        get => (double)GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }

    public bool IsLargeArc { get; internal set; } = false;

    protected override Geometry DefiningGeometry => GetDefiningGeometry();

    static MicaArc()
    {
        StrokeStartLineCapProperty.OverrideMetadata(
            typeof(MicaArc),
            new FrameworkPropertyMetadata(PenLineCap.Round)
        );

        StrokeEndLineCapProperty.OverrideMetadata(
            typeof(MicaArc),
            new FrameworkPropertyMetadata(PenLineCap.Round)
        );
    }

    protected Geometry GetDefiningGeometry()
    {
        var geometryStream = new StreamGeometry();
        var arcSize = new Size(
            Math.Max(0, (RenderSize.Width - StrokeThickness) / 2),
            Math.Max(0, (RenderSize.Height - StrokeThickness) / 2)
        );

        using (StreamGeometryContext context = geometryStream.Open())
        {
            context.BeginFigure(
                PointAtAngle(Math.Min(StartAngle, EndAngle)),
                false,
                false
            );

            context.ArcTo(
                PointAtAngle(Math.Max(StartAngle, EndAngle)),
                arcSize,
                0,
                IsLargeArc,
                SweepDirection.Counterclockwise,
                true,
                false
            );
        }

        geometryStream.Transform = new TranslateTransform(StrokeThickness / 2, StrokeThickness / 2);

        return geometryStream;
    }

    protected Point PointAtAngle(double angle)
    {
        var radAngle = angle * (Math.PI / 180);
        var xRadius = (RenderSize.Width - StrokeThickness) / 2;
        var yRadius = (RenderSize.Height - StrokeThickness) / 2;

        return new Point(xRadius + xRadius * Math.Cos(radAngle), yRadius - yRadius * Math.Sin(radAngle));
    }

    protected static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MicaArc control)
        {
            return;
        }

        control.IsLargeArc = Math.Abs(control.EndAngle - control.StartAngle) > 180;
        control.InvalidateVisual();
    }
}

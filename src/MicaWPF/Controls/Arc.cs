// <copyright file="Arc.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.ComponentModel;
using System.Windows.Shapes;

namespace MicaWPF.Controls;

/// <summary>
/// An arc used to make the <see cref="ProgressRing"/>.
/// </summary>
[ToolboxItem(false)]
public class Arc : Shape
{
    public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(Arc), new PropertyMetadata(0.0d, PropertyChangedCallback));

    public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(Arc), new PropertyMetadata(0.0d, PropertyChangedCallback));

    static Arc()
    {
        StrokeStartLineCapProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(PenLineCap.Round));
        StrokeEndLineCapProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(PenLineCap.Round));
    }

    /// <summary>
    /// Gets or sets the starting angle.
    /// </summary>
    public double StartAngle
    {
        get => (double)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    /// <summary>
    /// Gets or sets the ending angle.
    /// </summary>
    public double EndAngle
    {
        get => (double)GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether is it a large arc.
    /// </summary>
    public bool IsLargeArc { get; internal set; } = false;

    protected override Geometry DefiningGeometry => GetDefiningGeometry();

    protected static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Arc control)
        {
            return;
        }

        control.IsLargeArc = Math.Abs(control.EndAngle - control.StartAngle) > 180;
        control.InvalidateVisual();
    }

    protected Geometry GetDefiningGeometry()
    {
        var geometryStream = new StreamGeometry();
        var arcSize = new Size(Math.Max(0, (RenderSize.Width - StrokeThickness) / 2), Math.Max(0, (RenderSize.Height - StrokeThickness) / 2));

        using (var context = geometryStream.Open())
        {
            context.BeginFigure(PointAtAngle(Math.Min(StartAngle, EndAngle)), false, false);
            context.ArcTo(PointAtAngle(Math.Max(StartAngle, EndAngle)), arcSize, 0, IsLargeArc, SweepDirection.Counterclockwise, true, false);
        }

        geometryStream.Transform = new TranslateTransform(StrokeThickness / 2, StrokeThickness / 2);

        return geometryStream;
    }

    protected Point PointAtAngle(double angle)
    {
        var radAngle = angle * (Math.PI / 180);
        var xRadius = (RenderSize.Width - StrokeThickness) / 2;
        var yRadius = (RenderSize.Height - StrokeThickness) / 2;

        return new Point(xRadius + (xRadius * Math.Cos(radAngle)), yRadius - (yRadius * Math.Sin(radAngle)));
    }
}

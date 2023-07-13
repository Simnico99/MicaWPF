// <copyright file="DpiHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Interop;

namespace MicaWPF.Core.Helpers;

/// <summary>
/// An helper class to help with DPI.
/// </summary>
public static class DpiHelper
{
    private const double _logicalDpi = 96.0;

    [ThreadStatic]
    private static Matrix _transformToDip;

    static DpiHelper()
    {
        var dC = InteropMethods.GetDC(PtrHelper.Zero);
        if (dC != PtrHelper.Zero)
        {
            const int logicPixelsX = 88;
            const int logicPixelsY = 90;
            _ = DeviceDpiX = InteropMethods.GetDeviceCaps(dC, logicPixelsX);
            _ = DeviceDpiY = InteropMethods.GetDeviceCaps(dC, logicPixelsY);
            _ = InteropMethods.ReleaseDC(PtrHelper.Zero, dC);
        }
        else
        {
            DeviceDpiX = _logicalDpi;
            DeviceDpiY = _logicalDpi;
        }

        var identity = Matrix.Identity;
        var identity2 = Matrix.Identity;
        identity.Scale(DeviceDpiX / _logicalDpi, DeviceDpiY / _logicalDpi);
        identity2.Scale(_logicalDpi / DeviceDpiX, _logicalDpi / DeviceDpiY);
        TransformFromDevice = new MatrixTransform(identity2);
        TransformFromDevice.Freeze();
        TransformToDevice = new MatrixTransform(identity);
        TransformToDevice.Freeze();
    }

    /// <summary>
    /// Gets the current device.
    /// </summary>
    public static MatrixTransform TransformFromDevice { get; }

    /// <summary>
    /// Gets the destination device.
    /// </summary>
    public static MatrixTransform TransformToDevice { get; }

    /// <summary>
    /// Gets the device DPI X value.
    /// </summary>
    public static double DeviceDpiX { get; }

    /// <summary>
    /// Gets the device DPI Y value.
    /// </summary>
    public static double DeviceDpiY { get; }

    /// <summary>
    /// Gets logical value to Unit Scaling X.
    /// </summary>
    public static double LogicalToDeviceUnitsScalingFactorX => TransformToDevice.Matrix.M11;

    /// <summary>
    /// Gets logical value to Unit Scaling Y.
    /// </summary>
    public static double LogicalToDeviceUnitsScalingFactorY => TransformToDevice.Matrix.M22;

    /// <summary>
    /// Pixel point to Logical size.
    /// </summary>
    /// <returns>Logical size.</returns>
    public static Point DevicePixelsToLogical(Point devicePoint, double dpiScaleX, double dpiScaleY)
    {
        _transformToDip = Matrix.Identity;
        _transformToDip.Scale(1d / dpiScaleX, 1d / dpiScaleY);
        return _transformToDip.Transform(devicePoint);
    }

    /// <summary>
    /// Pixel size to Logical size.
    /// </summary>
    /// <returns>Logical size.</returns>
    public static Size DeviceSizeToLogical(Size deviceSize, double dpiScaleX, double dpiScaleY)
    {
        var pt = DevicePixelsToLogical(new Point(deviceSize.Width, deviceSize.Height), dpiScaleX, dpiScaleY);
        return new Size(pt.X, pt.Y);
    }

    /// <summary>
    /// Logical size to pixel.
    /// </summary>
    /// <returns>Pixel size.</returns>
    public static Rect LogicalToDeviceUnits(this Rect logicalRect)
    {
        var result = logicalRect;
        result.Transform(TransformToDevice.Matrix);
        return result;
    }

    /// <summary>
    /// Logical size to pixel.
    /// </summary>
    /// <returns>Pixel size.</returns>
    public static Rect DeviceToLogicalUnits(this Rect deviceRect)
    {
        var result = deviceRect;
        result.Transform(TransformFromDevice.Matrix);
        return result;
    }

    /// <summary>
    /// Roound value of dpi.
    /// </summary>
    /// <returns>The rounded value.</returns>
    public static double RoundLayoutValue(double value, double dpiScale)
    {
        double newValue;

        if (!MathHelper.AreClose(dpiScale, 1.0))
        {
            newValue = Math.Round(value * dpiScale) / dpiScale;
            if (double.IsNaN(newValue) || double.IsInfinity(newValue) || MathHelper.AreClose(newValue, double.MaxValue))
            {
                newValue = value;
            }
        }
        else
        {
            newValue = Math.Round(value);
        }

        return newValue;
    }
}

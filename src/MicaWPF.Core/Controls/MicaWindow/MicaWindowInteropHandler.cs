// <copyright file="MicaWindowInteropHandler.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Interop;
using System.Windows.Media;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Interop;

namespace MicaWPF.Core.Controls.MicaWindow;

public class MicaWindowInteropHandler : MicaWindowProperty
{
    protected const int _hTMAXBUTTON = 9;
    protected const string _buttonMaxName = "Maximize";
    protected const string _buttonRestoreName = "Restore";

    private static readonly SolidColorBrush _transparentBrush = new(Color.FromArgb(0, 0, 0, 0));
    private static readonly double _dpiScale = DpiHelper.LogicalToDeviceUnitsScalingFactorX;

    public MicaWindowInteropHandler()
        : base()
    {
    }

    protected System.Windows.Controls.Button? ButtonMax
    {
        get; set;
    }

    protected System.Windows.Controls.Button? ButtonRestore
    {
        get; set;
    }

    protected override void OnInitialized(EventArgs e)
    {
        if (OsHelper.IsWindows11_OrGreater && TitleBarType == TitleBarType.WinUI)
        {
            var windowHwnd = new WindowInteropHelper(this).EnsureHandle();
            HwndSource.FromHwnd(windowHwnd)?.AddHook(HwndSourceHook);
            InteropMethods.RoundWindowCorner(windowHwnd);
            InteropMethods.HideAllWindowButton(windowHwnd);
        }

        base.OnInitialized(e);
    }

    protected Size GetHostingScreenSize()
    {
        var windowHwnd = new WindowInteropHelper(this).EnsureHandle();

        var mi = default(InteropValues.MONITORINFO);
        mi.cbSize = (uint)Marshal.SizeOf(mi);

        var monitor = InteropMethods.MonitorFromWindow(windowHwnd, 2);
        InteropMethods.GetMonitorInfo(monitor, ref mi);

        var width = mi.rcWork.right - mi.rcWork.left;
        var height = mi.rcWork.bottom - mi.rcWork.top;

        return new Size(width, height);
    }

    private nint ShowSnapLayout(nint lparam, ref bool handled)
    {
        var x = (short)(lparam.ToInt32() & 0xffff);
        var y = lparam.ToInt32() >> 16;
        var point = new Point(x, y);
        var button = WindowState == WindowState.Maximized ? ButtonRestore : ButtonMax;

        if (button != null)
        {
            var buttonSize = new Size(button.ActualWidth * _dpiScale, button.ActualHeight * _dpiScale);
            var buttonLocation = button.PointToScreen(default);
            var rect = new Rect(buttonLocation, buttonSize);

            handled = rect.Contains(point);
            if (handled)
            {
                var color = TryFindResource($"MicaWPF.GradientBrushes.ControlElevationBorder") as LinearGradientBrush ?? new LinearGradientBrush();
                button.Background = color;
            }
            else
            {
                button.Background = _transparentBrush;
            }

            return PtrHelper.Create(_hTMAXBUTTON);
        }

        return PtrHelper.Zero;
    }

    private void HideMaximiseAndMinimiseButton(nint lparam, ref bool handled)
    {
        var lParamInt = lparam.ToInt32();
        var x = unchecked((short)(lParamInt & 0xffff));
        var y = unchecked((short)(lParamInt >> 16));

        var dpiScale = DpiHelper.LogicalToDeviceUnitsScalingFactorX;
        var button = WindowState == WindowState.Maximized ? ButtonRestore : ButtonMax;
        if (button == null || !button.IsVisible)
        {
            return;
        }

        var rect = new Rect(button.PointToScreen(default), new Size(button.ActualWidth * dpiScale, button.ActualHeight * dpiScale));
        if (!rect.Contains(new Point(x, y)))
        {
            return;
        }

        handled = true;
        var invokeProv = new ButtonAutomationPeer(button).GetPattern(PatternInterface.Invoke) as IInvokeProvider;
        invokeProv?.Invoke();
    }

    private nint HwndSourceHook(nint hwnd, int msg, nint n, nint lparam, ref bool handled)
    {
        if (ResizeMode is ResizeMode.NoResize or ResizeMode.CanMinimize)
        {
            return PtrHelper.Zero;
        }

        switch (msg)
        {
            case InteropValues.HwndSourceMessages.WM_NCHITTEST:
                if (SnapLayoutHelper.IsSnapLayoutEnabled())
                {
                    return ShowSnapLayout(lparam, ref handled);
                }

                break;
            case InteropValues.HwndSourceMessages.WM_NCLBUTTONDOWN:
                HideMaximiseAndMinimiseButton(lparam, ref handled);
                break;
            case InteropValues.HwndSourceMessages.WM_GETMINMAXINFO:
                var mmi = Marshal.PtrToStructure<InteropValues.MINMAXINFO>(lparam);
                var screen = GetHostingScreenSize();
                if (MaxWidth < screen.Width)
                {
                    mmi.ptMaxPosition.X = (int)((screen.Width - MaxWidth) / 2);
                    mmi.ptMaxSize.X = (int)MaxWidth;
                    mmi.ptMaxSize.Y = (int)screen.Height + 8;

                    Marshal.StructureToPtr(mmi, lparam, true);
                }

                break;
            case InteropValues.HwndSourceMessages.WM_WINDOWPOSCHANGING:
                var button = WindowState == WindowState.Maximized ? ButtonRestore : ButtonMax;
                if (button is not null)
                {
                    button.Background = _transparentBrush;
                }

                break;
        }

        return PtrHelper.Zero;
    }
}

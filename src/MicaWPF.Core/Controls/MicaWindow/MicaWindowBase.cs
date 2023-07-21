// <copyright file="MicaWindowBase.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Shell;
using MicaWPF.Core.Extensions;
using MicaWPF.Core.Helpers;

namespace MicaWPF.Core.Controls.MicaWindow;

public class MicaWindowBase : MicaWindowActionHandler, IMicaWindow
{
    public MicaWindowBase()
        : base()
    {
        CustomWindowChrome.CaptionHeight = TitleBarHeight - 7;
        CustomWindowChrome.CornerRadius = new CornerRadius(8);
        CustomWindowChrome.GlassFrameThickness = new Thickness(-1);
        WindowChrome.SetWindowChrome(this, CustomWindowChrome);
    }

    public override void OnApplyTemplate()
    {
        ButtonMax = GetTemplateChild(_buttonMaxName) as System.Windows.Controls.Button;
        ButtonRestore = GetTemplateChild(_buttonRestoreName) as System.Windows.Controls.Button;

        base.OnApplyTemplate();
    }

    public override void EndInit()
    {
        AddPadding(WindowState);
        ApplyResizeBorderThickness(WindowState);

        base.EndInit();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property.Name is nameof(WindowState))
        {
            AddPadding((WindowState)e.NewValue);
            ApplyResizeBorderThickness((WindowState)e.NewValue);
        }

        base.OnPropertyChanged(e);
    }

    protected override void OnActivated(EventArgs e)
    {
        this.EnableBackdrop(SystemBackdropType);
        base.OnActivated(e);
    }

    private void AddPadding(WindowState windowsState)
    {
        MarginMaximized = windowsState == WindowState.Maximized ? new Thickness(6) : new Thickness(0);
    }

    private void ApplyResizeBorderThickness(WindowState windowsState)
    {
        if (windowsState == WindowState.Maximized || ResizeMode == ResizeMode.NoResize)
        {
            CustomWindowChrome.ResizeBorderThickness = new Thickness(0);
        }
        else
        {
            CustomWindowChrome.ResizeBorderThickness = new Thickness(6);
        }
    }
}

// <copyright file="MicaWindowProperty.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Services;

namespace MicaWPF.Core.Controls.MicaWindow;

public class MicaWindowProperty : MicaWindowStyle
{
    public static readonly DependencyProperty CustomWindowChromeProperty = DependencyProperty.Register(nameof(CustomWindowChrome), typeof(WindowChrome), typeof(MicaWindowStyle));
    public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register(nameof(TitleBarContent), typeof(UIElement), typeof(MicaWindowStyle));
    public static readonly DependencyProperty UseAccentOnTitleBarAndBorderProperty = DependencyProperty.Register(nameof(UseAccentOnTitleBarAndBorder), typeof(bool), typeof(MicaWindowStyle), new UIPropertyMetadata(MicaWPFServiceUtility.HasBeenInitialized && MicaWPFServiceUtility.AccentColorService.IsTitleBarAndWindowsBorderColored));
    public static readonly DependencyProperty ChangeTitleColorWhenInactiveProperty = DependencyProperty.Register(nameof(ChangeTitleColorWhenInactive), typeof(bool), typeof(MicaWindowStyle), new UIPropertyMetadata(true));
    public static readonly DependencyProperty TitleBarHeightProperty = DependencyProperty.Register(nameof(TitleBarHeight), typeof(int), typeof(MicaWindowStyle), new UIPropertyMetadata(34));
    public static readonly DependencyProperty TitleBarTypeProperty = DependencyProperty.Register(nameof(TitleBarType), typeof(TitleBarType), typeof(MicaWindowStyle), new UIPropertyMetadata(TitleBarType.WinUI));
    public static readonly DependencyProperty TitleBarBrushProperty = DependencyProperty.Register(nameof(TitleBarBrush), typeof(Brush), typeof(MicaWindowStyle));
    public static readonly DependencyProperty MarginMaximizedProperty = DependencyProperty.Register(nameof(MarginMaximized), typeof(Thickness), typeof(MicaWindowStyle));

    public MicaWindowProperty()
        : base()
    {
        CustomWindowChrome = new WindowChrome();
    }

    public BackdropType SystemBackdropType { get; set; } = BackdropType.Mica;

    public int TitleBarHeight
    {
        get => (int)GetValue(TitleBarHeightProperty);
        set => SetValue(TitleBarHeightProperty, value);
    }

    public bool UseAccentOnTitleBarAndBorder
    {
        get => (bool)GetValue(UseAccentOnTitleBarAndBorderProperty);
        set => SetValue(UseAccentOnTitleBarAndBorderProperty, value);
    }

    public TitleBarType TitleBarType
    {
        get => (TitleBarType)GetValue(TitleBarTypeProperty);
        set => SetValue(TitleBarTypeProperty, value);
    }

    public bool ChangeTitleColorWhenInactive
    {
        get => (bool)GetValue(MarginMaximizedProperty);
        set => SetValue(MarginMaximizedProperty, value);
    }

    public UIElement TitleBarContent
    {
        get => (UIElement)GetValue(TitleBarContentProperty);
        set => SetValue(TitleBarContentProperty, value);
    }

    public Brush TitleBarBrush
    {
        get => (Brush)GetValue(TitleBarBrushProperty);
        set => SetValue(TitleBarBrushProperty, value);
    }

    public WindowChrome CustomWindowChrome
    {
        get => (WindowChrome)GetValue(CustomWindowChromeProperty);
        set => SetValue(CustomWindowChromeProperty, value);
    }

    /// <summary>
    /// Gets or sets if the margin maximized.
    /// </summary>
    internal Thickness? MarginMaximized
    {
        get => (Thickness)GetValue(MarginMaximizedProperty);
        set => SetValue(MarginMaximizedProperty, value);
    }
}

// <copyright file="IMicaWindow.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Controls;

public interface IMicaWindow
{
    bool ChangeTitleColorWhenInactive { get; set; }

    UIElement TitleBarContent { get; set; }

    int TitleBarHeight { get; set; }

    bool UseAccentOnTitleBarAndBorder { get; set; }

    void EndInit();

    void OnApplyTemplate();
}
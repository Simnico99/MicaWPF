// <copyright file="Frame.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.ComponentModel;
using System.Windows;
using MicaWPF.Core.Controls;
using MicaWPF.Core.Extensions;

namespace MicaWPF.Controls;

/// <summary>
/// A custom frame that reload styles on theme change.
/// </summary>
[ToolboxItem(true)]
public class Frame : System.Windows.Controls.Frame
{
    public Frame()
    {
        FocusVisualStyle = null;
        Focusable = false;
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        if (newContent is DependencyObject dependencyObject)
        {
            dependencyObject.RefreshChildrenStyle();
        }

        base.OnContentChanged(oldContent, newContent);
    }
}

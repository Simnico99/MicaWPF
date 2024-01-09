// <copyright file="DesignTimeHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.ComponentModel;
using System.Windows;

namespace MicaWPF.Core.Helpers;

public static class DesignTimeHelper
{
    public static bool IsDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
}

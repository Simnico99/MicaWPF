// <copyright file="IWindowsAccentHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Defaults.Helpers;

public interface IWindowsAccentHelper
{
    bool AreTitleBarAndBordersAccented();

    AccentColors GetAccentColor();
}
// <copyright file="AccentColorServiceWinRT.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Defaults.Helpers;
using MicaWPF.Core.Services;
using MicaWPF.Helpers;

namespace MicaWPF.Services;

/// <summary>
/// Service that manages the accent colors of the application.
/// </summary
public sealed class AccentColorServiceWinRT : AccentColorService
{
    public override IWindowsAccentHelper WindowsAccentHelper { get; } = new WindowsAccentHelper();
}
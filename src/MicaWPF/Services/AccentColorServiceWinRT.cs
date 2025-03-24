﻿// <copyright file="AccentColorServiceWinRT.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Helpers;
using MicaWPF.Core.Services;
using MicaWPF.Helpers;

namespace MicaWPF.Services;

/// <summary>
/// Service that manages the accent colors of the application.
/// </summary
public sealed class AccentColorServiceWinRT : AccentColorService
{
    public override WindowsAccentHelper WindowsAccentHelper { get; } = new WindowsAccentHelperWinRT();
}
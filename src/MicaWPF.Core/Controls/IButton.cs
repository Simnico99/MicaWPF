// <copyright file="IButton.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Symbols;

namespace MicaWPF.Core.Controls;

public interface IButton
{
    CornerRadius CornerRadius { get; set; }

    FluentSystemIcons.Regular Icon { get; set; }

    bool IconFilled { get; set; }
}
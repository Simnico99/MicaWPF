// <copyright file="ThemeDictionary.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Markup;
using MicaWPF.Core.Styles;

namespace MicaWPF.Styles;

[Ambient]
[Localizability(LocalizationCategory.Ignore)]
[UsableDuringInitialization(true)]
public sealed class ThemeDictionary : ThemeDictionaryBase
{
    public override string SourceLocation { get; } = "pack://application:,,,/MicaWPF;component/Styles/Themes";
}

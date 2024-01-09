// <copyright file="ThemeDictionary.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Styles;

namespace MicaWPF.Lite.Styles;

public sealed class ThemeDictionary : ThemeDictionaryBase
{
    public override string SourceLocation { get; } = "pack://application:,,,/MicaWPF.Lite;component/Styles/Themes";
}
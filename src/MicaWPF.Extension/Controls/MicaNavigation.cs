﻿namespace MicaWPF.Extension.Controls;
public class MicaNavigation : Menu
{
    static MicaNavigation()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaNavigation), new FrameworkPropertyMetadata(typeof(MicaNavigation)));
    }
}
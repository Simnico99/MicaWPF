﻿using MicaWPF.Symbols;

namespace MicaWPF.Controls;

/// <summary>
/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
///
/// Step 1a) Using this custom control in a XAML file that exists in the current project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is 
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:MicaWPF.Controls"
///
///
/// Step 1b) Using this custom control in a XAML file that exists in a different project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is 
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:MicaWPF.Controls;assembly=MicaWPF.Controls"
///
/// You will also need to add a project reference from the project where the XAML file lives
/// to this project and Rebuild to avoid compilation errors:
///
///     Right click on the target project in the Solution Explorer and
///     "Add Reference"->"Projects"->[Browse to and select this project]
///
///
/// Step 2)
/// Go ahead and use your control in the XAML file.
///
///     <MyNamespace:MicaButton/>
///
/// </summary>
public class MicaButton : Button
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon),
        typeof(FluentSystemIcons.Regular), typeof(Button),
        new PropertyMetadata(FluentSystemIcons.Regular.Empty));


    public static readonly DependencyProperty IconFilledProperty = DependencyProperty.Register(nameof(IconFilled),
        typeof(bool), typeof(Button), new PropertyMetadata(false));

    public FluentSystemIcons.Regular Icon
    {
        get => (FluentSystemIcons.Regular)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IconFilled
    {
        get => (bool)GetValue(IconFilledProperty);
        set => SetValue(IconFilledProperty, value);
    }

    static MicaButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaButton), new FrameworkPropertyMetadata(typeof(MicaButton)));
    }
}

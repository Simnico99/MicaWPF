// <copyright file="DependencyObjectExtension.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MicaWPF.Core.Extensions;

/// <summary>
/// Extensions for <see cref="DependencyObject"/>.
/// </summary>
public static class DependencyObjectExtension
{
    /// <summary>
    ///     Get all objects of a certain type in a <see cref="DependencyObject"/> (Visual only).
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of the type of object specified.
    /// </returns>
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj)
        where T : DependencyObject
    {
        if (depObj == null)
        {
            yield break;
        }

        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);

            if (child is T t)
            {
                yield return t;
            }

            foreach (var childOfChild in FindVisualChildren<T>(child))
            {
                yield return childOfChild;
            }
        }
    }

    /// <summary>
    ///     Get all objects of a certain type in a <see cref="DependencyObject"/> (Logical only).
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of the type of object specified.
    /// </returns>
    public static IEnumerable<T> FindLogicalChildren<T>(this DependencyObject depObj)
        where T : DependencyObject
    {
        if (depObj != null)
        {
            foreach (var rawChild in LogicalTreeHelper.GetChildren(depObj))
            {
                if (rawChild is not null)
                {
                    if (rawChild is DependencyObject child)
                    {
                        if (child is T t)
                        {
                            yield return t;
                        }

                        foreach (var childOfChild in FindLogicalChildren<T>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Refresh the style of logical and visual children in a <see cref="DependencyObject"/>.
    /// </summary>
    public static void RefreshChildrenStyle(this DependencyObject depObj)
    {
        foreach (var element in depObj.FindLogicalChildren<FrameworkElement>())
        {
            if (element is Frame)
            {
                element.RefreshChildrenStyle();
            }

            element.RefreshStyle();
        }
    }
}

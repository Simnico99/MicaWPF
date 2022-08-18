using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Extensions;
public static class DependencyObjectExtension
{
    /// <summary>
    ///     Get all objects of a certain type in a form or a page.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of the type of object specified.
    /// </returns>
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
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

    public static IEnumerable<T> FindLogicalChildren<T>(this DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            foreach (object rawChild in LogicalTreeHelper.GetChildren(depObj))
            {
                if (rawChild is DependencyObject)
                {
                    DependencyObject child = (DependencyObject)rawChild;
                    if (child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindLogicalChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }

    public static void RefreshChildrenStyle(this DependencyObject depObj) 
    {
        foreach (var element in depObj.FindLogicalChildren<FrameworkElement>())
        {
            element.RefreshStyle();
            if (element is Controls.Frame frame)
            {
                ((DependencyObject)frame.Content).RefreshChildrenStyle();
            }
        }
    }
}

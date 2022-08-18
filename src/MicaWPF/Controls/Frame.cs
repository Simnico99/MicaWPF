using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicaWPF.Extensions;

namespace MicaWPF.Controls;
public class Frame : System.Windows.Controls.Frame
{
    protected override async void OnContentChanged(object oldContent, object newContent)
    {
        if (newContent is DependencyObject dependencyObject)
        {
            foreach (var element in dependencyObject.FindVisualChildrens<FrameworkElement>())
            {
                if (element.Style is not null)
                {
                    while (!element.IsLoaded)
                    {
                        await Task.Delay(50);
                    }


                    var savedStyle = element.Style;
                    element.Style = null;

                    element.UpdateDefaultStyle();

                    element.Style = savedStyle;
                }
            }
        }

        base.OnContentChanged(oldContent, newContent);
    }
}

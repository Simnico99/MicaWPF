using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicaWPF.Extensions;

namespace MicaWPF.Controls;
public class Frame : System.Windows.Controls.Frame
{
    public Frame()
    {
        Navigated += Frame_Navigated;
    }

    private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        foreach (var element in this.FindVisualChildrens<FrameworkElement>())
        {
            var savedStyle = element.Style;
            element.Style = null;

            element.UpdateDefaultStyle();

            element.Style = savedStyle;
        }
    }
}

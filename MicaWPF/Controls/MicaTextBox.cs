using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Controls;
public class MicaTextBox : TextBox
{
    static MicaTextBox() 
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaTextBox), new FrameworkPropertyMetadata(typeof(MicaTextBox)));
    }
}

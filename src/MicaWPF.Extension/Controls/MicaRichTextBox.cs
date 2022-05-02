using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Extension.Controls;
public class MicaRichTextBox : RichTextBox
{
    static MicaRichTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaRichTextBox), new FrameworkPropertyMetadata(typeof(MicaRichTextBox)));
    }
}

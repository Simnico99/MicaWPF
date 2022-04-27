using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Controls;
public class MicaNavigation : Menu
{
    static MicaNavigation()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaNavigation), new FrameworkPropertyMetadata(typeof(MicaNavigation)));
    }
}

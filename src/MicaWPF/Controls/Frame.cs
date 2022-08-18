﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicaWPF.Extensions;

namespace MicaWPF.Controls;
public class Frame : System.Windows.Controls.Frame
{
    protected override void OnContentChanged(object oldContent, object newContent)
    {
        if (newContent is DependencyObject dependencyObject)
        {
            dependencyObject.RefreshChildrenStyle();
        }

        base.OnContentChanged(oldContent, newContent);
    }
}

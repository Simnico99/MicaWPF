using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Controls;
public class MicaListViewItem : ListViewItem
{
    public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(
    "ItemName", typeof(string),
    typeof(MicaWindow)
    );

    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.Register(
    "IconLocation", typeof(string),
    typeof(MicaWindow)
    );

    public string? ItemNoume { get; set; }

    public string? ItemName
    {
        get => (string)GetValue(ItemNameProperty);
        set => SetValue(ItemNameProperty, value);
    }

    public string? IconLocation
    {
        get => (string)GetValue(IconLocationProperty);
        set => SetValue(IconLocationProperty, value);
    }

    static MicaListViewItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaListViewItem), new FrameworkPropertyMetadata(typeof(MicaListViewItem)));
    }
}

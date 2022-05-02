namespace MicaWPF.Extension.Controls;
public class MicaListViewItem : ListViewItem
{
    public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(
    "ItemName", typeof(string),
    typeof(MicaListViewItem)
    );

    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.Register(
    "IconLocation", typeof(string),
    typeof(MicaListViewItem)
    );

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

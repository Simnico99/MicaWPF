namespace MicaWPF.Expansion.Controls;
public class MenuListViewItem : ListViewItem
{
    public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(
    "ItemName", typeof(string),
    typeof(MenuListViewItem)
    );

    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.Register(
    "IconLocation", typeof(string),
    typeof(MenuListViewItem)
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
}

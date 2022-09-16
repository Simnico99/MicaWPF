namespace MicaWPF.Controls;

internal sealed class ColumnDefinitionExtended : ColumnDefinition
{
    public static DependencyProperty VisibleProperty;

    public bool Visible
    {
        get => (bool)GetValue(VisibleProperty);
        set => SetValue(VisibleProperty, value);
    }

    static ColumnDefinitionExtended()
    {
        VisibleProperty = DependencyProperty.Register("Visible",
            typeof(bool),
            typeof(ColumnDefinitionExtended),
            new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));

        WidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended),
            new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
                new CoerceValueCallback(CoerceWidth)));

        MinWidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended),
            new FrameworkPropertyMetadata((double)0, null,
                new CoerceValueCallback(CoerceMinWidth)));
    }

    public static void SetVisible(DependencyObject obj, bool nVisible)
    {
        obj.SetValue(VisibleProperty, nVisible);
    }
    public static bool GetVisible(DependencyObject obj)
    {
        return (bool)obj.GetValue(VisibleProperty);
    }

    private static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        obj.CoerceValue(WidthProperty);
        obj.CoerceValue(MinWidthProperty);
    }

    private static object CoerceWidth(DependencyObject obj, object nValue)
    {
        return ((ColumnDefinitionExtended)obj).Visible ? nValue : new GridLength(0);
    }

    private static object CoerceMinWidth(DependencyObject obj, object nValue)
    {
        return ((ColumnDefinitionExtended)obj).Visible ? nValue : (double)0;
    }
}

namespace MicaWPF.Services;

public static class ThemeDictionnaryHelper
{
    public static readonly DependencyProperty CurrentThemeDictionaryProperty =
     DependencyProperty.RegisterAttached("CurrentThemeDictionary", typeof(Uri),
     typeof(ThemeDictionnaryHelper),
     new UIPropertyMetadata(null, CurrentThemeDictionaryChanged));

    public static Uri GetCurrentThemeDictionary(DependencyObject obj)
    {
        return (Uri)obj.GetValue(CurrentThemeDictionaryProperty);
    }

    public static void SetCurrentThemeDictionary(DependencyObject obj, Uri value)
    {
        obj.SetValue(CurrentThemeDictionaryProperty, value);
    }

    private static void CurrentThemeDictionaryChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        if (obj is FrameworkElement frameworkElement)
        {
            ApplyTheme(frameworkElement, GetCurrentThemeDictionary(obj));
        }
    }

    private static void ApplyTheme(FrameworkElement targetElement, Uri dictionaryUri)
    {
        if (targetElement == null)
        {
            return;
        }

        ThemeResourceDictionary? themeDictionary = null;
        if (dictionaryUri != null)
        {
            themeDictionary = new ThemeResourceDictionary
            {
                Source = dictionaryUri
            };
            targetElement.Resources.MergedDictionaries.Insert(0, themeDictionary);
        }

        var existingDictionaries =
            (from dictionary in targetElement.Resources.MergedDictionaries.OfType<ThemeResourceDictionary>()
             select dictionary).ToList();

        foreach (var thDictionary in existingDictionaries)
        {
            if (themeDictionary == thDictionary)
            {
                continue;
            }

            targetElement.Resources.MergedDictionaries.Remove(thDictionary);
        }
    }
}

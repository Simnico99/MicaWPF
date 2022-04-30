using MicaWPF.Mvvm;
using MicaWPF.Symbols;

namespace MicaWPF.Controls;

public class MicaPasswordBox : MicaTextBox
{
    private bool _takenControl = false;

    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(nameof(Password), typeof(string), typeof(MicaPasswordBox), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(nameof(PasswordChar), typeof(char), typeof(MicaPasswordBox), new PropertyMetadata('*', OnPasswordCharChanged));

    public static readonly DependencyProperty PasswordRevealModeProperty = DependencyProperty.Register(nameof(PasswordRevealMode), typeof(RevealMode), typeof(MicaPasswordBox), new PropertyMetadata(RevealMode.Hidden, OnPasswordRevealModeChanged));

    public static readonly DependencyProperty ShowRevealButtonProperty = DependencyProperty.Register(nameof(ShowRevealButton), typeof(bool), typeof(MicaPasswordBox), new PropertyMetadata(true));

    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(nameof(TemplateButtonCommand), typeof(IRelayCommand), typeof(MicaPasswordBox), new PropertyMetadata(null));

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        internal set => SetValue(PasswordProperty, value);
    }
    public char PasswordChar
    {
        get => (char)GetValue(PasswordCharProperty);
        set => SetValue(PasswordCharProperty, value);
    }

    public RevealMode PasswordRevealMode
    {
        get => (RevealMode)GetValue(PasswordRevealModeProperty);
        set => SetValue(PasswordRevealModeProperty, value);
    }

    public bool ShowRevealButton
    {
        get => (bool)GetValue(ShowRevealButtonProperty);
        set => SetValue(ShowRevealButtonProperty, value);
    }

    public new string Text
    {
        get => base.Text;
        set
        {
            SetValue(PasswordProperty, value);
            SetValue(TextProperty, new string(PasswordChar, value?.Length ?? 0));
        }
    }

    public IRelayCommand TemplateButtonCommand => (IRelayCommand)GetValue(TemplateButtonCommandProperty);

    static MicaPasswordBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MicaPasswordBox), new FrameworkPropertyMetadata(typeof(MicaPasswordBox)));
    }

    public MicaPasswordBox()
    {
        if (Icon is FluentSystemIcons.Regular.Empty)
        {
            Icon = FluentSystemIcons.Regular.Eye20;
        }

        SetValue(TemplateButtonCommandProperty, new RelayCommand(o => Button_OnClick(this, o)));
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        if (_takenControl)
        {
            return;
        }

        if (PasswordRevealMode == RevealMode.Visible)
        {
            base.OnTextChanged(e);
            Password = base.Text;

            return;
        }

        var text = base.Text;
        var password = Password;
        var selectionIndex = SelectionStart;

        if (text.Length < password.Length)
        {
            password = password.Remove(selectionIndex, password.Length - text.Length);
            Password = password;
        }

        if (string.IsNullOrEmpty(text))
        {
            base.OnTextChanged(e);
            return;
        }

        // TODO: Pasting text breaks this loop.
        var newContent = text.Replace(PasswordChar.ToString(), string.Empty);

        if (newContent.Length > 1)
        {
            var index = text.IndexOf(newContent[0]);
            Password = index > password.Length - 1 ? password + newContent : password.Insert(index, newContent);
        }
        else
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] == PasswordChar)
                {
                    continue;
                }

                Password = text.Length == password.Length ? password.Remove(i, 1).Insert(i, text[i].ToString()) : password.Insert(i, text[i].ToString());
            }
        }

        _takenControl = true;

        base.Text = new string(PasswordChar, Password.Length);
        SelectionStart = selectionIndex;

        _takenControl = false;

        base.OnTextChanged(e);
    }

    private void UpdatePasswordWithNewChar(char newChar)
    {
        if (PasswordRevealMode == RevealMode.Visible)
        {
            return;
        }

        base.Text = new string(newChar, base.Text.Length);
    }

    private void UpdateRevealIfPossible(RevealMode revealMode)
    {
        if (revealMode == RevealMode.Visible && Password.Length < 0)
        {
            PasswordRevealMode = RevealMode.Hidden;
            return;
        }

        SetValue(TextProperty,
            revealMode == RevealMode.Visible ? Password : new string(PasswordChar, Password.Length));
    }

    private void Button_OnClick(object sender, object parameter)
    {
        if (parameter == null)
        {
            return;
        }

        var param = parameter as string ?? string.Empty;


        if (sender is MicaPasswordBox passwordBox)
        {
            switch (param)
            {
                case "reveal":
                    passwordBox.PasswordRevealMode = passwordBox.PasswordRevealMode == RevealMode.Visible
                    ? RevealMode.Hidden
                    : RevealMode.Visible;

                    break;
            }
        }

    }

    private static void OnPasswordCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MicaPasswordBox control)
        {
            control.UpdatePasswordWithNewChar(control.PasswordChar);
        }
    }

    private static void OnPasswordRevealModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MicaPasswordBox control)
        {
            control.UpdateRevealIfPossible(control.PasswordRevealMode);
        }
    }
}

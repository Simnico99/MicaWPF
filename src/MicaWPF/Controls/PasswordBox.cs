using MicaWPF.Symbols;
using System.ComponentModel;

namespace MicaWPF.Controls;

/// <summary>
/// A password box with a button to show or hide passwords.
/// </summary>
[ToolboxItem(true)]
public class PasswordBox : TextBox
{
    private bool _takenControl = false;
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBox), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(nameof(PasswordChar), typeof(char), typeof(PasswordBox), new PropertyMetadata('•', OnPasswordCharChanged));
    public static readonly DependencyProperty PasswordRevealModeProperty = DependencyProperty.Register(nameof(PasswordRevealMode), typeof(RevealMode), typeof(PasswordBox), new PropertyMetadata(RevealMode.Hidden, OnPasswordRevealModeChanged));
    public static readonly DependencyProperty ShowRevealButtonProperty = DependencyProperty.Register(nameof(ShowRevealButton), typeof(bool), typeof(PasswordBox), new PropertyMetadata(true));

    /// <summary>
    /// The current password the user has typed.
    /// </summary>
    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        internal set => SetValue(PasswordProperty, value);
    }

    /// <summary>
    /// The character used to hide the password.
    /// </summary>
    public char PasswordChar
    {
        get => (char)GetValue(PasswordCharProperty);
        set => SetValue(PasswordCharProperty, value);
    }

    /// <summary>
    /// Is the password revealed or not.
    /// </summary>
    public RevealMode PasswordRevealMode
    {
        get => (RevealMode)GetValue(PasswordRevealModeProperty);
        set => SetValue(PasswordRevealModeProperty, value);
    }

    /// <summary>
    /// Should show the reveal button or not.
    /// </summary>
    public bool ShowRevealButton
    {
        get => (bool)GetValue(ShowRevealButtonProperty);
        set => SetValue(ShowRevealButtonProperty, value);
    }

    /// <summary>
    /// The current text in the box.
    /// </summary>
    public new string Text
    {
        get => base.Text;
        set
        {
            SetValue(PasswordProperty, value);
            SetValue(TextProperty, new string(PasswordChar, value?.Length ?? 0));
        }
    }

    public PasswordBox()
    {
        if (Icon is FluentSystemIcons.Regular.Empty)
        {
            Icon = FluentSystemIcons.Regular.Eye20;
        }
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

    private static void OnPasswordCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox control)
        {
            control.UpdatePasswordWithNewChar(control.PasswordChar);
        }
    }

    private static void OnPasswordRevealModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox control)
        {
            control.UpdateRevealIfPossible(control.PasswordRevealMode);
        }
    }
}

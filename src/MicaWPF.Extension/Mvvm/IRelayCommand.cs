// ICommand implementation example provided by Microsoft.
// https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.input.icommand?view=winrt-22000
namespace MicaWPF.Extension.Mvvm;

public interface IRelayCommand : ICommand
{
    public new bool CanExecute(object parameter);
    public new void Execute(object parameter);
}

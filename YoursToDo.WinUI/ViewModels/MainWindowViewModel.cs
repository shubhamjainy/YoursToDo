using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;

namespace YoursToDo.WinUI.ViewModels
{
    public sealed partial class MainWindowViewModel : ObservableObject
    {
        private readonly IWindowFactory Factory;
        public MainWindowViewModel(IWindowFactory factory)
        {
            Factory = factory;
        }

        [RelayCommand]
        private void Login()
        {
            Factory.ShowLoginWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Main));
        }

        [RelayCommand]
        private void CreateAccount()
        {
            Factory.ShowCreateAccountWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Main));
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;

namespace YoursToDo.ViewModels
{
    internal sealed partial class MainWindowViewModel : ObservableObject
    {
        private readonly Lazy<IWindowFactory> Factory;

        public MainWindowViewModel(Lazy<IWindowFactory> factory)
        {
            Factory = factory;
            Init();
        }

        private async void Init()
        {
            await Task.Run(async () =>
            {
                for (; ; )
                {
                    CurrentTime = DateTime.Now.ToLongTimeString();
                    await Task.Delay(1000);
                }
            });
        }

        [RelayCommand]
        private void Login()
        {
            Factory.Value.ShowLoginWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Main));
        }

        [RelayCommand]
        private void CreateAccount()
        {
            Factory.Value.ShowCreateAccountWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Main));
        }

        [ObservableProperty]
        private string currentTime = string.Empty;
    }
}
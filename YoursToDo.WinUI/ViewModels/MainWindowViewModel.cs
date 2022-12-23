using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Dispatching;
using System;
using System.Threading.Tasks;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;

namespace YoursToDo.WinUI.ViewModels
{
    public sealed partial class MainWindowViewModel : ObservableObject
    {
        private readonly DispatcherQueue _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        private readonly Lazy<IWindowFactoryBase> Factory;
        
        public MainWindowViewModel(Lazy<IWindowFactoryBase> factory)
        {
            Factory = factory;
            Init();
        }

        private async void Init()
        {
            await Task.Run(() =>
            {
                for (; ; )
                {
                    _dispatcherQueue.TryEnqueue(() =>
                    {
                        CurrentTime = DateTime.Now.ToLongTimeString();
                    });
                    Task.Delay(1000);
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
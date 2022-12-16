// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.WinUI.NotificationMessages;
using YoursToDo.WinUI.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace YoursToDo.WinUI.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            this.InitializeComponent();
            this.Title = "YourToDo";
            ViewModel = Ioc.Default.GetService<DashboardViewModel>();

            WeakReferenceMessenger.Default.Register<ClosingNotificationMessage>(this, (recipient, message) =>
            {
                var grid = this.Content as Grid;
                if (grid.Name.Equals(message.Value.ToString()))
                {
                    this.Close();
                }
            });

            WeakReferenceMessenger.Default.Register<DialogNotificationMessage>(this, async (recipient, message) =>
            {
                await MessageDialog.ShowAsync(this.Content, message.Message, message.Title, message.ButtonText);
            });

            WeakReferenceMessenger.Default.Register<DialogWithResultNotificationMessage>(this, (recipient, message) =>
            {
                var result = MessageDialog.ShowWithResultAsync(this.Content, message.Message, message.Title, message.PrimaryButtonText, message.SecondaryButtonText);
                message.Reply(result);
            });

            WeakReferenceMessenger.Default.Register<CustomDialogNotificationMessage>(this, (recipient, message) =>
            {
                var result = MessageDialog.ShowCustomViewWithResultAsync(this.Content, message.DefaultText, message.Title, message.PrimaryButtonText, message.SecondaryButtonText);
                message.Reply(result);
            });
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            WeakReferenceMessenger.Default.Unregister<ClosingNotificationMessage>(this);
            WeakReferenceMessenger.Default.Unregister<DialogNotificationMessage>(this);
            WeakReferenceMessenger.Default.Unregister<DialogWithResultNotificationMessage>(this);
            WeakReferenceMessenger.Default.Unregister<CustomDialogNotificationMessage>(this);
        }
        public DashboardViewModel ViewModel { get; set; }
    }
}
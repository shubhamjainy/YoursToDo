// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.WinUI.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace YoursToDo.WinUI.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "YourToDo";
            ViewModel = Ioc.Default.GetService<MainWindowViewModel>();

            WeakReferenceMessenger.Default.Register<ClosingNotificationMessage>(this, (recipient, message) =>
            {
                var grid = this.Content as Grid;
                if (grid.Name.Equals(message.Value.ToString()))
                {
                    this.Close();

                }
            });
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            WeakReferenceMessenger.Default.Unregister<ClosingNotificationMessage>(this);
        }
    }
}
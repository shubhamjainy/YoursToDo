// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Manager;
using YoursToDo.EFCore;
using YoursToDo.EFCore.Interface;
using YoursToDo.EFCore.Service;
using YoursToDo.WinUI.Helper;
using YoursToDo.WinUI.ViewModels;
using YoursToDo.WinUI.Views;
using Window = Microsoft.UI.Xaml.Window;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace YoursToDo.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            // Register services
            Ioc.Default.ConfigureServices(
               new ServiceCollection()
               .AddSingleton<IUserService, UserService>()
               .AddSingleton<IItemService, ItemService>()
               .AddSingleton<IWindowFactoryBase, WindowFactory>()
               .AddSingleton<IUserManager, UserManager>()
               .AddTransient(provider => new Lazy<IWindowFactoryBase>(provider.GetService<IWindowFactoryBase>))
               .AddTransient(provider => new Lazy<IUserService>(provider.GetService<IUserService>))
               .AddTransient(provider => new Lazy<IItemService>(provider.GetService<IItemService>))
               .AddTransient(provider => new Lazy<IUserManager>(provider.GetService<IUserManager>))
               .AddTransient<MainWindowViewModel>()
               .AddTransient<LoginViewModel>()
               .AddTransient<CreateAccountViewModel>()
               .AddTransient<DashboardViewModel>()
               .AddDbContext<UserDBContext>()
               .BuildServiceProvider());
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            WindowFactory.CreateWindow(m_window);
        }

        private Window m_window;
    }
}
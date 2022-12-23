using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Manager;
using YoursToDo.EFCore;
using YoursToDo.EFCore.Interface;
using YoursToDo.EFCore.Service;
using YoursToDo.Helper;
using YoursToDo.ViewModels;

namespace YoursToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Register services
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
               .AddSingleton<IUserService, UserService>()
                .AddSingleton<IItemService, ItemService>()
                .AddSingleton<IWindowFactory, WindowFactory>()
                .AddSingleton<IUserManager, UserManager>()
                .AddTransient(provider => new Lazy<IWindowFactory>(provider.GetService<IWindowFactory>))
                .AddTransient(provider => new Lazy<IUserService>(provider.GetService<IUserService>))
                .AddTransient(provider => new Lazy<IItemService>(provider.GetService<IItemService>))
                .AddTransient(provider => new Lazy<IUserManager>(provider.GetService<IUserManager>))
                .AddTransient<MainWindowViewModel>()
                .AddTransient<LoginViewModel>()
                .AddTransient<CreateAccountViewModel>()
                .AddTransient<DashboardViewModel>()
                .AddTransient<EditItemViewModel>()
                .AddDbContext<UserDBContext>()
                .BuildServiceProvider());
        }
    }
}
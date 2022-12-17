using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Manager;
using YoursToDo.EFCore;
using YoursToDo.EFCore.Entity;
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
                .AddSingleton<IDataService<User>, DataService<User>>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IItemService, ItemService>()
                .AddSingleton<IWindowFactory, WindowFactory>()
                .AddSingleton<IUserManager, UserManager>()
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
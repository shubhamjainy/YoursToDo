using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using YoursToDo.Common;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Models;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.Helper;
using YoursToDo.Service;
using YoursToDo.ViewModels;

namespace YoursToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserDBContext _context =
            new();

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        public App()
        {
            // Register services
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IDataService<User>, DataService<User>>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IWindowFactory, WindowFactory>()
                .AddTransient<MainWindowViewModel>()
                .AddTransient<LoginViewModel>()
                .AddTransient<CreateAccountViewModel>()
                .BuildServiceProvider());

            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Users.Load();
        }
    }
}
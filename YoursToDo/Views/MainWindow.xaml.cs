using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.ViewModels;

namespace YoursToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<MainWindowViewModel>();

            WeakReferenceMessenger.Default.Register<ClosingNotificationMessage>(this, (recipient, message) =>
            {
                if (this.Name.Equals(message.Value.ToString()))
                {
                    this.Close();
                }
            });
        }
    }
}
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.ViewModels;

namespace YoursToDo.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<DashboardViewModel>();

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
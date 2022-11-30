using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.ViewModels;

namespace YoursToDo.Views
{
    /// <summary>
    /// Interaction logic for CreateAccountView.xaml
    /// </summary>
    public partial class CreateAccountView : Window
    {
        public CreateAccountView()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<CreateAccountViewModel>();

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

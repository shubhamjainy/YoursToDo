using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.ViewModels;

namespace YoursToDo.Views
{
    /// <summary>
    /// Interaction logic for EditItemView.xaml
    /// </summary>
    public partial class EditItemView : Window
    {
        public EditItemView()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<EditItemViewModel>();

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

using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows;
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
        }
    }
}

using YoursToDo.Common.Interface;
using YoursToDo.Views;

namespace YoursToDo.Helper
{
    internal sealed class WindowFactory : IWindowFactory
    {
        public void ShowCreateAccountWindow() => new CreateAccountView().Show();
        public void ShowLoginWindow() => new LoginView().Show();
        public void ShowMainWindow() => new MainWindow().Show();
        public void ShowDashboardWindow() => new DashboardView().Show();
        public bool? ShowEditItemWindow() => (new EditItemView()).ShowDialog();
    }
}
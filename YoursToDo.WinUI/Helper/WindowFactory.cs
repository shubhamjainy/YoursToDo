using Microsoft.UI.Xaml;
using WinUIEx;
using YoursToDo.Common.Interface;
using YoursToDo.WinUI.Views;

namespace YoursToDo.WinUI.Helper
{
    internal sealed class WindowFactory : IWindowFactory
    {
        public void ShowCreateAccountWindow() => CreateWindow(new CreateAccountView());

        public void ShowLoginWindow() => CreateWindow(new LoginView());

        public void ShowMainWindow() => CreateWindow(new MainWindow());
        public void ShowDashboardWindow() => CreateWindow(new DashboardView());

        public static void CreateWindow(Window window)
        {
            window.SetIsMaximizable(Constants.IsMaximizable);
            window.SetIsResizable(Constants.IsResizable);
            window.SetWindowSize(Constants.Width, Constants.Height);
            window.CenterOnScreen();
            window.Activate();
        }
    }
}
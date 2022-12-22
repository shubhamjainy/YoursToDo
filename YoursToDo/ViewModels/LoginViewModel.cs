using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using YoursToDo.Common;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.EFCore.Interface;

namespace YoursToDo.ViewModels
{
    internal sealed partial class LoginViewModel : ObservableValidator
    {
        private readonly Lazy<IUserService> UserService;
        private readonly Lazy<IWindowFactory> Factory;
        private readonly Lazy<IUserManager> UserManager;

        public LoginViewModel(Lazy<IUserService> userService, Lazy<IWindowFactory> factory, Lazy<IUserManager> userManager)
        {
            UserService = userService;
            Factory = factory;
            UserManager = userManager;
        }

        [RelayCommand(CanExecute = nameof(CanLoginExecute))]
        private async Task LoginAsync()
        {
            var user = await UserService.Value.Get(email);

            if (user is not null && user.Password.Equals(Password))
            {
                UserManager.Value.SetUserData(user.Name, user.Email, user.Id);
                Factory.Value.ShowDashboardWindow();
                WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Login));
            }
            else
            {
                MessageBox.Show(Constant.IncorrectEmailOrPassword);
            }
        }

        private bool CanLoginExecute()
        {
            return !HasErrors && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        [RelayCommand]
        private void CreateAccount()
        {
            Factory.Value.ShowCreateAccountWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Login));
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(10)]
        [EmailAddress]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string email = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(8)]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password = string.Empty;
    }
}
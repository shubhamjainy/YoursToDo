using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using YoursToDo.Common;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.EFCore.Interface;
using YoursToDo.WinUI.NotificationMessages;

namespace YoursToDo.WinUI.ViewModels
{
    public sealed partial class LoginViewModel : ObservableValidator
    {
        private readonly Lazy<IWindowFactoryBase> Factory;
        private readonly Lazy<IUserService> UserService;
        private readonly Lazy<IUserManager> UserManager;

        public LoginViewModel(Lazy<IUserService> userService, Lazy<IWindowFactoryBase> factory, Lazy<IUserManager> userManager)
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
                await WeakReferenceMessenger.Default.Send(new DialogWithOkButtonNotificationMessage(Constant.IncorrectEmailOrPassword));
            }
        }

        private bool CanLoginExecute()
        {
            if (HasErrors)
            {
                ShowErrorInfo = true;
                ErrorMessage = string.Join("\n", GetErrors());
            }
            else
            {
                ShowErrorInfo = false;
                ErrorMessage = "";
            }

            var canExecute = !HasErrors && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            if (canExecute)
            {
                ShowErrorInfo = false;
            }

            return canExecute;
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
        [EmailAddress]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string email;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(8)]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password;

        [ObservableProperty]
        private bool showErrorInfo;

        [ObservableProperty]
        private string errorMessage;
    }
}
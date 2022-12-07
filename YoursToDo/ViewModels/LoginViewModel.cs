using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using YoursToDo.Common;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;

namespace YoursToDo.ViewModels
{
    internal sealed partial class LoginViewModel : ObservableValidator
    {
        private readonly IUserService UserService;
        private readonly IWindowFactory Factory;
        private readonly IUserManager UserManager;

        public LoginViewModel(IUserService userService, IWindowFactory factory, IUserManager userManager)
        {
            UserService = userService;
            Factory = factory;
            UserManager = userManager;
        }

        [RelayCommand(CanExecute = nameof(CanLoginExecute))]
        private async Task LoginAsync()
        {
            var user = await UserService.Get(email);

            if (user is not null && user.Password.Equals(Password))
            {
                UserManager.SetUserData(user.Name, user.Email, user.Id);
                Factory.ShowDashboardWindow();
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
            Factory.ShowCreateAccountWindow();
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
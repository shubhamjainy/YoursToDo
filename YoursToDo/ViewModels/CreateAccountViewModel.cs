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
using YoursToDo.EFCore.Entity;
using YoursToDo.EFCore.Interface;

namespace YoursToDo.ViewModels
{
    public sealed partial class CreateAccountViewModel : ObservableValidator
    {
        private readonly Lazy<IUserService> UserService;
        private readonly Lazy<IWindowFactory> Factory;

        public CreateAccountViewModel(Lazy<IUserService> userService, Lazy<IWindowFactory> factory)
        {
            UserService = userService;
            Factory = factory;
        }

        [RelayCommand]
        private void Login()
        {
            Factory.Value.ShowLoginWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.CreateAccount));
        }

        private bool CanLoginExecute()
        {
            return !HasErrors && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
        }

        [RelayCommand(CanExecute = nameof(CanLoginExecute))]
        private async Task CreateAccountAsync()
        {
            var getUser = await UserService.Value.Exists(email);

            if (getUser)
            {
                MessageBox.Show(Constant.AccountAlreadyExists);
                return;
            }

            User user = new()
            {
                Email = Email,
                Password = Password,
                Name = Name
            };
            var result = await UserService.Value.Create(user);
            if (result is not null)
            {
                MessageBox.Show(Constant.AccountCreationSuccessful);
                Login();
            }
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(10)]
        [EmailAddress]
        [NotifyCanExecuteChangedFor(nameof(CreateAccountCommand))]
        private string email = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(8)]
        [NotifyCanExecuteChangedFor(nameof(CreateAccountCommand))]
        private string password = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(4)]
        [NotifyCanExecuteChangedFor(nameof(CreateAccountCommand))]
        private string name = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [CustomValidation(typeof(CreateAccountViewModel), nameof(MatchPassword))]
        [MinLength(8)]
        [NotifyCanExecuteChangedFor(nameof(CreateAccountCommand))]
        public string confirmPassword = string.Empty;

        public static ValidationResult? MatchPassword(string name, ValidationContext context)
        {
            CreateAccountViewModel instance = (CreateAccountViewModel)context.ObjectInstance;

            if (instance.Password.Equals(instance.ConfirmPassword))
            {
                return ValidationResult.Success;
            }

            return new("Password mis-match");
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using YoursToDo.Common;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.EFCore.Entity;
using YoursToDo.EFCore.Interface;
using YoursToDo.WinUI.NotificationMessages;

namespace YoursToDo.WinUI.ViewModels
{
    public sealed partial class CreateAccountViewModel : ObservableValidator
    {
        private readonly Lazy<IWindowFactory> Factory;
        private readonly Lazy<IUserService> UserService;

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

            var canExecute = !HasErrors && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
            if (canExecute)
            {
                ShowErrorInfo = false;
            }

            return canExecute;
        }

        [RelayCommand(CanExecute = nameof(CanLoginExecute))]
        private async Task CreateAccountAsync()
        {
            var getUser = await UserService.Value.Exists(email);

            if (getUser)
            {
               await WeakReferenceMessenger.Default.Send(new DialogWithOkButtonNotificationMessage(Constant.AccountAlreadyExists));

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
                var dialogResult = await WeakReferenceMessenger.Default.Send(new DialogWithOkButtonNotificationMessage(Constant.AccountCreationSuccessful, "Success"));
                if (dialogResult == ContentDialogResult.Primary)
                {
                    Login();
                }
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

        [ObservableProperty]
        private bool showErrorInfo;

        [ObservableProperty]
        private string errorMessage;
        public static ValidationResult MatchPassword(string name, ValidationContext context)
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
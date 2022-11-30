﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using YoursToDo.Common;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Models;
using YoursToDo.Common.NotificationMessages;

namespace YoursToDo.ViewModels
{
    public sealed partial class CreateAccountViewModel : ObservableValidator
    {
        private readonly IUserService UserService;
        private readonly IWindowFactory Factory;

        public CreateAccountViewModel(IUserService userService, IWindowFactory factory)
        {
            UserService = userService;
            Factory = factory;
        }

        [RelayCommand]
        private void Login()
        {
            Factory.ShowLoginWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.CreateAccount));
        }

        private bool CanLoginExecute()
        {
            return !HasErrors && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
        }

        [RelayCommand(CanExecute = nameof(CanLoginExecute))]
        private async Task CreateAccountAsync()
        {
            var getUser = await UserService.Get(email);

            if (getUser is not null)
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
            var result = await UserService.Create(user);
            if (result is not null)
            {
                MessageBox.Show(Constant.AccountCreationSuccessful);
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
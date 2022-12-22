using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.EFCore.Entity;
using YoursToDo.EFCore.Interface;

namespace YoursToDo.ViewModels
{
    internal sealed partial class EditItemViewModel : ObservableObject
    {
        private readonly Lazy<IUserService> UserService;
        private readonly Lazy<IUserManager> UserManager;
        private User? user;
        private readonly int userId;
        private readonly Item selectedItem;

        public EditItemViewModel(Lazy<IUserService> userService, Lazy<IUserManager> userManager)
        {
            UserService = userService;
            UserManager = userManager;

            userId = UserManager.Value.UserId;
            selectedItem = UserManager.Value.SelectedItem!;
            UpdatedToDoItem = selectedItem!.Content;
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditCommand))]
        private string updatedToDoItem = string.Empty;

        [RelayCommand(CanExecute = nameof(CanEditExecute))]
        private async Task EditAsync()
        {
            var clone = selectedItem;
            if (user is null || clone is null)
            {
                return;
            }
            user.Items.Remove(clone);
            clone.Content = UpdatedToDoItem;
            user.Items.Add(clone);
            await UserService.Value.Update(user);
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.EditItem));
        }

        [RelayCommand]
        private void Cancel()
        {
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.EditItem));
        }
        private bool CanEditExecute()
        {
            return !string.IsNullOrEmpty(UpdatedToDoItem) && !selectedItem.Content.Equals(UpdatedToDoItem);
        }

        [RelayCommand]
        private async Task LoadedAsync()
        {
            user = await UserService.Value.Get(userId);
        }
    }
}
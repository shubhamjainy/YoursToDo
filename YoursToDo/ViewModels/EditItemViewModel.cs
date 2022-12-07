using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;
using YoursToDo.Common.Entity;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;

namespace YoursToDo.ViewModels
{
    internal sealed partial class EditItemViewModel : ObservableObject
    {
        private readonly IUserService UserService;
        private readonly IUserManager UserManager;
        private User? user;
        private readonly int userId;
        private readonly Item selectedItem;

        public EditItemViewModel(IUserService userService, IUserManager userManager)
        {
            UserService = userService;
            UserManager = userManager;

            userId = UserManager.UserId;
            selectedItem = UserManager.SelectedItem!;
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
            await UserService.Update(user);
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
            user = await UserService.Get(userId);
        }
    }
}
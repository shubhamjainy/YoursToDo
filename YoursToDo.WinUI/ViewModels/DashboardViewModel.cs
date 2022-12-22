using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
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
    public sealed partial class DashboardViewModel : ObservableRecipient
    {
        private readonly IItemService ItemService;
        private readonly IUserService UserService;
        private readonly IWindowFactory Factory;
        private readonly IUserManager UserManager;
        private User user;

        public DashboardViewModel(IItemService itemService, IWindowFactory factory, IUserService userService, IUserManager userManager)
        {
            ItemService = itemService;
            UserService = userService;
            Factory = factory;
            UserManager = userManager;

            Name = UserManager.Name;
            UserId = UserManager.UserId;
        }

        public async Task LoadAllToDoItems()
        {
            user = await UserService.Get(UserId);
            Items = user.Items;
        }

        [RelayCommand(CanExecute = nameof(CanLoginExecute))]
        private async Task AddAsync()
        {
            if (user == null)
            {
                return;
            }

            Item item = new()
            {
                CreatedAt = DateTime.Now,
                UserId = UserId,
                Content = NewToDoItem
            };

            user.Items.Add(item);
            var result = await UserService.Update(user);

            if (result is not null)
            {
                await WeakReferenceMessenger.Default.Send(new DialogWithOkButtonNotificationMessage(Constant.NewItemAddedSuccessful, "Success"));
                NewToDoItem = string.Empty;
            }
        }

        [RelayCommand]
        private void LogOut()
        {
            Factory.ShowMainWindow();
            WeakReferenceMessenger.Default.Send(new ClosingNotificationMessage(WindowType.Dashboard));
        }

        [RelayCommand]
        private async Task LoadedItemsAsync()
        {
            await LoadAllToDoItems();
        }

        [RelayCommand]
        private async Task EditAsync()
        {
            var result = await WeakReferenceMessenger.Default.Send(new DialogWithOkCancelButtonNotificationMessage(Constant.AreYouSureToUpdateSelectedItem, "Warning"));
            if (result == ContentDialogResult.Primary)
            {
                UserManager.SetSelectedToDoItem(SelectedToDoItem);
                var updatedToDoItemContent = await WeakReferenceMessenger.Default.Send(new CustomDialogNotificationMessage(SelectedToDoItem.Content));
                if (!string.IsNullOrEmpty(updatedToDoItemContent) && SelectedToDoItem.Content != updatedToDoItemContent)
                {
                    var clone = SelectedToDoItem;
                    user.Items.Remove(clone);
                    clone.Content = updatedToDoItemContent;
                    user.Items.Add(clone);
                    await UserService.Update(user);
                }
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            var result = await WeakReferenceMessenger.Default.Send(new DialogWithOkCancelButtonNotificationMessage(Constant.AreYouSureToDeleteSelectedItem, "Warning"));
            if (result == ContentDialogResult.Primary)
            {
                await ItemService.Delete(SelectedToDoItem);
            }
        }

        private bool CanLoginExecute()
        {
            return !string.IsNullOrEmpty(NewToDoItem);
        }

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private int userId;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private string newToDoItem = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Item> items = new();

        [ObservableProperty]
        private Item selectedToDoItem = new();
    }
}

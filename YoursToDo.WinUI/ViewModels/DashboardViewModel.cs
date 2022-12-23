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
        private readonly Lazy<IItemService> ItemService;
        private readonly Lazy<IUserService> UserService;
        private readonly Lazy<IWindowFactoryBase> Factory;
        private readonly Lazy<IUserManager> UserManager;
        private User user;

        public DashboardViewModel(Lazy<IItemService> itemService, Lazy<IWindowFactoryBase> factory, Lazy<IUserService> userService, Lazy<IUserManager> userManager)
        {
            ItemService = itemService;
            UserService = userService;
            Factory = factory;
            UserManager = userManager;

            Name = UserManager.Value.Name;
            UserId = UserManager.Value.UserId;
        }

        public async Task LoadAllToDoItems()
        {
            user = await UserService.Value.Get(UserId);
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
            var result = await UserService.Value.Update(user);

            if (result is not null)
            {
                await WeakReferenceMessenger.Default.Send(new DialogWithOkButtonNotificationMessage(Constant.NewItemAddedSuccessful, "Success"));
                NewToDoItem = string.Empty;
            }
        }

        [RelayCommand]
        private void LogOut()
        {
            Factory.Value.ShowMainWindow();
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
                UserManager.Value.SetSelectedToDoItem(SelectedToDoItem);
                var updatedToDoItemContent = await WeakReferenceMessenger.Default.Send(new CustomDialogNotificationMessage(SelectedToDoItem.Content));
                if (!string.IsNullOrEmpty(updatedToDoItemContent) && SelectedToDoItem.Content != updatedToDoItemContent)
                {
                    var clone = SelectedToDoItem;
                    user.Items.Remove(clone);
                    clone.Content = updatedToDoItemContent;
                    user.Items.Add(clone);
                    await UserService.Value.Update(user);
                }
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            var result = await WeakReferenceMessenger.Default.Send(new DialogWithOkCancelButtonNotificationMessage(Constant.AreYouSureToDeleteSelectedItem, "Warning"));
            if (result == ContentDialogResult.Primary)
            {
                await ItemService.Value.Delete(SelectedToDoItem);
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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.EFCore.Entity;
using YoursToDo.EFCore.Interface;
using Constant = YoursToDo.Common.Constant;

namespace YoursToDo.ViewModels
{
    internal sealed partial class DashboardViewModel : ObservableObject
    {
        private readonly Lazy<IItemService> ItemService;
        private readonly Lazy<IUserService> UserService;
        private readonly Lazy<IWindowFactory> Factory;
        private readonly Lazy<IUserManager> UserManager;
        private User? user;

        public DashboardViewModel(Lazy<IItemService> itemService, Lazy<IWindowFactory> factory, Lazy<IUserService> userService, Lazy<IUserManager> userManager)
        {
            ItemService = itemService;
            UserService = userService;
            Factory = factory;
            UserManager = userManager;

            Name = UserManager.Value.Name;
            UserId = UserManager.Value.UserId;
        }

        private async Task LoadAllToDoItems()
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
                MessageBox.Show(Constant.NewItemAddedSuccessful);
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
        private void Edit()
        {
            if (MessageBox.Show(Constant.AreYouSureToUpdateSelectedItem, "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                UserManager.Value.SetSelectedToDoItem(SelectedToDoItem);

                Factory.Value.ShowEditItemWindow();
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (MessageBox.Show(Constant.AreYouSureToDeleteSelectedItem, "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
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
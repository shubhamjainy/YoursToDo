using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using static ABI.System.Windows.Input.ICommand_Delegates;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using YoursToDo.Common.Enums;
using YoursToDo.Common.Interface;
using YoursToDo.Common.NotificationMessages;
using YoursToDo.Common.Entity;
using YoursToDo.Common;

namespace YoursToDo.WinUI.ViewModels
{
    public sealed partial class DashboardViewModel : ObservableObject
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
                MessageBox.Show(Constant.NewItemAddedSuccessful);
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
        private void Edit()
        {
            //if (MessageBox.Show(Constant.AreYouSureToUpdateSelectedItem, "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //{
            //    UserManager.SetSelectedToDoItem(SelectedToDoItem);

            //    Factory.ShowEditItemWindow();
            //}
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            //if (MessageBox.Show(Constant.AreYouSureToDeleteSelectedItem, "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //{
            //    await ItemService.Delete(SelectedToDoItem);
            //}
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

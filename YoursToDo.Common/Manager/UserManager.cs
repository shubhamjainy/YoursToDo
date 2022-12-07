using YoursToDo.Common.Entity;
using YoursToDo.Common.Interface;

namespace YoursToDo.Common.Manager
{
    public sealed class UserManager : IUserManager
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public Item? SelectedItem { get; set; }

        public void SetUserData(string userName, string email, int userId)
        {
            this.Name = userName;
            this.Email = email;
            this.UserId = userId;
        }

        public void SetSelectedToDoItem(Item selectedItem)
        {
            this.SelectedItem = selectedItem;
        }
    }
}
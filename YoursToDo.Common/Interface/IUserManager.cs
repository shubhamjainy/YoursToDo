using YoursToDo.Common.Entity;

namespace YoursToDo.Common.Interface
{
    public interface IUserManager
    {
        string Name { get; set; }
        string Email { get; set; }
        int UserId { get; set; }
        Item? SelectedItem { get; set; }

        void SetUserData(string userName, string email, int userId);

        void SetSelectedToDoItem(Item selectedItem);
    }
}
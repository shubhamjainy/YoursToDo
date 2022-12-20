using YoursToDo.EFCore.Entity;

namespace YoursToDo.Common.Interface
{
    public interface IUserManager
    {
        public string Name { get; set; }
        public string Email { get; set; }
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
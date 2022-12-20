using YoursToDo.Common.Interface;
using YoursToDo.EFCore.Entity;

namespace YoursToDo.Common.Manager
{
    public sealed class UserManager : IUserManager
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public Item? SelectedItem { get; set; }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace YoursToDo.Common.Entity
{
    public record User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public virtual ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public User()
        {

        }
    }
}
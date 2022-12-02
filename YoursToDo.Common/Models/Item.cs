using System.ComponentModel.DataAnnotations;

namespace YoursToDo.Common.Models
{
    public record Item
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; } = new();

        public Item()
        {

        }

        public Item(string content, int userId, DateTime createdAt)
        {
            Content = content;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}
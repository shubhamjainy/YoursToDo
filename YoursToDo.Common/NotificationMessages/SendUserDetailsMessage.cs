namespace YoursToDo.Common.NotificationMessages
{
    public class SendUserDetailsMessage
    {
        /// <summary>
        /// Gets a string containing any arbitrary message to be passed to recipient(s).
        /// </summary>
        public string Name { get; private set; }
        public string Email { get; private set; }
        public int UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SendUserDetailsMessage class.
        /// </summary>
        /// <param name="name"></param>
        public SendUserDetailsMessage(string name, string email, int userId)
        {
            Name = name;
            Email = email;
            UserId = userId;
        }
    }
}
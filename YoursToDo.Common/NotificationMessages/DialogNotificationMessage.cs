namespace YoursToDo.Common.NotificationMessages
{
    public class DialogNotificationMessage
    {
        /// <summary>
        /// Gets a string containing any arbitrary message to be passed to recipient(s).
        /// </summary>
        public string Message { get; private set; }
        public string Title { get; private set; }
        public string ButtonText { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DialogNotificationMessage class.
        /// </summary>
        /// <param name="message"></param>
        public DialogNotificationMessage(string message, string title = "Error", string buttonText = "OK")
        {
            Message = message;
            Title = title;
            ButtonText = buttonText;
        }
    }
}
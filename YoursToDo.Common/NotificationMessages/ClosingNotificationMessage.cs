using CommunityToolkit.Mvvm.Messaging.Messages;
using YoursToDo.Common.Enums;

namespace YoursToDo.Common.NotificationMessages
{
    public class ClosingNotificationMessage : ValueChangedMessage<WindowType>
    {
        /// <summary>
        /// Gets a string containing any arbitrary message to be passed to recipient(s).
        /// </summary>
        public WindowType Notification { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the ClosingNotificationMessage class.
        /// </summary>
        /// <param name="notification"></param>
        public ClosingNotificationMessage(WindowType notification) : base(notification)
        {
            Notification = notification;
        }
    }
}
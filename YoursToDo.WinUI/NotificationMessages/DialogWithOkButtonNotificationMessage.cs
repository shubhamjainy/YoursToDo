using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.UI.Xaml.Controls;

namespace YoursToDo.WinUI.NotificationMessages
{
    internal sealed class DialogWithOkButtonNotificationMessage: AsyncRequestMessage<ContentDialogResult>
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
        public DialogWithOkButtonNotificationMessage(string message, string title = "Error", string buttonText = "OK")
        {
            Message = message;
            Title = title;
            ButtonText = buttonText;
        }
    }
}
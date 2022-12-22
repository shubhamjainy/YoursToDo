using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.UI.Xaml.Controls;

namespace YoursToDo.WinUI.NotificationMessages
{
    internal sealed class DialogWithOkCancelButtonNotificationMessage : AsyncRequestMessage<ContentDialogResult>

    {
        /// <summary>
        /// Gets a string containing any arbitrary message to be passed to recipient(s).
        /// </summary>
        public string Message { get; private set; }
        public string Title { get; private set; }
        public string PrimaryButtonText { get; private set; }
        public string SecondaryButtonText { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DialogNotificationMessage class.
        /// </summary>
        /// <param name="message"></param>
        public DialogWithOkCancelButtonNotificationMessage(string message, string title = "Error", string primaryButtonText = "OK", string secondaryButtonText = "Cancel")
        {
            Message = message;
            Title = title;
            PrimaryButtonText = primaryButtonText;
            SecondaryButtonText = secondaryButtonText;
        }
    }
}
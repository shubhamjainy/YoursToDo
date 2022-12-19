using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.UI.Xaml.Controls;

namespace YoursToDo.WinUI.NotificationMessages
{
    internal sealed class CustomDialogNotificationMessage : AsyncRequestMessage<string>
    {
        public string Title { get; private set; }
        public string DefaultText { get; private set; }
        public string PrimaryButtonText { get; private set; }
        public string SecondaryButtonText { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DialogNotificationMessage class.
        /// </summary>
        /// <param name="message"></param>
        public CustomDialogNotificationMessage(string defaultText, string title = "Edit the selected item", string primaryButtonText = "OK", string secondaryButtonText = "Cancel")
        {
            DefaultText = defaultText;
            Title = title;
            PrimaryButtonText = primaryButtonText;
            SecondaryButtonText = secondaryButtonText;
        }
    }
}
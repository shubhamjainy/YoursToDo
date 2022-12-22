using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace YoursToDo.WinUI
{
    internal static class MessageDialog
    {
        internal static async Task<ContentDialogResult> ShowAsync(this UIElement element, string message, string title = "Error", string buttonText = "OK")
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = buttonText,
                XamlRoot = element.XamlRoot,
                DefaultButton = ContentDialogButton.Primary
            };

            return await dialog.ShowAsync();
        }

        internal static async Task<ContentDialogResult> ShowWithResultAsync(this UIElement element, string message, string title = "Warning", string primaryButtonText = "OK", string secondaryButtonText = "Cancel")
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = primaryButtonText,
                SecondaryButtonText = secondaryButtonText,
                XamlRoot = element.XamlRoot,
                DefaultButton = ContentDialogButton.Secondary
            };

            return await dialog.ShowAsync();
        }

        internal static async Task<string> ShowCustomViewWithResultAsync(this UIElement element, string defaultText, string title = "Edit the selected Item", string primaryButtonText = "OK", string secondaryButtonText = "Cancel")
        {
            var inputTextBox = new TextBox
            {
                AcceptsReturn = false,
                Height = 32,
                Text = defaultText,
                SelectionStart = defaultText.Length,
                TextWrapping= TextWrapping.Wrap
            };

            var dialog = new ContentDialog
            {
                Title = title,
                Content = inputTextBox,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                PrimaryButtonText = primaryButtonText,
                SecondaryButtonText = secondaryButtonText,
                XamlRoot = element.XamlRoot,
                DefaultButton = ContentDialogButton.Secondary
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return inputTextBox.Text;
            }
            return string.Empty;
        }
    }
}
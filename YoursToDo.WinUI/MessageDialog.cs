using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace YoursToDo.WinUI
{
    internal static class MessageDialog
    {
        internal static async Task ShowAsync(this UIElement element, string message, string title = "Error", string buttonText = "OK")
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = buttonText,
                XamlRoot = element.XamlRoot,
                DefaultButton = ContentDialogButton.Primary
            };

            await dialog.ShowAsync();
        }
    }
}
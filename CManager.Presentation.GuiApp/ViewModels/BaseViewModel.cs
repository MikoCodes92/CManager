using CommunityToolkit.Mvvm.ComponentModel;

namespace CManager.Presentation.GuiApp.ViewModels
{
    // Base view model with common functionality
    public abstract partial class BaseViewModel: ObservableObject
    {
        // Title of the view
        [ObservableProperty]
        private string _title = string.Empty;

        //status message to display
        [ObservableProperty]
        private string _statusMessage = string.Empty;

        //shows an error message
        protected void ShowError(string message)
        {
            StatusMessage = $"Required:{message}";
        }

        //shows a success message
        protected void showSuccess(string message)
        {
            StatusMessage = $"Success:{message}";
        }
    }
}

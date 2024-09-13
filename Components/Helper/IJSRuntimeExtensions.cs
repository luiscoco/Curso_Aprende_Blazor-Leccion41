using Microsoft.JSInterop;

namespace Leccion40.Components.Helper
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ShowAlert(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showAlert", message);
        }
    }
}
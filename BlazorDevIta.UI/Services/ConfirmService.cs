using Microsoft.JSInterop;

namespace BlazorDevIta.UI.Services
{
    public class ConfirmService : IAsyncDisposable, IConfirmService
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? module = null;

        public ConfirmService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Init()
        {
            module = await _jsRuntime.InvokeAsync<IJSObjectReference>
                ("import", "./_content/BlazorDevIta.UI/confirm.js");
        }

        public async Task ShowConfirm(string confirmId)
        {
            if (module is not null)
                await module.InvokeVoidAsync("showConfirm", confirmId);
        }

        public async Task HideConfirm(string confirmId)
        {
            if (module is not null)
                await module.InvokeVoidAsync("hideConfirm", confirmId);
        }

        public async ValueTask DisposeAsync()
        {
            if (module is not null)
                await module.DisposeAsync();
        }
    }
}

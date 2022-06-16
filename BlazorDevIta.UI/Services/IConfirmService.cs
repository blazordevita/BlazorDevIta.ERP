
namespace BlazorDevIta.UI.Services
{
    public interface IConfirmService
    {
        Task HideConfirm(string confirmId);
        Task Init();
        Task ShowConfirm(string confirmId);
    }
}
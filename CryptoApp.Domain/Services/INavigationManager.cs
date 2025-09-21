namespace CryptoApp.Domain.Services
{
    public interface INavigationManager
    {
        void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class;
    }
}

namespace CryptoApp.Presentation.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(object parameter = null) where TViewModel : class;
    }
}

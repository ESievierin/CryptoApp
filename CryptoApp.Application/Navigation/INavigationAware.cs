namespace CryptoApp.Presentation.Services.Navigation
{
    public interface INavigationAware
    {
        Task OnNavigatedTo(object parameter);
    }
}

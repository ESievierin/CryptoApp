namespace CryptoApp.ApplicationCore.Services.Navigation
{
    public interface INavigationAware
    {
        Task OnNavigatedTo(object parameter);
    }
}

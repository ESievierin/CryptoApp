using CryptoApp.ApplicationCore.Services.Navigation;
using CryptoApp.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace CryptoApp.Presentation.Services.Navigation
{
    public class FrameNavigationService(IServiceProvider serviceProvider, Frame frame) : INavigationManager
    {
        private readonly Dictionary<Type, Type> map = new();

        public void Register<TViewModel, TView>()
            where TViewModel : class
            where TView : Page
        {
            map[typeof(TViewModel)] = typeof(TView);
        }

        public void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class
        {
            if (map.TryGetValue(typeof(TViewModel), out var viewType))
            {
                var view = (Page)serviceProvider.GetRequiredService(viewType);
                var vm = serviceProvider.GetRequiredService<TViewModel>();

                view.DataContext = vm;

                if (vm is INavigationAware aware && parameter is not null)
                {
                    aware.OnNavigatedTo(parameter);
                }

                frame.Navigate(view);
            }
        }
    }
}

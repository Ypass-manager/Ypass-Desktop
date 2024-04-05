using System;
using System.Linq;
using YpassDesktop.ViewModels;

namespace YpassDesktop.Service
{
    public static class HomePageNavigationService
    {
        private static readonly NavigationServiceBase _navigationService = new NavigationServiceBase();

        public static event Action<BaseViewModel>? NavigationChanged
        {
            add => _navigationService.NavigationChanged += value;
            remove => _navigationService.NavigationChanged -= value;
        }

        public static void Initialize(BaseViewModel firstPage)
        {
            _navigationService.Initialize(firstPage);
        }

        public static void NavigateTo(BaseViewModel newPage, ParameterBuilder? parameterBuilder = null)
        {
            _navigationService.NavigateTo(newPage, parameterBuilder);
        }

        public static void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}

using System;
using System.Collections.Generic;
using YpassDesktop.ViewModels;

namespace YpassDesktop.Service
{
    public static class NavigationService
    {
        private static readonly Stack<BaseViewModel> _navigationHistory = new Stack<BaseViewModel>();

        public static event Action<BaseViewModel>? NavigationChanged;

        public static void Initialize(BaseViewModel firstPage)
        {
            _navigationHistory.Push(firstPage); // Add the first page to the history
        }
        public static void NavigateTo(BaseViewModel newPage)
        {
            _navigationHistory.Push(newPage); // Store the current page in the history
            OnNavigationChanged(newPage);
        }

        public static void GoBack()
        {
            if (_navigationHistory.Count > 1)
            {
                // Pop the current page from the history
                _navigationHistory.Pop();
                // Navigate back to the previous page
                var previousPage = _navigationHistory.Peek();
                OnNavigationChanged(previousPage);
            }
        }
        private static void OnNavigationChanged(BaseViewModel newPage)
        {
            NavigationChanged?.Invoke(newPage);
        }
    }
}

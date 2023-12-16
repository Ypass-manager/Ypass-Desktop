using DynamicData;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace YpassDesktop.ViewModels;
public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        // Set current page to first on start up
        _CurrentPage = new SimplePageViewModel();

        // Subscribe to the ChangePageObservable to handle page changes
        (_CurrentPage as BasePageViewModel)?.ChangePageObservable
                .Subscribe(newPage => setCurrentPage(newPage));

    }


    // The default is the first page
    private BaseViewModel _CurrentPage;

    /// <summary>
    /// Gets the current page. The property is read-only
    /// </summary>
    public BaseViewModel CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }

    public bool setCurrentPage(BaseViewModel page)
    {
        if (_CurrentPage != page)
        {
            CurrentPage = page;
            return true;
        }
        return false;
    }
}
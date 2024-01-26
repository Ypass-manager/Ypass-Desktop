using ReactiveUI;
using System;
using YpassDesktop.Service;
using YpassDesktop.Views;


namespace YpassDesktop.ViewModels;
public class MainWindowViewModel : BaseViewModel
{
    private BaseViewModel _CurrentPage;
    public MainWindowViewModel()
    {
        var simplePageViewModel= new InscriptionPageViewModel();
        NavigationService.Initialize(simplePageViewModel);
        
        //Subscribe to the service to know when a page has been change, and set the page
        NavigationService.NavigationChanged += newPage => SetCurrentPage(newPage);

        // First Page by default
        // _CurrentPage = simplePageViewModel;
        // Test value for inscription
        _CurrentPage = simplePageViewModel;
    }

    public BaseViewModel CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }

    public bool SetCurrentPage(BaseViewModel page)
    {
        if (_CurrentPage != page)
        {
            CurrentPage = page;
            return true;
        }
        return false;
    }
}
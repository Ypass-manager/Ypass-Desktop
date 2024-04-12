using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Linq;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;
using YpassDesktop.Views;


namespace YpassDesktop.ViewModels;
public class MainWindowViewModel : BaseViewModel
{
    private BaseViewModel _CurrentPage;
    public MainWindowViewModel()
    {   
        // PageViewModels available for testing
        var SimplePageViewModel= new NewOrExistentDatabasePageViewModel();
        var InscriptionPageViewModel = new InscriptionPageViewModel();
        var NewOrExistentDatabaseViewModel = new NewOrExistentDatabasePageViewModel();
        var HomePageViewModel = new AccountListPageViewModel();

        // For testing purposes, replace simplePageViewModel in NavigationService.Initialize() and in _CurrentPage with another PageViewModel
        NavigationService.Initialize(HomePageViewModel);
        
        //Subscribe to the service to know when a page has been change, and set the page
        NavigationService.NavigationChanged += newPage => SetCurrentPage(newPage);

        // First Page by default
        // _CurrentPage = simplePageViewModel;
        _CurrentPage = HomePageViewModel;

       
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
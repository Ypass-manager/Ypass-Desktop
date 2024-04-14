using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Windows.Input;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;
using YpassDesktop.Views;

namespace YpassDesktop.ViewModels;

public class HomePageViewModel : BaseViewModel
{
    private BaseViewModel _CurrentHomePage;
    public HomePageViewModel()
    {

        var HomePageViewModel = new ListAccountPageViewModel();

        HomePageNavigationService.Initialize(HomePageViewModel);

        //Subscribe to the service to know when a page has been change, and set the page
        HomePageNavigationService.NavigationChanged += newPage => SetCurrentHomePage(newPage);

        _CurrentHomePage = HomePageViewModel;

        AddAccountCommand = ReactiveCommand.Create(NavigateToAddAccountPage);
        ViewHistoryConnectionCommand = ReactiveCommand.Create(NavigateToHistoryConnectionPage);
        GoHomePageCommand = ReactiveCommand.Create(GoHomePage);
        DisconnectCommand = ReactiveCommand.Create(Disconnect);
    }

    public BaseViewModel CurrentHomePage
    {
        get { return _CurrentHomePage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentHomePage, value); }
    }

    public bool SetCurrentHomePage(BaseViewModel page)
    {
        if (_CurrentHomePage != page)
        {
            CurrentHomePage = page;
            return true;
        }
        return false;
    }
    public ICommand AddAccountCommand { get; }

    private void NavigateToAddAccountPage()
    {
        Service.HomePageNavigationService.NavigateTo(new AddAccountPageViewModel());
    }

    public ICommand ViewHistoryConnectionCommand { get; }
    private void NavigateToHistoryConnectionPage()
    {
        Service.HomePageNavigationService.NavigateTo(new HistoryConnectionPageViewModel());
    }
    public ICommand DisconnectCommand { get; }

    private void Disconnect()
    {
        AuthenticationService.Logout();

        var parameterBuilder = new ParameterBuilder();
        parameterBuilder.Add("resetHistoryNavigation", true);
        Service.MainWindowNavigationService.NavigateTo(new NewOrExistentDatabasePageViewModel(), parameterBuilder);
    }

    public ICommand GoHomePageCommand { get; }
    private void GoHomePage()
    {
        Service.HomePageNavigationService.ClearNavigationHistory();
        BaseViewModel homePage = new ListAccountPageViewModel();
        Service.HomePageNavigationService.Initialize(homePage);
        SetCurrentHomePage(homePage);
    }

    [Required]
    public string? DatabaseName
    {
        get
        {
            return EncryptionService.GetDatabaseName();
        }
    }
}
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
    // For now, exists only to make HomePageView.axaml available for testing
    // Will be worked on later

    private BaseViewModel _CurrentHomePage;
    public HomePageViewModel() {

        var HomePageViewModel = new EncryptTestPageViewModel();

        HomePageNavigationService.Initialize(HomePageViewModel);

        //Subscribe to the service to know when a page has been change, and set the page
        HomePageNavigationService.NavigationChanged += newPage => SetCurrentHomePage(newPage);

        _CurrentHomePage = HomePageViewModel;

        AddAccountCommand = ReactiveCommand.Create(NavigateToAddAccountPage);
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

    public ICommand DisconnectCommand { get; }

    private void Disconnect()
    {
        AuthenticationService.Logout();

        var parameterBuilder = new ParameterBuilder();
        parameterBuilder.Add("resetHistoryNavigation", true);
        Service.MainWindowNavigationService.NavigateTo(new NewOrExistentDatabasePageViewModel(), parameterBuilder);
    }

    private string? _databaseName;

    [Required]
    public string? DatabaseName
    {
        get
        {
            return AuthenticationService.GetDbName();
        }
    }
}

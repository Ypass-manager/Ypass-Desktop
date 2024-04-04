using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels;

public class HomePageViewModel : BaseViewModel
{
    // For now, exists only to make HomePageView.axaml available for testing
    // Will be worked on later
    public HomePageViewModel() {

        AddAccountCommand = ReactiveCommand.Create(NavigateToAddAccountPage);
        
    }

    public ICommand AddAccountCommand { get; }

    private void NavigateToAddAccountPage()
    {
        Service.NavigationService.NavigateTo(new AccountPageViewModel());
    }
}

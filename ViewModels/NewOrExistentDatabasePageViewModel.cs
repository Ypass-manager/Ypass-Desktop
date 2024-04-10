using ReactiveUI;
using System;
using System.Windows.Input;
using Tmds.DBus.Protocol;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels;

public class NewOrExistentDatabasePageViewModel : BaseViewModel
{
    public NewOrExistentDatabasePageViewModel()
    {
        // Let the user choose if they want to create a new database or if they want to use an existent one
        
        NavigateToConnexionPageCommand = ReactiveCommand.Create(NavigateToConnexionPage);
        NavigateToInscriptionPageCommand = ReactiveCommand.Create(NavigateToInscriptionPage);

    }

    public override void Initialize()
    {
        if (NavigationParameter is ParameterBuilder param)
        {
            bool resetHistoryNav = param.Get<bool>("resetHistoryNavigation");
            if (resetHistoryNav)
            {
                MainWindowNavigationService.ClearNavigationHistory();
                HomePageNavigationService.ClearNavigationHistory();
                MainWindowNavigationService.Initialize(this);
            }
        }


    }

    public ICommand NavigateToConnexionPageCommand { get; }
    public ICommand NavigateToInscriptionPageCommand { get; }



    private void NavigateToConnexionPage()
    {
        Service.MainWindowNavigationService.NavigateTo(new ConnectionPageViewModel());
    }

    private void NavigateToInscriptionPage()
    {
        Service.MainWindowNavigationService.NavigateTo(new InscriptionPageViewModel());
    }
}


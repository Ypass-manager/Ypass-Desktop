using ReactiveUI;
using System;
using System.Windows.Input;

namespace YpassDesktop.ViewModels;

public class NewOrExistentDatabasePageViewModel : BaseViewModel
{
    public NewOrExistentDatabasePageViewModel()
    {
        // Let the user choose if they want to create a new database or if they want to use an existent one
        
        NavigateToConnexionPageCommand = ReactiveCommand.Create(NavigateToConnexionPage);
        NavigateToInscriptionPageCommand = ReactiveCommand.Create(NavigateToInscriptionPage);
        
        GoBackCommand = ReactiveCommand.Create(GoBack);
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
    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO THE PREVIOUS PAGE");
        Service.MainWindowNavigationService.GoBack();

    }
}


using ReactiveUI;
using System;
using System.Windows.Input;

namespace YpassDesktop.ViewModels;

public class NewOrExistentDatabasePageViewModel : BaseViewModel
{
    public NewOrExistentDatabasePageViewModel()
    {
        // Let the user choose if they want to create a new database or if they want to use an existent one
        NavigateNextCommand = ReactiveCommand.Create<string>(NavigateNext);
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    // WIP

    public ICommand NavigateNextCommand { get; }
    private void NavigateNext(string userChoice)
    {
        if (userChoice == "ExistentListCommand")
        {
            Service.NavigationService.NavigateTo(new ConnectionPageViewModel());
        }
        else if (userChoice == "NewListCommand")
        {
            Service.NavigationService.NavigateTo(new NewDatabasePageViewModel());
        }

    }

    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO THE PREVIOUS PAGE");
        Service.NavigationService.GoBack();

    }
}


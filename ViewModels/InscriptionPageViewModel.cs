using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels;

public class InscriptionPageViewModel : BaseViewModel
{
    private ManagerAccount _managerAccount;

    public InscriptionPageViewModel()
    {
        // Listen to changes of DatabaseName, Password and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.DatabaseName, x => x.Password, x => x.ConfirmPassword)
            .Subscribe(_ => UpdateCanNavigateNext());

        var canNavNext = this.WhenAnyValue(x => x.CanNavigateNext);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        NavigateToConnexionPageCommand = ReactiveCommand.Create(NavigateToConnexionPage);
    }

    private string? _databaseName;

    [Required]
    public string? DatabaseName
    {
        get { return _databaseName; }
        set { this.RaiseAndSetIfChanged(ref _databaseName, value); }
    }

    private string? _password;

    [Required]
    [PasswordPropertyText]
    public string? Password
    {
        get { return _password; }
        set { this.RaiseAndSetIfChanged(ref _password, value); }
    }

    private string? _confirmPassword;

    [Required]
    [PasswordPropertyText]
    public string? ConfirmPassword
    {
        get { return _confirmPassword; }
        set { this.RaiseAndSetIfChanged(ref _confirmPassword, value); }
    }

    private bool _CanNavigateNext;

    public bool CanNavigateNext
    {
        get { return _CanNavigateNext; }
        protected set { this.RaiseAndSetIfChanged(ref _CanNavigateNext, value); }
    }

    // Update CanNavigateNext. Allow next page if E-Mail and Password are valid
    private void UpdateCanNavigateNext()
    {
        CanNavigateNext =
                !string.IsNullOrEmpty(_databaseName)
            && !string.IsNullOrEmpty(_password) && !string.IsNullOrEmpty(_confirmPassword)
            && (_password.Equals(_confirmPassword));



    }


    public ICommand NavigateNextCommand { get; }
    private void NavigateNext()
    {
        try
        {
            
            EncryptionService.InitializeDatabaseWithMasterPassword(Password, DatabaseName);
            // Database initialization successful, navigate to the next page or perform any additional logic
            var parameterBuilder = new ParameterBuilder();
            parameterBuilder.Add("email", DatabaseName);
            parameterBuilder.Add("password", Password);

            AuthenticationService.Login();

            Service.NavigationService.NavigateTo(new ThirdPageViewModel(), parameterBuilder);
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            Console.WriteLine($"Error initializing database: {ex.Message}");
            // Optionally, show a message to the user indicating that there was an error
        }

    }

    public ICommand NavigateToConnexionPageCommand { get; }
    private void NavigateToConnexionPage()
    {
        Service.NavigationService.NavigateTo(new ConnectionPageViewModel());
    }

    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO THE PREVIOUS PAGE");
        Service.NavigationService.GoBack();

    }
}

using ReactiveUI;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Windows.Input;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels;

public class ConnectionViewModel : BaseViewModel
{
    public ConnectionViewModel()
    {
        // Listen to user's input and compare it to data from a database and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.DatabaseName, x => x.PasswordInput)
                .Subscribe(_ => UpdateCanLogin());

        var canLogin = this.WhenAnyValue(x => x.CanLogin);

        LoginCommand = ReactiveCommand.Create(Login, canLogin);
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    private string? _databaseName;

    [Required]
    public string? DatabaseName
    {
        get { return _databaseName; }
        set { this.RaiseAndSetIfChanged(ref _databaseName, value); }
    }

    private string? _passwordInput;

    [Required]
    [PasswordPropertyText]
    public string? PasswordInput
    {
        get { return _passwordInput; }
        set { this.RaiseAndSetIfChanged(ref _passwordInput, value); }
    }

    /*
    private string? _databasePassword;

    [Required]
    [PasswordPropertyText]
    public string? DatabasePassword
    {
        get { return _databasePassword; }
        set { this.RaiseAndSetIfChanged(ref _databasePassword, value); }
    }
    */

    private bool _canLogin;

    public bool CanLogin
    {
        get { return _canLogin; }
        protected set { this.RaiseAndSetIfChanged(ref _canLogin, value); }
    }

    private void UpdateCanLogin()
    {
        CanLogin = !string.IsNullOrEmpty(_databaseName) && !string.IsNullOrEmpty(_passwordInput);
    }

    public ICommand LoginCommand { get; }
    private void Login()
    {   
        if (AuthenticationService.IsLoggedIn)
        {
            try
            {
                EncryptionService.LoadDatabaseWithMasterPassword(PasswordInput, DatabaseName);

                var parameterBuilder = new ParameterBuilder();
                parameterBuilder.Add("databaseName", DatabaseName);
                parameterBuilder.Add("passwordInput", PasswordInput);

                Service.NavigationService.NavigateTo(new ThirdPageViewModel(), parameterBuilder);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error can't connect to database: {ex.Message}");
                // Optionally, show a message to the user indicating that there was an error
            }
        }
    }

    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO THE PREVIOUS PAGE");
        Service.NavigationService.GoBack();
    }
}
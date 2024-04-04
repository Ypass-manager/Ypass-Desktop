using ReactiveUI;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Windows.Input;
using YpassDesktop.Service;
using static YpassDesktop.Service.EncryptionService;

namespace YpassDesktop.ViewModels;

public class ConnectionPageViewModel : BaseViewModel
{
    public ConnectionPageViewModel()
    {
        // Listen to user's input and compare it to data from a database and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.DatabaseName, x => x.PasswordInput)
                .Subscribe(_ => UpdateCanLogin());

        var canLogin = this.WhenAnyValue(x => x.CanLogin);

        LoginCommand = ReactiveCommand.Create(Login, canLogin);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        NavigateToInscriptionPageCommand = ReactiveCommand.Create(NavigateToInscriptionPage);
    }
    private string _connectionStatus;
    public string ConnectionStatus
    {
        get => _connectionStatus;
        set {
            if(IsConnectionStatusVisible != true) {IsConnectionStatusVisible = true; }
            this.RaiseAndSetIfChanged(ref _connectionStatus, value);
        }
    }

    private bool _isConnectionStatusVisible;
    public bool IsConnectionStatusVisible
    {
        get => _isConnectionStatusVisible;
        set => this.RaiseAndSetIfChanged(ref _isConnectionStatusVisible, value);
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
        IsConnectionStatusVisible = false;
        CanLogin = !string.IsNullOrEmpty(_databaseName) && !string.IsNullOrEmpty(_passwordInput);
    }

    public ICommand LoginCommand { get; }
    private void Login()
    {   
        if (!AuthenticationService.IsLoggedIn)
        {
            try
            {
                string? masterPassword = PasswordInput;
                string? databaseName = DatabaseName;
                if(string.IsNullOrEmpty(masterPassword) || string.IsNullOrEmpty(databaseName))
                {
                    return;
                }
                EncryptionService.LoadDatabaseWithMasterPassword(masterPassword, databaseName);

                var parameterBuilder = new ParameterBuilder();
                parameterBuilder.Add("databaseName", DatabaseName);
                parameterBuilder.Add("passwordInput", PasswordInput);
                ConnectionStatus = "Connection successful";
                AuthenticationService.Login();
                Service.NavigationService.NavigateTo(new HomePageViewModel());
                //Service.NavigationService.NavigateTo(new ThirdPageViewModel(), parameterBuilder);
            }
            catch (IncorrectMasterPasswordException ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Login failed: {ex.Message}");
                ConnectionStatus = "Database / Password is incorrect.";
                // Optionally, show a message to the user indicating that there was an error
            }
            catch (Exception ex){
                Console.WriteLine($"Unmanaged error : {ex.Message}");
                ConnectionStatus = ex.Message.ToString();
            }
            return;
        }
        Service.NavigationService.NavigateTo(new HomePageViewModel());
        ConnectionStatus = "Already connect.";
    }

    public ICommand NavigateToInscriptionPageCommand { get; }
    private void NavigateToInscriptionPage()
    {
        Service.NavigationService.NavigateTo(new InscriptionPageViewModel());
    }
    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO THE PREVIOUS PAGE");
        Service.NavigationService.GoBack();
    }
}
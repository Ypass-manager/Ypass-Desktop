using ReactiveUI;
using System;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using YpassDesktop.Service;
using System.Windows.Forms;
using static YpassDesktop.Service.EncryptionService;
using YpassDesktop.DataAccess;

namespace YpassDesktop.ViewModels;

public class ConnectionPageViewModel : BaseViewModel
{
    private Interaction<Unit, string?> openFileDialogInteraction;
    public ConnectionPageViewModel()
    {
        // Listen to user's input and compare it to data from a database and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.DatabaseName, x => x.PasswordInput)
                .Subscribe(_ => UpdateCanLogin());

        var canLogin = this.WhenAnyValue(x => x.CanLogin);

        LoginCommand = ReactiveCommand.Create(Login, canLogin);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        NavigateToInscriptionPageCommand = ReactiveCommand.Create(NavigateToInscriptionPage);

        // File dialog

        openFileDialogInteraction = new Interaction<Unit, string?>();
        OpenFileDialogCommand = ReactiveCommand.CreateFromObservable(OpenFileDialog);

        openFileDialogInteraction.RegisterHandler(interaction =>
        {
            var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Database File";
            openFileDialog.Filter = "Database Files (*.db)|*.db|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = localAppDataPath;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Pass the selected file path to the interaction result
                string fileName = Path.GetFileName(openFileDialog.FileName);
                interaction.SetOutput(fileName);
            }
            else
            {
                // If the user cancels, set the result to null
                interaction.SetOutput(null);
            }
        });
    }

    public ReactiveCommand<Unit, Unit> OpenFileDialogCommand { get; }
    private IObservable<Unit> OpenFileDialog()
    {
        // Trigger the interaction and return the result
        return openFileDialogInteraction.Handle(Unit.Default)
            .Select(selectedFileName =>
            {
                // Perform actions with the selected file's name
                DatabaseName = selectedFileName;
                return Unit.Default;
            })
            .Catch<Unit, Exception>(ex =>
            {
                // Handle any errors
                Console.WriteLine($"Error opening file dialog: {ex.Message}");
                return Observable.Empty<Unit>();
            });
    }

    private string? _connectionStatus;
    public string? ConnectionStatus
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
                MainWindowNavigationService.NavigateTo(new HomePageViewModel());
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
        MainWindowNavigationService.NavigateTo(new HomePageViewModel());
        ConnectionStatus = "Already connect.";
    }

    public ICommand NavigateToInscriptionPageCommand { get; }
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
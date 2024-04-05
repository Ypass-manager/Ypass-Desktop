using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using YpassDesktop.Service;
namespace YpassDesktop.ViewModels;
public class SecondPageViewModel : BaseViewModel
{
    public SecondPageViewModel()
    {
        
        // Listen to changes of MailAddress and Password and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.MailAddress, x => x.Password)
            .Subscribe(_ => UpdateCanNavigateNext());

        var canNavNext = this.WhenAnyValue(x => x.CanNavigateNext);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    private string? _MailAddress;

    /// <summary>
    /// The E-Mail of the user. This field is required
    /// </summary>
    [Required]
    [EmailAddress]
    public string? MailAddress
    {
        get { return _MailAddress; }
        set { this.RaiseAndSetIfChanged(ref _MailAddress, value); }
    }

    private string? _Password;

    /// <summary>
    /// The password of the user. This field is required.
    /// </summary>
    [Required]
    public string? Password
    {
        get { return _Password; }
        set { this.RaiseAndSetIfChanged(ref _Password, value); }
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
                !string.IsNullOrEmpty(_MailAddress)
            && _MailAddress.Contains("@")
            && !string.IsNullOrEmpty(_Password);
    }

    
    public ICommand NavigateNextCommand { get; }
    private void NavigateNext()
    {
        var parameterBuilder = new ParameterBuilder();
        parameterBuilder.Add("email", MailAddress);
        parameterBuilder.Add("password", Password);

        AuthenticationService.Login();

        Service.MainWindowNavigationService.NavigateTo(new ThirdPageViewModel(), parameterBuilder);
        
    }

    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO HE PREVIOUS PAGE");
        Service.MainWindowNavigationService.GoBack();
        
    }
}
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace YpassDesktop.ViewModels;

public class InscriptionViewModel : BaseViewModel
{
    public InscriptionViewModel()
    {
        // Listen to changes of DatabaseName, Password and ConfirmPassword and update CanNavigateNext accordingly

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

    // WIP
}

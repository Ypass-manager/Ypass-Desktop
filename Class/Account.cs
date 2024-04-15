using ReactiveUI;
using System;
using System.Windows.Input;
using YpassDesktop.ViewModels;

namespace YpassDesktop.Class;

public class Account : BaseViewModel
{
    // For now, exists only to make HomePageView.axaml available for testing
    // Will be worked on later

    private string _passwordEncrypt = string.Empty;
    private bool _isPasswordVisible = false;
    public Account() {

        TogglePasswordVisibilityCommand = ReactiveCommand.Create(tooglePasswordVisibility);

    }
    
    public string? Username { get; set; }
    public string Password
    {
        get
        {
            if (IsPasswordVisible)
                return Service.EncryptionService.DecryptPassword(_passwordEncrypt);
            else
                return new string('*', _passwordEncrypt.Length);
        }
        set => this.RaiseAndSetIfChanged(ref _passwordEncrypt, value);
    }
    public void TogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }
    public string? Title { get; set;}
    public bool IsPasswordVisible
    {
        get { return _isPasswordVisible; }
        set
        {
            this.RaiseAndSetIfChanged(ref _isPasswordVisible, value);
            this.RaisePropertyChanged(nameof(Password));
        }
    }
    public ICommand TogglePasswordVisibilityCommand { get; }

    private void tooglePasswordVisibility()
    {
        TogglePasswordVisibility();
    }

}


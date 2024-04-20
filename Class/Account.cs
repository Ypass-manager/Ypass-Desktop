using ReactiveUI;
using System;
using System.Windows.Input;
using System.Windows.Forms;
using YpassDesktop.ViewModels;
using System.ComponentModel;

namespace YpassDesktop.Class;

public class Account : BaseViewModel, INotifyPropertyChanged
{
    // For now, exists only to make HomePageView.axaml available for testing
    // Will be worked on later

    private string _passwordEncrypt = string.Empty;
    private bool _isPasswordVisible = false;
    private string? _username = string.Empty;
    private string? _title;
    public Account() {

        TogglePasswordVisibilityCommand = ReactiveCommand.Create(tooglePasswordVisibility);
        CopyPasswordCommand = ReactiveCommand.Create(copyPassword);
    }
    
    public string? Username
    {
        get
        {
            if (_username == string.Empty)
                return "[Not definied]";
            return _username;
        }
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }
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
    public string? Title {
        get
        {
            if (_title == string.Empty)
                return "[Not definied]";
            return _title;
        }
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
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

    public ICommand CopyPasswordCommand { get; }

    private void copyPassword()
    {
        Clipboard.SetText(Service.EncryptionService.DecryptPassword(_passwordEncrypt));
    }
}


using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using AccountObj = YpassDesktop.Class.Account;
namespace YpassDesktop.ViewModels;

public class ListAccountPageViewModel : BaseViewModel, INotifyPropertyChanged
{
    private ObservableCollection<AccountObj>? _accounts;

    public ListAccountPageViewModel() {

        Accounts = new ObservableCollection<AccountObj>();

        Service.EncryptionService.LoadDatabaseWithMasterPassword("mdp", "HomePageDB.db");
        AuthenticationService.Login();
        YpassDbContext _dbContext = new YpassDbContext(Service.EncryptionService.GetDatabaseName());
        foreach (var account in new Account(_dbContext).GetAllAccount())
        {
            Accounts.Add(new AccountObj { Username = account.Username, Password = account.Password, Title = account.Title });
        }

    }

    public ObservableCollection<AccountObj>? Accounts
    {
        get => _accounts;
        set => this.RaiseAndSetIfChanged(ref _accounts, value);
    }
}

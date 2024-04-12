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
    // For now, exists only to make HomePageView.axaml available for testing
    // Will be worked on later
    protected readonly YpassDbContext _dbContext;
    private ObservableCollection<AccountObj> _accounts;

    public ListAccountPageViewModel() {

        _dbContext = new YpassDbContext("HomePageDB.db");
        try
        {
            EncryptionService.LoadDatabaseWithMasterPassword("mdp", "HomePageDB.db");
        }
        catch
        {
            EncryptionService.InitializeDatabaseWithMasterPassword("mdp", "HomePageDB.db");
        }

        Accounts = new ObservableCollection<AccountObj>
        {
            new AccountObj { Email = "custom1@gmail.com", Password = "*******************" },
            new AccountObj { Email = "custom2@gmail.com", Password = "*******************" },
        };

    }

    public ObservableCollection<AccountObj> Accounts
    {
        get => _accounts;
        set => this.RaiseAndSetIfChanged(ref _accounts, value);
    }
}

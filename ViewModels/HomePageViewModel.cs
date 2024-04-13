using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly YpassDbContext _dbContext;

        // Define properties to hold the data
        private ObservableCollection<WebsiteViewModel> _websites;
        public ObservableCollection<WebsiteViewModel> Websites
        {
            get => _websites;
            set => this.RaiseAndSetIfChanged(ref _websites, value);
        }

        public ICommand TogglePasswordVisibilityCommand { get; }

        public HomePageViewModel()
        {
            _dbContext = new YpassDbContext("HomePageDB.db");

            try
            {
                EncryptionService.LoadDatabaseWithMasterPassword("mdp", "HomePageDB.db");
            }
            catch
            {
                EncryptionService.InitializeDatabaseWithMasterPassword("mdp", "HomePageDB.db");
            }

            // Group accounts by WebsiteName
            var groupedAccounts = _dbContext.Account
                .Select(account => new AccountViewModel
                {
                    Username = account.Username,
                    Password = account.Password,
                    WebsiteName = account.Title,
                    WebsiteUrl = account.WebsiteUrl
                })
                .GroupBy(accountViewModel => accountViewModel.WebsiteName)
                .OrderBy(group => group.Key); // Sort groups by WebsiteName

            // Create GroupViewModel for each website
            Websites = new ObservableCollection<WebsiteViewModel>(
                groupedAccounts.Select(website => new WebsiteViewModel
                {
                    WebsiteName = website.Key,
                    Accounts = new ObservableCollection<AccountViewModel>(website.OrderBy(account => account.Username))
            })
);

            TogglePasswordVisibilityCommand = ReactiveCommand.Create(TogglePasswordVisibility);
        }

        private void TogglePasswordVisibility()
        {
            // Toggle the visibility of the password property in each account view model
            foreach (var website in Websites)
            {
                foreach (var account in website.Accounts)
                {
                    account.IsPasswordVisible = !account.IsPasswordVisible;
                }
            }
        }
    }

    // Define a view model for Account
    public class AccountViewModel : ReactiveObject
    {
        private string _username;
        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set => this.RaiseAndSetIfChanged(ref _isPasswordVisible, value);
        }

        private string _websiteName;
        public string WebsiteName
        {
            get => _websiteName;
            set => this.RaiseAndSetIfChanged(ref _websiteName, value);
        }

        private string? _websiteUrl;
        public string? WebsiteUrl
        {
            get => _websiteUrl;
            set => this.RaiseAndSetIfChanged(ref _websiteUrl, value);
        }
    }

    public class WebsiteViewModel
    {
        public string WebsiteName { get; set; }
        public ObservableCollection<AccountViewModel> Accounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using YpassDesktop.Service;
using YpassDesktop.DataAccess;
using System.Windows.Input;

namespace YpassDesktop.ViewModels
{
    public class AccountPageViewModel: BaseViewModel
    {
        public AccountPageViewModel() {
            // Listen to changes of MailAddress and Password and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.Title, x => x.AccountPassword, x => x.AccountUsername)
        .Subscribe(_ => UpdateCanAddAccount());

        var canAddAccount = this.WhenAnyValue(x => x.CanAddAccount);
        
        AddAccountCommand = ReactiveCommand.Create(AddAccount, canAddAccount);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        }

        private void UpdateCanAddAccount()
        {
            CanAddAccount =
                    !string.IsNullOrEmpty(_Title)
                && !string.IsNullOrEmpty(_AccountUsername)
                && !string.IsNullOrEmpty(_AccountPassword);
    }   
        private string? _Title;
        public string? Title
        {
            get { return _Title; }
            set
            {
                this.RaiseAndSetIfChanged(ref _Title, value);
            }
        }

        private string? _WebsiteUrl;
        public string? WebsiteUrl
        {
            get { return  _WebsiteUrl; }
            set
            {
                this.RaiseAndSetIfChanged(ref _WebsiteUrl, value);
            }
        }

        private string? _AccountUsername;
        public string? AccountUsername
        {
            get { return _AccountUsername; }
            set
            {
                this.RaiseAndSetIfChanged(ref _AccountUsername, value);
            }
        }
        private string? _AccountPassword;
        public string? AccountPassword
        {
            get { return _AccountPassword; }
            set
            {
                this.RaiseAndSetIfChanged(ref _AccountPassword, value);
            }
        }

        private bool _CanAddAccount;

        public bool CanAddAccount
        {
            get { return _CanAddAccount; }
            protected set { this.RaiseAndSetIfChanged(ref _CanAddAccount, value); }
        }

        public ICommand AddAccountCommand { get; }
        private void AddAccount()
        {
            string database_name = AuthenticationService.GetDbName();
            byte[]? salt_derived_key = AuthenticationService.GetSaltDerivedKey();
            if (salt_derived_key != null)
            {
                EncryptionService.LoadDatabaseWithSaltDerivationKey(salt_derived_key, database_name);
            }
                
            Account account = new Account(new YpassDbContext(database_name));
            if(!string.IsNullOrEmpty(Title)){account.Title = Title;}
            if(!string.IsNullOrEmpty(AccountUsername)) { account.Username = AccountUsername; }
            
            if(!string.IsNullOrEmpty(AccountPassword)) {
                account.SetPassword(EncryptionService.EncryptPassword(AccountPassword));
            }
            account.Save();
            
            //Service.HomePageNavigationService.GoBack();
        }

        public ICommand GoBackCommand { get; }
        private void GoBack()
        {
            Console.WriteLine("GO BACK TO THE PREVIOUS PAGE");
            Service.HomePageNavigationService.GoBack();

        }

    }
}

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
        protected readonly YpassDbContext _dbContext;
        public AccountPageViewModel() {
            // Listen to changes of MailAddress and Password and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.Title, x => x.AccountPassword, x => x.AccountUsername)
        .Subscribe(_ => UpdateCanAddAccount());

        var canAddAccount = this.WhenAnyValue(x => x.CanAddAccount);
        _dbContext = new YpassDbContext("YpassDB.db");
        AddAccountCommand = ReactiveCommand.Create(AddAccount, canAddAccount);
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
            Service.NavigationService.NavigateTo(new ThirdPageViewModel());
            Account account = new Account(_dbContext);
            //ManagerAccount accountManager = new ManagerAccount(_dbContext);
            //accountManager.SetDatabase("dede");
            //accountManager.Save();
            //account.title ="test";
            account.SetTitle("test");
            account.Save();
            //var AccountDB = new Account(new YpassDbContext("YpassDB.db"));
            //if(!string.IsNullOrEmpty(Title)){account.Title = Title;}
            //if(!string.IsNullOrEmpty(AccountUsername)) { account.Username = AccountUsername; }
            //if(!string.IsNullOrEmpty(AccountPassword)) { account.Password = AccountPassword; }
            //_dbContext.Account.Add(account);
            //_dbContext.SaveChanges();
            
            
        }

    }
}

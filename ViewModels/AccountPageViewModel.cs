using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using YpassDesktop.Service;
using YpassDesktop.DataAccess;

namespace YpassDesktop.ViewModels
{
    public class AccountPageViewModel:BaseViewModel
    {
        public AccountPageViewModel() {
            EncryptionService.InitializeDatabaseWithMasterPassword("J'adore7!mon$dechaTBec1ause;it1sLakeThat", "YpassDB.db");
        }

        private string? _title;
        public String? title
        {
            get { return _title; }
            set
            {
                this.RaiseAndSetIfChanged(ref _title, value);
            }
        }

        public String? websiteUrl
        {
            get { return  websiteUrl; }
        }

        private string? _accountUsername;
        public String? accountUsername
        {
            get { return accountUsername; }
            set
            {
                this.RaiseAndSetIfChanged(ref _accountUsername, value);
            }
        }

        private string? _accountPassword;
        public String? accountPassword
        {
            get { return accountPassword; }
            set
            {
                this.RaiseAndSetIfChanged(ref _accountPassword, value);
            }
        }

        public Boolean? isChecked
        {
            get { return isChecked; }
        }

        //Binding du button
        /*public void sendForm
        {

        }*/
    }
}

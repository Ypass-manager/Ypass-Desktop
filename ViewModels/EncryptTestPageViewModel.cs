using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels
{
    public class EncryptTestPageViewModel : BaseViewModel
    {

        public EncryptTestPageViewModel() {


            password = "LeMotdePasseDeToto";

            EncryptionService.InitializeDatabaseWithMasterPassword("J'adore7!mon$dechaTBec1ause;it1sLakeThat");

            string _encryptPassword = EncryptionService.EncryptPassword(password);
            string _decryptPassword = EncryptionService.DecryptPassword(_encryptPassword);
            encryptPassword = _encryptPassword;
            decryptPassword = _decryptPassword;

        }


        
        public string password { get; }
        public string encryptPassword { get; }
        public string decryptPassword { get; }


    }
}

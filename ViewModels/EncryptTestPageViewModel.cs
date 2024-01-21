using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;
using static YpassDesktop.Service.EncryptionService;

namespace YpassDesktop.ViewModels
{
    public class EncryptTestPageViewModel : BaseViewModel
    {

        public EncryptTestPageViewModel() {



            password = "LeMotdePasseDeToto";

            //EncryptionService.InitializeDatabaseWithMasterPassword("J'adore7!mon$dechaTBec1ause;it1sLakeThat", "YpassDB.db");
            try
            {
                EncryptionService.LoadDatabaseWithMasterPassword("J'adore7!mon$dechaTBec1ause;it1sLakeThat", "YpassDB.db");
               
            string _encryptPassword = EncryptionService.EncryptPassword(password);
            string _decryptPassword = EncryptionService.DecryptPassword(_encryptPassword);
            encryptPassword = _encryptPassword;
            decryptPassword = _decryptPassword;
            }
            catch(IncorrectMasterPasswordException ex)
            {
                Console.Write(ex.Message);
                decryptPassword = "Incorrect Master Password";
            }
            

            

        }


        
        public string password { get; }
        public string encryptPassword { get; }
        public string decryptPassword { get; }


    }
}

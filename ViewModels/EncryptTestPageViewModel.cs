using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace YpassDesktop.ViewModels
{
    public class EncryptTestPageViewModel : BaseViewModel
    {

        public EncryptTestPageViewModel() {

            // For this example, the Master Password is
            var master_password = Encoding.UTF8.GetBytes("J'adore7!mon$dechaTBec1ause;it1sLakeThat");
            // Generate the SALT_DERIVED KEY
            var salt_derived_key = Utils.HashTool.GenerateRandomSalt(); // WE STORE IT TO THE DATABASE AND GET IT LATER

            //Let's derivate it to obtain the derivation key for AES (the key part)

            var derivation_key_with_salt = Utils.HashTool.DeriveKey(master_password, salt_derived_key);

            // Let's derivate this derivation_key_with_salt to obtain a the IV for aes

            
            byte[] IV = Utils.EncryptionTool.GenerateIV(); // ON STOCKE IV en base de données par la suite, et on le récupére
      

            // Now we generate the CRITICAL SALT that will be encrypt with AES

            var SALT_CRITICAL = Encoding.UTF8.GetString(Utils.HashTool.GenerateRandomSalt());
            Console.WriteLine("SALT CRITICAL BEFORE ENCRYPT : ", SALT_CRITICAL);

            var SALT_CRITICAL_ENCRYPT = Utils.EncryptionTool.EncryptStringToBytes_Aes(SALT_CRITICAL, derivation_key_with_salt, IV);
            Console.WriteLine("SALT CRITICAL ENCRYPT", SALT_CRITICAL_ENCRYPT);

            // WE STORE IN THE DATABASE THE SALT_CRITICAL_ENCRYPT


            // ------------------------------------------------------------ //

            // Now let's encrypt a user password

            // WE SHOULD FIRST GET THE SALT_CRITICAL

            var salt_critical_decrypt = Utils.EncryptionTool.DecryptStringFromBytes_Aes(SALT_CRITICAL_ENCRYPT, derivation_key_with_salt, IV); // IV EST RECUPERER DE LA BASE DE DONNES AINSI QUE SALT CRITICAL ENCRYPT
            Console.WriteLine("SALT CRITICAL AFTER DECRYPT", salt_critical_decrypt);
            var encryptPassword = Utils.EncryptionTool.EncryptStringToBytes_Aes("LeMotdePasseDeToto", Encoding.UTF8.GetBytes(salt_critical_decrypt), IV);





            // exposer our variable to the front
            SaltCritical = SALT_CRITICAL;
            SaltCriticalEncrypt = Convert.ToBase64String(SALT_CRITICAL_ENCRYPT);
            SaltCriticalDecrypt = salt_critical_decrypt;
        }


        public string SaltCritical { get; }
        public string SaltCriticalEncrypt { get; }
        public string SaltCriticalDecrypt { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Tmds.DBus.Protocol;
namespace YpassDesktop.Service
{
    public static class EncryptionService
    {
        
        private static byte[]? SALT_CRITICAL_ENCRYPT;
        private static byte[]? derivation_key_with_salt;
        private static byte[]? IV;
        public static void InitializeDatabaseWithMasterPassword(string master_password)
        {
            if (master_password == null)
            {
                throw new Exception("Master Password should be set");
            }
            
            var PASSWORD = Encoding.UTF8.GetBytes(master_password);
            
            // Generate the SALT_DERIVED KEY
            var salt_derived_key = Utils.HashTool.GenerateRandomSalt(); // TODO : WE STORE IT TO THE DATABASE AND GET IT LATER

            //Let's derivate it to obtain the derivation key for AES (the key part)
            derivation_key_with_salt = Utils.HashTool.DeriveKey(PASSWORD, salt_derived_key);

            // We generate the unique IV for aes
            IV = Utils.EncryptionTool.GenerateIV(); // ON STOCKE IV en base de données par la suite, et on le récupére

            // Now we generate the CRITICAL SALT that will be encrypt with AES
            var SALT_CRITICAL = Encoding.UTF8.GetString(Utils.HashTool.GenerateRandomSalt());
           
            SALT_CRITICAL_ENCRYPT = Utils.EncryptionTool.EncryptStringToBytes_Aes(SALT_CRITICAL, derivation_key_with_salt, IV);

            // WE STORE IN THE DATABASE THE SALT_CRITICAL_ENCRYPT

            // TODO
            // ------------------------------------------------------------ //

        }

        public static string EncryptPassword(string password)
        {
            if (SALT_CRITICAL_ENCRYPT == null || derivation_key_with_salt == null || IV == null)
            {
                // Handle the case where any of the variables is null
                // (e.g., throw an exception, log an error, etc.)
                throw new ArgumentNullException("One or more required parameters are null");
            }
            var salt_critical_decrypt = DecryptSaltCritical();
            var encryptPassword = Utils.EncryptionTool.EncryptStringToBytes_Aes(password, salt_critical_decrypt, IV);

            return Convert.ToBase64String(encryptPassword);

        }

        public static string DecryptPassword(string encrypt_password)
        {
            var salt_critical_decrypt = DecryptSaltCritical();
            var decryptPassword = Utils.EncryptionTool.DecryptStringFromBytes_Aes(Convert.FromBase64String(encrypt_password!), salt_critical_decrypt!, IV!);
            return decryptPassword;
        }


        private static byte[] DecryptSaltCritical()
        {
            string decrypt_pass = Utils.EncryptionTool.DecryptStringFromBytes_Aes(SALT_CRITICAL_ENCRYPT!, derivation_key_with_salt!, IV!); // IV EST RECUPERER DE LA BASE DE DONNES AINSI QUE SALT CRITICAL ENCRYPT
            return Encoding.UTF8.GetBytes(decrypt_pass);
        }
    }
}

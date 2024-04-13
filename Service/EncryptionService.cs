using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Tmds.DBus.Protocol;
using YpassDesktop.DataAccess;
using System.Linq.Expressions;
using System.Security.Cryptography;
namespace YpassDesktop.Service
{
    public static class EncryptionService
    {

        private static string? DATABASE_NAME;
        private static byte[]? SALT_CRITICAL_ENCRYPT;
        private static byte[]? derivation_key_with_salt;
        private static byte[]? IV;

        /// <summary>
        /// Initializes a new database with the specified master password. To be use only when the user register for the first time.
        /// </summary>
        /// <param name="master_password">The master password for accessing the database.</param>
        /// <param name="database_name">The name of the database to initialize.</param>
        public static void InitializeDatabaseWithMasterPassword(string master_password, string database_name)
        {
            if (master_password == null)
            {
                throw new Exception("Master Password should be set");
            }
            using (var ManagerAccountDB = new ManagerAccount(new YpassDbContext(database_name)))
            {

                var PASSWORD = Encoding.UTF8.GetBytes(master_password);

                // Generate the SALT_DERIVED KEY
                var salt_derived_key = Utils.HashTool.GenerateRandomSalt();
                // WE STORE IT TO THE DATABASE AND GET IT LATER
                ManagerAccountDB.SetSalt(salt_derived_key);

                //Let's derivate it to obtain the derivation key for AES (the key part)
                derivation_key_with_salt = Utils.HashTool.DeriveKey(PASSWORD, salt_derived_key);

                // We generate the unique IV for aes
                IV = Utils.EncryptionTool.GenerateIV();
                // WE STORE THE IV IN THE DATABASE
                ManagerAccountDB.SetIV(IV);

                // Now we generate the CRITICAL SALT that will be encrypt with AES
                var SALT_CRITICAL = Encoding.UTF8.GetString(Utils.HashTool.GenerateRandomSalt());

                SALT_CRITICAL_ENCRYPT = Utils.EncryptionTool.EncryptStringToBytes_Aes(SALT_CRITICAL, derivation_key_with_salt, IV);

                // WE STORE IN THE DATABASE THE SALT_CRITICAL_ENCRYPT

                ManagerAccountDB.SetSaltCritical(SALT_CRITICAL_ENCRYPT);

                ManagerAccountDB.SetDatabase(database_name);

                ManagerAccountDB.Save();

                DATABASE_NAME = database_name;

            }
            
            
        }

        /// <summary>
        /// Loads the specified database with the provided master password. Return the salt_derived_key
        /// </summary>
        /// <param name="master_password">The master password for accessing the database.</param>
        /// <param name="database_name">The name of the database to load.</param>
        /// <exception cref="IncorrectMasterPasswordException">Thrown when the provided master password is incorrect.</exception>
        public static void LoadDatabaseWithMasterPassword(string master_password, string database_name)

        {
            if (master_password == null) { throw new Exception("Master Password should be set"); }
            
            var ManagerAccountDB = new ManagerAccount(new YpassDbContext(database_name));

            ManagerAccount? manager_account_object = ManagerAccountDB.GetManagerAccountByDatabaseName(database_name);

            if(manager_account_object == null)
            {
                throw new Exception("Database has not been found");
            }

            var PASSWORD = Encoding.UTF8.GetBytes(master_password);
            var salt_derived_key = manager_account_object.GetSalt();

            derivation_key_with_salt = Utils.HashTool.DeriveKey(PASSWORD, salt_derived_key);

            IV = manager_account_object.GetIV();

            SALT_CRITICAL_ENCRYPT = manager_account_object.GetSaltCritical();

            //Let's verify now if the master password is good
                
            DecryptSaltCritical();
            
            DATABASE_NAME = database_name;
        }


        public static void LoadDatabaseWithSaltDerivationKey(byte[] salt_derived_key, string database_name)
        {

            var ManagerAccountDB = new ManagerAccount(new YpassDbContext(database_name));

            ManagerAccount? manager_account_object = ManagerAccountDB.GetManagerAccountByDatabaseName(database_name);

            if (manager_account_object == null)
            {
                throw new Exception("Database has not been found");
            }

            derivation_key_with_salt = salt_derived_key;

            IV = manager_account_object.GetIV();

            SALT_CRITICAL_ENCRYPT = manager_account_object.GetSaltCritical();

            //Let's verify now if the master password is good

            DecryptSaltCritical();
        }

        /// <summary>
        /// Encrypts the provided password using a secure encryption algorithm. The LoadDatabaseWithMasterPassword() should be call before.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password.</returns>
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

        /// <summary>
        /// Decrypts the provided encrypted password. The LoadDatabaseWithMasterPassword() should be call before.
        /// </summary>
        /// <param name="encrypt_password">The encrypted password to decrypt.</param>
        /// <returns>The decrypted password.</returns>
        public static string DecryptPassword(string encrypt_password)
        {
            var salt_critical_decrypt = DecryptSaltCritical();
            var decryptPassword = Utils.EncryptionTool.DecryptStringFromBytes_Aes(Convert.FromBase64String(encrypt_password!), salt_critical_decrypt!, IV!);
            return decryptPassword;
        }


        private static byte[] DecryptSaltCritical()
        {
            try
            {
                string decrypt_pass = Utils.EncryptionTool.DecryptStringFromBytes_Aes(SALT_CRITICAL_ENCRYPT!, derivation_key_with_salt!, IV!);
                return Encoding.UTF8.GetBytes(decrypt_pass);
            }
            catch(CryptographicException ex)
            {
                throw new IncorrectMasterPasswordException("Master password is incorrect.");
            }
            
        }

        public static String GetDatabaseName()
        {
            if (AuthenticationService.IsLogin())
            {
                if(DATABASE_NAME != null)
                    return DATABASE_NAME;
                else{
                    return "[not found]";
                }
            }
            return "Not connected.";
            
        }

        public static void UnloadDatabase(){
            DATABASE_NAME = null;
            SALT_CRITICAL_ENCRYPT = null;
            derivation_key_with_salt = null;
            IV = null;
        }

        public class IncorrectMasterPasswordException : Exception
        {
            public IncorrectMasterPasswordException(string message) : base(message) { }
        }
    }
}

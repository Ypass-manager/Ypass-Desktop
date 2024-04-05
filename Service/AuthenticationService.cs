using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YpassDesktop.DataAccess;

namespace YpassDesktop.Service
{
    public static class AuthenticationService
    {
        private static bool _isLoggedIn = false;
        private static String? _databaseName;
        private static byte[]? _salt_derived_key;
        
        public static bool IsLoggedIn {
            get { return _isLoggedIn; }
            private set { _isLoggedIn = value; }
        }

        public static void Login(string databaseName, byte[] salt_derived_key)
        {
            // Perform authentication
            // If authentication is successful:
            IsLoggedIn = true;
            _databaseName = databaseName;
            _salt_derived_key = salt_derived_key;
        }

        public static void Logout()
        {
            // Perform logout
            IsLoggedIn = false;
            _databaseName = null;
        }

        public static String GetDbName()
        {
            if (_isLoggedIn)
            {
                if(_databaseName != null)
                    return _databaseName;
            }
            return "Not connected.";
            
        }

        public static byte[]? GetSaltDerivedKey()
        {
            if(_salt_derived_key != null) return _salt_derived_key;
            return null;
        }

    }
}

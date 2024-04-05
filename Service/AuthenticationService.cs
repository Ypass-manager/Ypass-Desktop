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
        
        public static bool IsLoggedIn {
            get { return _isLoggedIn; }
            private set { _isLoggedIn = value; }
        }

        public static void Login(String databaseName)
        {
            // Perform authentication
            // If authentication is successful:
            IsLoggedIn = true;
            _databaseName = databaseName;
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

    }
}

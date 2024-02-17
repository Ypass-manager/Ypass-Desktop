using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YpassDesktop.Service
{
    public static class AuthenticationService
    {
        private static bool _isLoggedIn = false;

        public static bool IsLoggedIn {
            get { return _isLoggedIn; }
            private set { _isLoggedIn = value; }
        }

        public static void Login()
        {
            // Perform authentication
            // If authentication is successful:
            IsLoggedIn = true;
        }

        public static void Logout()
        {
            // Perform logout
            IsLoggedIn = false;
        }
    }
}

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
        
        public static bool IsLoggedIn {
            get { return _isLoggedIn; }
            private set { _isLoggedIn = value; }
        }
        public static bool IsLogin(){
            return _isLoggedIn;
        }

        public static void Login()
        {
            // Perform authentication
            // If authentication is successful:
            IsLoggedIn = true;
            // Add to the database the connection
            string database_name = Service.EncryptionService.GetDatabaseName();
            HistoryConnection historyConnection = new HistoryConnection(new YpassDbContext(database_name));
            historyConnection.Save();
        }

        public static void Logout()
        {
            // Perform logout
            IsLoggedIn = false;
            Service.EncryptionService.UnloadDatabase();
        }

    }
}

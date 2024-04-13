
using System.Collections.Generic;
using System.Globalization;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels
{
    public class HistoryConnectionPageViewModel : BaseViewModel
    {

        public HistoryConnectionPageViewModel() {
            
        
            

        }

        public List<string> ConnectionDates{
            get {
                string database_name = AuthenticationService.GetDbName();
                HistoryConnection historyConnection = new HistoryConnection(new YpassDbContext(database_name));

                var formattedDates = new List<string>();
                foreach (var date in historyConnection.GetListDateConnection())
                {

                    formattedDates.Add("Connexion le " + date.ToString("dddd dd MMMM 'à' HH'h'mm", new CultureInfo("fr-FR")));
                }

                return formattedDates;
            }
        }
    }
}

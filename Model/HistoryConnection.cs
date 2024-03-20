using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;
using YpassDesktop.DataAccess;

public class HistoryConnection
{
    private YpassDbContext _dbContext;
    public HistoryConnection(YpassDbContext dbContext)
    {
        _dbContext = dbContext;
        Connections = new List<DateTime>();
    }
    private DateTime LastConnection { get; set; }
    public List<DateTime> Connections { get; set; }



    //Add Each connectionDate
    public void UpdateHistory()
    {
        LastConnection = DateTime.Now;
        Connections.Add(LastConnection);
        _dbContext.SaveChanges();
    }


    //Foreach Connection in Connections
    /*public void BrowseHistory()
    {
        foreach (DateTime Connection in Connections)
        {
            
        }
    }*/

    
}
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
    }
    private DateTime LastConnection { get; set; }

    //Save the last connection date
    public void SaveHistory()
    {
        LastConnection = DateTime.Now;
        _dbContext.HistoryConnection.Add(this);
        _dbContext.SaveChanges();
    }

    
}
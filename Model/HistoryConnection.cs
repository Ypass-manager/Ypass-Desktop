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
    public int HistoryConnectionId { get; set; } = 0;
    public DateTime ConnectionDate { get; set; } = DateTime.Now;

    //Save the last connection date
    public void Save()
    {
        ConnectionDate = DateTime.Now;
        _dbContext.HistoryConnection.Add(this);
        _dbContext.SaveChanges();
    }

    public List<DateTime> GetListDateConnection(){
        return _dbContext.HistoryConnection
                     .OrderByDescending(h => h.ConnectionDate)
                     .Select(h => h.ConnectionDate)
                     .ToList();
    }

    
}
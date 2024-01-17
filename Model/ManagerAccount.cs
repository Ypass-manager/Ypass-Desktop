using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ManagerAccount
{
    public int managerAccountId { get; set; }
    public string accountName { get; set; }
    public string saltCrypt { get; set; }
    public string hashPass { get; set; }
    public string databaseName { get; set; }

}
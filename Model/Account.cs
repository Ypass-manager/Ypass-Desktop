using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YpassDesktop.DataAccess;

public class Account
{
    private readonly YpassDbContext _dbContext;

    public Account (YpassDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int accountId { get; set; }
    public String username { get; set; }
    public String password { get; set; }
    public DateTime lastModification { get; set; }
    public bool isFavorite { get; set; }
    public String websiteUrl { get; set; }
    public String title { get; set; }

    public void Save()
    {
        _dbContext.Account.Add(this);
        _dbContext.SaveChanges();
    }
}

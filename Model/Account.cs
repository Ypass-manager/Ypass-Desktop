using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YpassDesktop.DataAccess;

public class Account
{
    private YpassDbContext _dbContext;
    
    public Account(YpassDbContext dbContext){
        _dbContext = dbContext;
    }
    public int AccountId { get; set; } = 0;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime LastModification { get; set; } = new DateTime();
    public bool IsFavorite { get; set; } = false;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public void SetTitle(string title){
        Title = title;
    }

    public void Save()
    {
        _dbContext.Account.Add(this);
        _dbContext.SaveChanges();
    }
}

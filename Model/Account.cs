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
    public DateTime LastModification { get; set; } = DateTime.Now;
    public bool IsFavorite { get; set; } = false;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public void Save()
    {
        var existingAccount = _dbContext.Account.FirstOrDefault(a => a.AccountId == this.AccountId);
        if(existingAccount == null){
            _dbContext.Account.Add(this);
        }
        _dbContext.SaveChanges();
    }

    public void SetPassword(string password){
        Password = password;
        LastModification = DateTime.Now;
    }

    public Account? GetAccountByTitle(string title)
    {
        return _dbContext.Account.FirstOrDefault(account => account.Title == title);
    }

    public List<Account> GetAllAccount()
    {
        return _dbContext.Account
                     .OrderByDescending(a => a.Title)
                     .ToList();
    }
}

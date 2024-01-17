using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YpassDesktop.DataAccess;

namespace YpassDesktop.Model;
public class ManagerAccount
{
    private readonly YpassDbContext _dbContext;
    public ManagerAccount(YpassDbContext dbContext)
    {
        _dbContext = dbContext;

    }
    public int ManagerAccountId { get; set; } = 0;
    public string AccountName { get; set; } = string.Empty;

    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public byte[] SaltCritical { get; set; } = Array.Empty<byte>();

    public byte[] IV { get; set; } = Array.Empty<byte>();
    public string HashPass { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;

    public void SetSalt(byte[] salt)
    {
        Salt = salt;
    }

    public byte[] GetSalt()
    {
        return Salt;
    }

    // Methods to set and get SaltCritical
    public void SetSaltCritical(byte[] saltCritical)
    {
        SaltCritical = saltCritical;
    }

    public byte[] GetSaltCritical()
    {
        return SaltCritical;
    }

    // Methods to set and get IV
    public void SetIV(byte[] iv)
    {
        IV = iv;
    }

    public byte[] GetIV()
    {
        return IV;
    }

    public void AddAccount(ManagerAccount account)
    {
        _dbContext.ManagerAccount.Add(account);
        _dbContext.SaveChanges();
    }


    public void Save()
        {
            _dbContext.ManagerAccount.Add(this);
            _dbContext.SaveChanges();
        }
    

}
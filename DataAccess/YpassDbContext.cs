using System;
using Microsoft.EntityFrameworkCore;

namespace YpassDesktop.DataAccess;


public class YpassDbContext : DbContext
{


    public DbSet<ManagerAccount> ManagerAccount { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<HistoryConnection> HistoryConnection { get; set; }
    public DbSet<Setting> Setting { get; set; }


    public string DbPath { get; }

    public YpassDbContext(string database_name)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, database_name);

        Database.Migrate();
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}



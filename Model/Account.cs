using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Account
{ 
    public int accountId { get; set; }
    public String username { get; set; }
    public String password { get; set; }
    public DateTime lastModification { get; set; }
    public bool isFavorite { get; set; }
    public String websiteUrl { get; set; }
    public String websiteName { get; set; }
}
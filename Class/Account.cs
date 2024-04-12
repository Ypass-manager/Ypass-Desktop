using System;
using System.Windows.Input;

namespace YpassDesktop.Class;

public class Account
{
    // For now, exists only to make HomePageView.axaml available for testing
    // Will be worked on later
    public Account() {


    }
    
public string Username { get; set; }
public string Password { get; set; }
public string Title { get; set;}

}


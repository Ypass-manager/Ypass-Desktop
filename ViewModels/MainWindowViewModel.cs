﻿using ReactiveUI;
using System;
using System.Linq;
using YpassDesktop.DataAccess;
using YpassDesktop.Service;


namespace YpassDesktop.ViewModels;
public class MainWindowViewModel : BaseViewModel
{
    private BaseViewModel _CurrentPage;
    public MainWindowViewModel()
    {
        var simplePageViewModel= new SimplePageViewModel();
        NavigationService.Initialize(simplePageViewModel);
        
        //Subscribe to the service to know when a page has been change, and set the page
        NavigationService.NavigationChanged += newPage => setCurrentPage(newPage);

        // First Page by default
        _CurrentPage = simplePageViewModel;


        // Load Database

        using var db = new YpassDbContext();
        Console.WriteLine($"Database path: {db.DbPath}.");
        db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        db.SaveChanges();

        // Read
        Console.WriteLine("Querying for a blog");
        var blog = db.Blogs
            .OrderBy(b => b.BlogId)
            .First();

        // Update
        Console.WriteLine("Updating the blog and adding a post");
        blog.Url = "https://devblogs.microsoft.com/dotnet";
        blog.Posts.Add(
            new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        db.SaveChanges();

        // Delete
        Console.WriteLine("Delete the blog");
        db.Remove(blog);
        db.SaveChanges();
    }

    public BaseViewModel CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }

    public bool setCurrentPage(BaseViewModel page)
    {
        if (_CurrentPage != page)
        {
            CurrentPage = page;
            return true;
        }
        return false;
    }
}
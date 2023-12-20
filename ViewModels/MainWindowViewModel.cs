using ReactiveUI;
using System;
using YpassDesktop.Service;


namespace YpassDesktop.ViewModels;
public class MainWindowViewModel : BaseViewModel
{
    private BaseViewModel _CurrentPage;
    public MainWindowViewModel()
    {
        var testEncryptPage= new EncryptTestPageViewModel();
        NavigationService.Initialize(testEncryptPage);
        
        //Subscribe to the service to know when a page has been change, and set the page
        NavigationService.NavigationChanged += newPage => setCurrentPage(newPage);

        // First Page by default
        _CurrentPage = testEncryptPage;
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
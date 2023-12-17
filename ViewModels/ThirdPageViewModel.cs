
using System;
using System.Windows.Input;
using ReactiveUI;

namespace YpassDesktop.ViewModels;
public class ThirdPageViewModel : BaseViewModel
{
    // The message to display
    public string _Message => "Done";
    public ThirdPageViewModel()
    {
        
       GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    public string Message { get { return _Message;} }


    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Service.NavigationService.GoBack();
        
    }
}
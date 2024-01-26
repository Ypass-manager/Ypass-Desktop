
using System;
using System.Reflection.Metadata;
using System.Windows.Input;
using ReactiveUI;
using YpassDesktop.Service;
using static YpassDesktop.ViewModels.SecondPageViewModel;

namespace YpassDesktop.ViewModels;
public class ThirdPageViewModel : BaseViewModel
{
    // The message to display
    private string? _Message;
    public ThirdPageViewModel()
    {
       
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    public override void Initialize()
    {
        // Receive some customParameters from the precedent Page
        Console.WriteLine($"NavigationParameter type: {NavigationParameter?.GetType().FullName}");

        if (NavigationParameter is ParameterBuilder param)
        {
            Message = $"I just received from the previous Page some parameters. Well, your email address is {param.Get<string>("email")} and your password is {param.Get<string>("password")}. Oops I didn't hash it o_O";
        }
        else
        {
            Message = "Failed to cast NavigationParameter to CustomParameter.";
        }

    }
    
    public string? Message
    {
        get { return _Message; }
        set
        {
            // We can use "RaiseAndSetIfChanged" to check if the value changed and automatically notify the UI
            this.RaiseAndSetIfChanged(ref _Message, value);
        }
    }


    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Service.NavigationService.GoBack();
        
    }
}
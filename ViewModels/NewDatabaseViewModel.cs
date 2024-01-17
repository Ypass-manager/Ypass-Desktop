using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using YpassDesktop.Service;

namespace YpassDesktop.ViewModels;

public class NewDatabaseViewModel : BaseViewModel
{
    public NewDatabaseViewModel()
    {
        // Listen to changes of DatabaseName and update CanNavigateNext accordingly
        this.WhenAnyValue(x => x.DatabaseName)
            .Subscribe(_ => UpdateCanNavigateNext());

        var canNavNext = this.WhenAnyValue(x => x.CanNavigateNext);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }

    private string? _databaseName;

    [Required]
    public string? DatabaseName
    {
        get { return _databaseName; }
        set { this.RaiseAndSetIfChanged(ref _databaseName, value); }
    }

    // WIP

    private bool _CanNavigateNext;

    public bool CanNavigateNext
    {
        get { return _CanNavigateNext; }
        protected set { this.RaiseAndSetIfChanged(ref _CanNavigateNext, value); }
    }

    // Update CanNavigateNext. Allow next page if E-Mail and Password are valid
    private void UpdateCanNavigateNext()
    {
        CanNavigateNext =
                !string.IsNullOrEmpty(_databaseName);
    }


    public ICommand NavigateNextCommand { get; }
    private void NavigateNext()
    {

        var parameterBuilder = new ParameterBuilder();
        parameterBuilder.Add("email", DatabaseName);

        Service.NavigationService.NavigateTo(new ThirdPageViewModel(), parameterBuilder);

    }

    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        Console.WriteLine("GO BACK TO HE PREVIOUS PAGE");
        Service.NavigationService.GoBack();

    }
}

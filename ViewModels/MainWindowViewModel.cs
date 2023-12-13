namespace YpassDesktop.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public SimpleViewModel SimpleViewModel { get; } = new SimpleViewModel();
}

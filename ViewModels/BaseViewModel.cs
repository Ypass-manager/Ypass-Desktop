using ReactiveUI;
using static YpassDesktop.ViewModels.InscriptionViewModel;
using static YpassDesktop.ViewModels.SecondPageViewModel;
namespace YpassDesktop.ViewModels;

public interface IInitializable
{
    void Initialize();
}
public class BaseViewModel : ReactiveObject, IInitializable
{
    public object? NavigationParameter { get; set; }

    public virtual void Initialize()
    {
    }
}


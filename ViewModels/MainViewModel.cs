using YpassDesktop.Views;
using System.Windows.Controls;

namespace YpassDesktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Page CurrentPage
        {
            get => GetProperty<Page>();
            set => SetProperty(value);
        }


        public MainViewModel()
        {
            this.CurrentPage = Services.NavigationService.GetPage<ExemplePage, ExempleViewModel>(this);
    
        }
        
    }
}
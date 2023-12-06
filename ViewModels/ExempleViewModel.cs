using YpassDesktop.Views;

namespace YpassDesktop.ViewModels
{
    public class ExempleViewModel : BaseViewModel
    {

        private MainViewModel mainViewModel;

        public ExempleViewModel(MainViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }
    }
}

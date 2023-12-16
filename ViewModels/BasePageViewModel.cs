using ReactiveUI;
using System.Reactive.Subjects;
using System;
using System.Reactive.Linq;

namespace YpassDesktop.ViewModels
{
    public abstract class BasePageViewModel : BaseViewModel
    {
        private readonly Subject<BaseViewModel> _changePageSubject = new Subject<BaseViewModel>();

        public IObservable<BaseViewModel> ChangePageObservable => _changePageSubject.AsObservable();

        // Method to change the page
        public void ChangePage(BaseViewModel newPage)
        {
            _changePageSubject.OnNext(newPage);
        }
    }
}

using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace YpassDesktop.ViewModels
{
    // This is our simple ViewModel. We need to implement the interface "INotifyPropertyChanged"
    // in order to notify the View if any of our properties changed.
    public class SimplePageViewModel : BasePageViewModel
    {
        public SimplePageViewModel(){

            // We can listen to any property changes with "WhenAnyValue" and do whatever we want in "Subscribe".
            this.WhenAnyValue(o => o.Name)
            .Subscribe(o => this.RaisePropertyChanged(nameof(Greeting)));
        }
        
        private string? _Name; // This is our backing field for Name
        public string? Name
        {
            get { return _Name; }
            set 
            { 
                // We can use "RaiseAndSetIfChanged" to check if the value changed and automatically notify the UI
                this.RaiseAndSetIfChanged(ref _Name, value); 
            }
        }

// Greeting will change based on a Name.
        public string Greeting
        {
            get { 
                
                if (string.IsNullOrEmpty(Name))
                {
                    // If no Name is provided, use a default Greeting
                    return "Type NEXT to change to the next page";
                }
                else if (Name.Equals("NEXT"))
                {
                    
                    ChangePage(new SecondPageViewModel());
                    return "";
                }
                else
                {
                    // else greet the User.
                    return $"Hello {Name}";
                }
            }
        }

    }
}
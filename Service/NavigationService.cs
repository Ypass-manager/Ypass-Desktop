using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YpassDesktop.ViewModels;

namespace YpassDesktop.Service
{
    internal abstract class NavigationService
    {
        /// <summary>
        /// Gets if the user can navigate to the next page
        /// </summary>
        public abstract bool CanNavigateNext { get; protected set; }

        /// <summary>
        /// Gets if the user can navigate to the previous page
        /// </summary>
        public abstract bool CanNavigatePrevious { get; protected set; }
    }
}

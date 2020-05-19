using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Ruteros.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruteros.Prism.ViewModels
{
    public class TripHistoryPageViewModel : ViewModelBase
    {
        public TripHistoryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.TripHistory;
        }
    }
}

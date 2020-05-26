using Prism.Navigation;
using Ruteros.Prism.Helpers;

namespace Ruteros.Prism.ViewModels
{
    public class TripDetailPageViewModel : ViewModelBase
    {
        public TripDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.TripDetail;
        }
    }
}


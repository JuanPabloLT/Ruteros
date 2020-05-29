using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Ruteros.Common.Helpers;
using Ruteros.Common.Models;
using Ruteros.Prism.Helpers;
using Ruteros.Prism.Views;

namespace Ruteros.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _startTripCommand;
        private bool _isAdmin;

        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.NewTrip;
            IsAdminAsync();
        }

        public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }


        private async void IsAdminAsync()
        {
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            if (user.UserType.ToString().Equals("Admin"))
            {
                IsAdmin = false;
            }
            else
            {
                IsAdmin = true;
            }
        }

        private async void StartTripAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(StartTripPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }
    }
}

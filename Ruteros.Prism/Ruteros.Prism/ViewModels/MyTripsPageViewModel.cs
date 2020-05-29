using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Ruteros.Common.Helpers;
using Ruteros.Common.Models;
using Ruteros.Common.Services;
using Ruteros.Prism.Helpers;

namespace Ruteros.Prism.ViewModels
{
    public class MyTripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isAdmin;
        private List<TripItemViewModel> _trips;
        private DelegateCommand _refreshCommand;

        public MyTripsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.MyTrips;
            StartDate = DateTime.Today.AddDays(-7);
            EndDate = DateTime.Today;
            IsRunning = false;
            LoadTripsAsync();
        }

        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(LoadTripsAsync));

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Document { get; set; }
        public string Shipping { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTripsAsync()
        {
            //IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            MyTripsRequest request = new MyTripsRequest
            {
                EndDate = EndDate.AddDays(1).ToUniversalTime(),
                StartDate = StartDate.ToUniversalTime(),
                UserId = user.Id,
                Document = Document,
                Shipping = Shipping
            };

            Response response = new Response();

            if (user.UserType.ToString().Equals("Admin")){
                response = await _apiService.GetMyTripsAdmin(url, "api", "/Trips/GetMyTripsAdmin", "bearer", token.Token, request);
                IsAdmin = true;
            }
            else
            {
                response = await _apiService.GetMyTrips(url, "api", "/Trips/GetMyTrips", "bearer", token.Token, request);
                IsAdmin = false;
            }
            

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<TripResponse> trips = (List<TripResponse>)response.Result;
            Trips = trips.Select(t => new TripItemViewModel(_navigationService)
            {
                EndDate = t.EndDate,
                Id = t.Id,
                Remarks = t.Remarks,
                Source = t.Source,
                SourceLatitude = t.SourceLatitude,
                SourceLongitude = t.SourceLongitude,
                StartDate = t.StartDate,
                Target = t.Target,
                TargetLatitude = t.TargetLatitude,
                TargetLongitude = t.TargetLongitude,
                TripDetails = t.TripDetails,
                User = t.User,
                Document = t.User.Document,
                Vehicle = t.Vehicle,
                Warehouse = t.Warehouse,
                Shipping = t.Shipping
            }).ToList();
        }
    }
}

using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ruteros.Common.Services;
using Ruteros.Prism.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Ruteros.Common.Models;
using Newtonsoft.Json;
using Ruteros.Common.Helpers;
using System;
using Ruteros.Prism.Views;

namespace Ruteros.Prism.ViewModels
{
    public class StartTripPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;
        private string _source;
        private string _buttonLabel;
        private bool _isSecondButtonVisible;
        private bool _isRunning;
        private bool _isEnabled;
        private Position _position;
        private TripResponse _tripResponse;
        private UserResponse _user;
        private TokenResponse _token;
        private string _url;
        //private Timer _timer;
        private Geocoder _geoCoder;
        private TripDetailsRequest _tripDetailsRequest;
        private DelegateCommand _getAddressCommand;
        private DelegateCommand _startTripCommand;

        public StartTripPageViewModel(INavigationService navigationService, IGeolocatorService geolocatorService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _geolocatorService = geolocatorService;
            _apiService = apiService;
            _tripDetailsRequest = new TripDetailsRequest { TripDetails = new List<TripDetailRequest>() };
            Title = Languages.StartTrip;
            ButtonLabel = Languages.StartTrip;
            IsEnabled = true;
            LoadSourceAsync();
        }

        public DelegateCommand GetAddressCommand => _getAddressCommand ?? (_getAddressCommand = new DelegateCommand(LoadSourceAsync));

        public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

        public string Plaque { get; set; }
        public int WarehouseId { get; set; }
        public string ShippingCode { get; set; }

        public bool IsSecondButtonVisible
        {
            get => _isSecondButtonVisible;
            set => SetProperty(ref _isSecondButtonVisible, value);
        }

        public string ButtonLabel
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Source
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }

        private async void LoadSourceAsync()
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
            {
                _position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
                Geocoder geoCoder = new Geocoder();
                IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(_position);
                List<string> addresses = new List<string>(sources);

                if (addresses.Count > 0)
                {
                    Source = addresses[0];
                }
            }
        }

        private async void StartTripAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            //IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                //IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            TripRequest tripRequest = new TripRequest
            {
                Address = Source,
                Latitude = _geolocatorService.Latitude,
                Longitude = _geolocatorService.Longitude,
                Plaque = Plaque,
                ShippingCode = ShippingCode,
                WarehouseId = WarehouseId,
                UserId = new Guid(user.Id)
            };

            Response response = await _apiService.NewTripAsync(url, "/api", "/Trips", tripRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            _tripResponse = (TripResponse)response.Result;
            IsSecondButtonVisible = true;
            ButtonLabel = Languages.EndTrip;
            StartTripPage.GetInstance().AddPin(_position, Source, Languages.StartTrip, PinType.Place);
            IsRunning = false;
            IsEnabled = true;



        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PlaqueError1,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(ShippingCode))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ShippingError1,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(WarehouseId.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.WarehouseErrror1,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PlaqueError1,
                    Languages.Accept);
                return false;
            }

            Regex regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PlaqueError2,
                    Languages.Accept);
                return false;
            }

            return true;
        }
    }
}

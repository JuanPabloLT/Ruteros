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
    public class ShippingsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isAdmin;
        private bool _isEnabled;
        private List<ShippingItemViewModel> _shippings;
        private DelegateCommand _refreshCommand;

        public ShippingsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Shipping;
            Shipping = "";
            IsRunning = false;
            IsEnabled = true;
            LoadShippingAsync();
        }

        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(LoadShippingAsync));

        public string Shipping { get; set; }

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

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        public List<ShippingItemViewModel> Shippings
        {
            get => _shippings;
            set => SetProperty(ref _shippings, value);
        }

        private async void LoadShippingAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            ShippingRequest request = new ShippingRequest
            {
                Shipping = Shipping
            };

            Response response = await _apiService.GetShippings(url, "api", "/Shippings/GetShippings", "bearer", token.Token, request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List <ShippingResponse> shippings = (List<ShippingResponse>)response.Result;
            Shippings = shippings.Select(s => new ShippingItemViewModel(_navigationService)
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();
        }
    }
}

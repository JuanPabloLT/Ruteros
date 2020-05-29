using Newtonsoft.Json;
using Prism.Navigation;
using Ruteros.Common.Helpers;
using Ruteros.Common.Models;
using Ruteros.Common.Services;
using Ruteros.Prism.Helpers;
using Ruteros.Prism.Views;
using System.Collections.Generic;
using System.Linq;

namespace Ruteros.Prism.ViewModels
{
    public class ShippingPageViewModel : ViewModelBase
    {
        private ShippingResponse _shipping;
        private List<ShippingDetailResponse> _shippings;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public ShippingPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            Title = Languages.CheckShipping;
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Shipping;
            LoadShippingAsync(Shipping.Id);
        }

        public ShippingResponse Shipping
        {
            get => _shipping;
            set => SetProperty(ref _shipping, value);
        }

        public List<ShippingDetailResponse> Shippings
        {
            get => _shippings;
            set => SetProperty(ref _shippings, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
           Shipping = parameters.GetValue<ShippingResponse>("shipping");
            LoadShippingAsync(Shipping.Id);
        }

        private async void LoadShippingAsync(int shipping)
        {

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            ShippingDetailRequest request = new ShippingDetailRequest
            {
                Shippings = shipping
            };

            Response response = await _apiService.GetShippingDetails(url, "api", "/Shippings/GetShippingDetails", "bearer", token.Token, request);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<ShippingDetailResponse> shippings = (List<ShippingDetailResponse>)response.Result;
            Shippings = shippings.Select(s => new ShippingDetailResponse()
            {
                Id = s.Id,
                Quantity = s.Quantity,
                Description = s.Description,
                PicturePath = s.PicturePath 
            }).ToList();
        }






    }
}
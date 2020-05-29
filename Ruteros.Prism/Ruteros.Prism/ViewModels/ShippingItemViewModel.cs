using Prism.Commands;
using Prism.Navigation;
using System;
using Ruteros.Common.Models;
using Ruteros.Prism.Views;

namespace Ruteros.Prism.ViewModels
{
    public class ShippingItemViewModel : ShippingResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectShipping2Command;

        public ShippingItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public DelegateCommand SelectShipping2Command => _selectShipping2Command ?? (_selectShipping2Command = new DelegateCommand(SelectShipping2Async));

        private async void SelectShipping2Async()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "shipping", this }
            };

            await _navigationService.NavigateAsync(nameof(ShippingPage), parameters);
        }
    }
}

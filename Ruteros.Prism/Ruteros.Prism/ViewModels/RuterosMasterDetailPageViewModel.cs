using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Ruteros.Common.Helpers;
using Ruteros.Common.Models;
using Ruteros.Common.Services;
using Ruteros.Prism.Helpers;
using Ruteros.Prism.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ruteros.Prism.ViewModels
{
    public class RuterosMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;
        private DelegateCommand _modifyUserCommand;
        private readonly IApiService _apiService;
        private static RuterosMasterDetailPageViewModel _instance;


        public RuterosMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _apiService = apiService;
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();            
        }

        public static RuterosMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            
            string url = App.Current.Resources["UrlAPI"].ToString();
            /*bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }*/

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            UserResponse userResponse = (UserResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }

        public DelegateCommand ModifyUserCommand => _modifyUserCommand ?? (_modifyUserCommand = new DelegateCommand(ModifyUserAsync));

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        private async void ModifyUserAsync()
        {
            await _navigationService.NavigateAsync($"/RuterosMasterDetailPage/NavigationPage/{nameof(ModifyUserPage)}");
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_departure_board",
                    PageName = "HomePage",
                    Title = Languages.NewTrip
                },
                new Menu
                {
                    Icon = "ic_history",
                    PageName = "TripHistoryPage",
                    Title = Languages.TripHistory
                },
                new Menu
                {
                    Icon = "ic_content_paste",
                    PageName = "ShippingPage",
                    Title = Languages.CheckShipping
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Settings.IsLogin ? Languages.Logout : Languages.Login
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }

    }
}

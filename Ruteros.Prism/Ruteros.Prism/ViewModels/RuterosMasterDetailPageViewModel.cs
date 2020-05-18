using Prism.Navigation;
using Ruteros.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ruteros.Prism.ViewModels
{
    public class RuterosMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public RuterosMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
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
                    Title = "New trip"
                },
                new Menu
                {
                    Icon = "ic_history",
                    PageName = "TripHistoryPage",
                    Title = "See trip history"
                },
                new Menu
                {
                    Icon = "ic_content_paste",
                    PageName = "ShippingPage",
                    Title = "Check shipping history"
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Log in"
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

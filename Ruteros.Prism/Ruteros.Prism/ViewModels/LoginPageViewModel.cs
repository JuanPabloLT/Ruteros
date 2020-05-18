using Prism.Navigation;

namespace Ruteros.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
        }

    }
}

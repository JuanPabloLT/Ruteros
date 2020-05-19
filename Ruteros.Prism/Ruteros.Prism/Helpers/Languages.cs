using Ruteros.Prism.Interfaces;
using Ruteros.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;


namespace Ruteros.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Logout => Resource.Logout;

        public static string LoginError => Resource.LoginError;

        public static string Error => Resource.Error;

        public static string Register => Resource.Register;

        public static string NewTrip => Resource.NewTrip;

        public static string Login => Resource.Login;

        public static string ModifyUser => Resource.ModifyUser;

        public static string CheckShipping => Resource.CheckShipping;

        public static string TripHistory => Resource.TripHistory;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Accept => Resource.Accept;




    }

}

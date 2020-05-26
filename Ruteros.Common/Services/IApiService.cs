using Ruteros.Common.Models;
using System.Threading.Tasks;

namespace Ruteros.Common.Services
{
    public interface IApiService
    {

        //USERS
        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);

        Task<bool> CheckConnectionAsync(string url);

        //TRIPS
        Task<Response> NewTripAsync(string urlBase, string servicePrefix, string controller, TripRequest model, string tokenType, string accessToken);
        
        Task<Response> AddTripDetailsAsync(string urlBase, string servicePrefix, string controller, TripDetailsRequest model, string tokenType, string accessToken);

    }
}

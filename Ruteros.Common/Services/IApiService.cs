using Ruteros.Common.Models;
using System.Threading.Tasks;

namespace Ruteros.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

    }
}

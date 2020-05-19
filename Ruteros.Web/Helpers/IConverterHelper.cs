using Ruteros.Web.Data.Entities;
using Ruteros.Common.Models;

namespace Ruteros.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity user);
    }
}

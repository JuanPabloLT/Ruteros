using Ruteros.Common.Models;

namespace Ruteros.Common.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}

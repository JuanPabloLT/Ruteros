using Ruteros.Common.Models;

namespace Ruteros.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}

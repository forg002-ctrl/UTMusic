using System.Web;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.BusinessLogic.Interfaces
{
    public interface ISession
    {
        Response UserLogin(ULoginData data);
        Response UserRegister(ULoginData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }
}
using System.Web;
using UTMUSIC.BusinessLogic.Core;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public Response UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }
        public Response UserRegister(ULoginData data)
        {
            return UserRegisterAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
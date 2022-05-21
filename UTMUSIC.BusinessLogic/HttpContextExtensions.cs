using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.BusinessLogic
{
    public static class HttpContextExtensions
    {
        public static UserMinimal GetMySessionObject(this HttpContext current)
        {
            return (UserMinimal)current?.Session["__SessionObject"];
        }

        public static void SetMySessionObject(this HttpContext current, UserMinimal profile)
        {
            current.Session.Add("__SessionObject", profile);
        }
    }
}

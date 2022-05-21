using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UTMUSIC.BusinessLogic;
using UTMUSIC.BusinessLogic.Core;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.BusinessLogic.Services;
using UTMUSIC.Domain.Entities.Enums;
using UTMUSIC.Domain.Enums;

namespace UTMUSIC.ActionFilters
{
    public class AdminModeAttribute : ActionFilterAttribute
    {
        private UserApi _userService;

        public AdminModeAttribute()
        {
            this._userService = new UserApi();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _userService.UserCookie(apiCookie.Value);
                if (profile == null || profile.Level != URole.Admin)
                {
                    filterContext.Result =
                        new RedirectToRouteResult(new RouteValueDictionary(new
                            { controller = "Error", action = "Access" }));
                }
                HttpContext.Current.SetMySessionObject(profile);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new
                        {controller = "Error", action = "Access"}));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTMUSIC.Controllers
{
    public class ErrorController : Controller
    {
        public string Index()
        {
            return "Unknown error happened";
        }

        public string Error404()
        {
            return "ERROR 404";
        }

        public string Access()
        {
            return "YOU HAVE NO ACCESS TO THIS PAGE";
        }
    }
}
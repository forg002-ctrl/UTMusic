using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTMUSIC.BusinessLogic.Interfaces;

namespace UTMUSIC.Web.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

    }
}
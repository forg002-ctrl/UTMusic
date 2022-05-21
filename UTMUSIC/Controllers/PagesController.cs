using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTMUSIC.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Playlist()
        {
            return View();
        }

        public ActionResult Artist()
        {
            return View();
        }
    }
}
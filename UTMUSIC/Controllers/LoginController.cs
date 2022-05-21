using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Entities.User;
using UTMUSIC.Web.Models;

namespace UTMUSIC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public LoginController(IMapper mapper)
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _mapper = mapper;
        }

        //POST login/signIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = _mapper.Map<UserLogin, ULoginData>(login);
                data.LoginIp = Request.UserHostAddress;
                data.LoginDateTime = DateTime.Now;

                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", userLogin.StatusMsg);
                return View();
            }

            return View();
        }

        //GET login/signIn
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        
        //POST login/signUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = _mapper.Map<UserLogin, ULoginData>(login);
                data.LoginIp = Request.UserHostAddress;
                data.LoginDateTime = DateTime.Now;

                var userLogin = _session.UserRegister(data);
                
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", userLogin.StatusMsg);
                return View();
                
            }

            return View();
        }

        //GET login/signUP
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            //Check if Cookie exists.
            if (Request.Cookies["X-KEY"] != null)
            {
                HttpCookie cookie = Request.Cookies["X-KEY"];

                cookie.Expires = DateTime.Now.AddDays(-100);

                Response.Cookies.Add(cookie);
            }
            else
            {
                TempData["Message"] = "Cookie not found.";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
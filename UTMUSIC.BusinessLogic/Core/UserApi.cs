using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UTMUSIC.Domain.DBModel;
using UTMUSIC.Domain.Entities.User;
using UTMUSIC.Helpers;
using UTMUSIC.Domain.Entities.Enums;

namespace UTMUSIC.BusinessLogic.Core
{
    public class UserApi
    {
        public Response UserLoginAction(ULoginData data)
        {
            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new SiteContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new Response { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new SiteContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new Response { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new SiteContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new Response { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new SiteContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new Response { Status = true };
            }
        }

        public Response UserRegisterAction(ULoginData data)
        {
            UDbTable foundUser;
            using (var db = new SiteContext())
            {
                foundUser = db.Users.FirstOrDefault(u => u.Email == data.Credential);
            }
            
            if (foundUser != null)
            {
                return new Response { Status = false, StatusMsg = "The Username with such username already exists" };
            }
            
            var pass = LoginHelper.HashGen(data.Password);
            var newUser = new UDbTable();
            newUser.Username = data.Credential;
            newUser.Password = pass;
            newUser.Level = URole.User;
            newUser.Email = data.Credential;
            newUser.LasIp = data.LoginIp.ToString();
            newUser.LastLogin = data.LoginDateTime;
            
            using (var db = new SiteContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            
            return new Response { Status = true };

        }

        public HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SiteContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SiteContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        public UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SiteContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;

            using (var db = new SiteContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;

            var userminimal = new UserMinimal();

            userminimal.Username = curentUser.Username;
            userminimal.Id = curentUser.Id;
            userminimal.Email = curentUser.Email;
            userminimal.Level = curentUser.Level;
            userminimal.LasIp = curentUser.LasIp;
            userminimal.LastLogin = curentUser.LastLogin;

            return userminimal;
        }
    }
}
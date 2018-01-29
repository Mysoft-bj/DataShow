using DataShow.Services.Core;
using DataShow.Services.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DataShow.Manage.Controllers.Login
{
    public class LoginController : Controller
    {
        

        public ActionResult Index()
        {
            return View("login");
        }

        public ActionResult DoLogin()
        {
            ViewBag.regErrorStatus = true;
            var usercode = Request.Form["username"];
            var password = Request.Form["password"];
            var passwordMd5 = GetMd5Str32(password);

            return Content("");
        }

     
        public ActionResult Logout()
        {
            UserInfo.Logout();
            return RedirectToAction("index", "home");
        }




        /// <summary>
        /// MD5 32位加密 加密后密码为小写
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetMd5Str32(string text)
        {
            try
            {
                using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
                {
                    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString().ToLower();
                }
            }
            catch { return null; }
        }

    }
}

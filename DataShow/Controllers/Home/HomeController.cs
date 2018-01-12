using My.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Application;
using My.Domain;
using My.Application.Login;

namespace FineSource.Controllers.home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var user= UserInfo.GetUserInfo();
            if (user!=null)
            {
                ViewBag.isLogin = true;
                ViewBag.user = user;
            }
            else {
                ViewBag.isLogin = false;  
            }
            return View();
        }

    }
}


using My.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using My.Application;
using My.Application.Login;
using System.Text;

namespace FineSource.Controllers.Login
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
           ViewBag.loginErrorStatus = true;
           ViewBag.regErrorStatus = true;
           return View("login");
        }

        public ActionResult DoLogin()
        {
            ViewBag.regErrorStatus = true;
            var usercode = Request.Form["username"];
            var password = Request.Form["password"];
            var passwordMd5 = GetMd5Str32(password);
            var sql = "SELECT id,UserName,UserCode,MobilePhone,IsAdmin from myuser where UserCode=@UserCode AND Password=@Password";
            DataTable dt= MysqlDataBaseManager.GetDataBase().ExecuteDataTable(
                sql,
                new MySqlParameter("UserCode", usercode),
                new MySqlParameter("Password", passwordMd5)
                );
            if (dt.Rows.Count == 1)
            {
                var user = new UserInfo
                {
                    Id = new Guid(dt.Rows[0]["id"].ToString()),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    UserCode = dt.Rows[0]["UserCode"].ToString(),
                    IsAdmin = dt.Rows[0]["UserCode"].ToString(),
                    MobilePhone = dt.Rows[0]["MobilePhone"].ToString()
                };
                UserInfo.SetUserInfo(user);
                ViewBag.loginErrorStatus = true;
                return RedirectToAction("index","home");
            }
            else {
                ViewBag.loginErrorStatus = false;
                ViewBag.username = usercode;
                return View("login");
            }
            
        }

        public ActionResult Register()
        {
            ViewBag.loginErrorStatus = true;
            ViewBag.regErrorStatus = true;
            ViewBag.tab = "reg";
            return View("login");
        }

        public ActionResult DoRegister()
        {
            ViewBag.loginErrorStatus = true;
            var usercode = Request.Form["username"];
            var mobile = Request.Form["mobile"];
            var email = Request.Form["email"];
            var password = Request.Form["password"];
            var passwordMd5 = GetMd5Str32(password);
            ViewBag.tab = "reg";
            if (!string.IsNullOrEmpty(usercode) && !string.IsNullOrEmpty(mobile) && !string.IsNullOrEmpty(password))
            {
                //校验用户名
                var sql = "SELECT count(*) from myuser where UserCode=@UserCode";
                if (MysqlDataBaseManager.GetDataBase().ExecuteScalar<Int16>(
                    sql,
                    new MySqlParameter("UserCode", usercode)
                    ) > 0) 
                {
                    ViewBag.regErrorStatus = false;
                    ViewBag.message = "用户名重复！";
                    return View("login");
                }

                //校验手机号
                sql = "SELECT count(*) from myuser where MobilePhone=@MobilePhone";
                if(MysqlDataBaseManager.GetDataBase().ExecuteScalar<Int16>(
                    sql,
                    new MySqlParameter("MobilePhone", mobile)
                    )>0)
                {
                    ViewBag.regErrorStatus = false;
                    ViewBag.message = "手机号已被使用！";
                    return View("login"); 
                }
                sql = "INSERT into myuser(id,username,usercode,password,mobilephone,email) VALUES(UUID(),@username,@usercode,@password,@mobilephone,@email)";
                MysqlDataBaseManager.GetDataBase().ExecuteNonQuery(
                    sql,
                    new MySqlParameter("username", usercode),
                    new MySqlParameter("usercode", usercode),
                    new MySqlParameter("password", passwordMd5),
                    new MySqlParameter("mobilephone", mobile),
                    new MySqlParameter("email", email)
                    );
                ViewBag.usercode = usercode;
                return View("RegisterSucess"); 
            }
            else {
                ViewBag.regErrorStatus = false;
                ViewBag.message = "相关必填字段未填写";
                return View("login");
            }
        }
        /// <summary>
        /// 校验用户名重复
        /// </summary>
        /// <returns></returns>
        public string CheckUserName() {
            var usercode = Request.QueryString["username"];
            if (!string.IsNullOrEmpty(usercode))
            {
                var sql = "SELECT count(*) from myuser where UserCode=@UserCode";
                int count = MysqlDataBaseManager.GetDataBase().ExecuteScalar<Int16> (
                    sql,
                    new MySqlParameter("UserCode", usercode)
                    );
                if (count > 0)
                {
                    return "false";
                }
                else {
                    return "true";
                }
            }
            else {
                return "false";
            }
           
        }
        /// <summary>
        /// 校验手机号重复
        /// </summary>
        /// <returns></returns>
        public string CheckMobile() {
            var mobile = Request.QueryString["mobile"]; ;
            if (!string.IsNullOrEmpty(mobile))
            {
                var sql = "SELECT count(*) from myuser where MobilePhone=@MobilePhone";
                int count = MysqlDataBaseManager.GetDataBase().ExecuteScalar<Int16>(
                    sql,
                    new MySqlParameter("MobilePhone", mobile)
                    );
                if (count > 0)
                {
                    return "false";
                }
                else
                {
                    return "true";
                }
            }
            else
            {
                return "false";
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataShow.Services.Core
{
    public class UserInfo
    {

        public UserInfo(){}
        private const string InfoKey = "UserInfo";

        public Guid UserGUID { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string MobilePhone { get; set; }
        public string IsAdmin { get; set; }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="info">用户信息</param>
        public static void SetUserInfo(UserInfo info)
        {
            var context = HttpContext.Current;
            context.Session[InfoKey] = info;
            if (context != null)
            {
                context.Session["UserName"] = info.UserName;
                context.Session["UserCode"] = info.UserCode;
                context.Session["UserGUID"] = info.UserGUID.ToString();
                context.Session["MobilePhone"] = info.MobilePhone.ToString();
                context.Session["IsAdmin"] = info.IsAdmin;
               
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        public static UserInfo GetUserInfo()
        {
            return GetUserInfo(HttpContext.Current);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(HttpContext context)
        {
            UserInfo userInfo = null;
            var s = context.Session;
            if (s[InfoKey] != null)
            {
                userInfo = (UserInfo)s[InfoKey];
            }
            else
            {
                
                    if (s["UserGUID"] != null && s["UserName"] != null && s["UserCode"] != null && s["MobilePhone"] != null && s["IsAdmin"] != null)
                    {
                        userInfo = new UserInfo()
                        {
                            UserName = s["UserName"].ToString(),
                            UserCode = s["UserCode"].ToString(),
                            UserGUID = new Guid(s["UserGUID"].ToString()),
                            MobilePhone = s["MobilePhone"].ToString(),
                            IsAdmin = s["IsAdmin"].ToString(),

                        };
                    }
            }

            return userInfo;
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        public static void Logout()
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Domain;
using My.Entity;
using My.Domain.Manage.Login;
using DataShow.Services.Core;

namespace DataShow.Services.Manage.Login
{
    public class LoginService
    {
        private DsUserDB db = null;
        public LoginService(){
            db = new DsUserDB();
         }

        public bool CheckLogin(string UserCode, string PassWord) {
            List<DsUser> ulist = db.GetCheckUserList(UserCode, PassWord);
            if (ulist.Count == 1)
            {
                var userInfo = ulist[0];
                var user = new UserInfo
                {
                    UserGUID = userInfo.UserGUID,
                    UserName = userInfo.UserName,
                    UserCode = userInfo.UserCode,
                    IsAdmin = userInfo.IsAdmin.ToString(),
                    MobilePhone = userInfo.MobilePhone
                };
                UserInfo.SetUserInfo(user);
                return true;
            }
            else {
                return false;
            }

        }

    }
}

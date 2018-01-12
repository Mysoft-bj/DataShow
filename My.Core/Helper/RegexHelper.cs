using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace My.Core
{
    public static class RegexHelper
    {
        public static string Phone = @"^1[34578]\d{9}$";
        public static string IdCard = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
        public static string EMail=@"^[a-zA-Z0-9_-]+\.?[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
        static Regex _phoneReg, _idCardReg, _eMailReg;
        static RegexHelper()
        {
            _phoneReg = new Regex(Phone);
            _idCardReg = new Regex(IdCard);
            _eMailReg = new Regex(EMail);
        }
        public static bool IsPhone(this string str)
        {
            return _phoneReg.IsMatch(str);
        }
        public static bool IsIDCard(this string str)
        {
            return _idCardReg.IsMatch(str);
        }
        public static bool IsEMail(this string str)
        {
            return _eMailReg.IsMatch(str);
        }
    }
}

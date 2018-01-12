using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
namespace My.Core
{
    public class Email : ITransientDependency
    {
        public Email(SiteInfo siteInfo)
        {
            MailHost = siteInfo.MailHost;
            MailUserName = siteInfo.MailUserName;
            MailPassword = siteInfo.MailPassword;
            SiteInfo = siteInfo;
        }
        public SiteInfo SiteInfo { get; set; }
        public string MailHost { get; set; }
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }

        public bool Send<T>(string to, string subject, string path, T t) {
            var temp = string.Empty;
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8)) {
                temp = reader.ReadToEnd();
            }
            var reg=new Regex(@"({{.*?}})");
            var siteMeta = MetaDataHelper.GetMetaData<SiteInfo>();
            var entityMeta = MetaDataHelper.GetMetaData<T>();
            var content = reg.Replace(temp, (m) =>
            {
                var prop = m.Value.Replace("{","").Replace("}","");
                if (siteMeta.Properties.ContainsKey(prop))
                {
                    return siteMeta.Properties[prop].GetValue(SiteInfo).ToString();
                }
                else if (entityMeta.Properties.ContainsKey(prop))
                {
                    return entityMeta.Properties[prop].GetValue(t).ToString();
                }
                return string.Empty;
            });
            return Send(to, subject, content);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">发送给</param>
        /// <param name="cc">抄送给</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <returns>true：发送成功，否则失败</returns>
        public  bool Send(string to, string cc, string subject, string content)
        {
            SmtpClient _smtpClient = new SmtpClient(MailHost);
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Credentials = new System.Net.NetworkCredential(MailUserName, MailPassword);//用户名和密码

            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(MailUserName);

            string[] arr = to.Split(';');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Trim() == "")
                    continue;

                _mailMessage.To.Add(new MailAddress(arr[i]));
            }


            if (cc != null && cc != "")
            {
                string[] ccArr = cc.Split(';');
                for (int i = 0; i < ccArr.Length; i++)
                {
                    if (ccArr[i].Trim() == "")
                        continue;

                    _mailMessage.CC.Add(ccArr[i]);
                }
            }

            _mailMessage.Subject = subject;//主题
            _mailMessage.Body = content;//内容

            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.Normal;//优先级

            try
            {
                _smtpClient.Send(_mailMessage);
                _mailMessage.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">发送给</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <returns>true：发送成功，否则失败</returns>
        public  bool Send(string to, string subject, string content)
        {
            return Send(to, null, subject, content);
        }
    }
  
}

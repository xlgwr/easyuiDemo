using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    [Serializable]
    [PetaPoco.TableName("m_SetEmail")]
    [PetaPoco.PrimaryKey("SetEmailID", autoIncrement = true)]
    public class SetEmailModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long SetEmailID { get; set; }

        /// <summary>
        /// 发件箱的邮件服务器地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 发送端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        /// </summary>
        public string MailUsername { get; set; }
        /// <summary>
        /// 发件人邮箱密码
        /// </summary>
        public string MailPassword { get; set; }
        /// <summary>
        /// 1表示对邮件内容进行socket层加密传输，0表示不加密
        /// </summary>
        public int EnableSsl { get; set; }


        public class VarKey
        {
            public const string tablename = "m_SetEmail";
            public const string SetEmailID = "SetEmailID";
            public const string Host = "Host";
            public const string Port = "Port";
            public const string MailUsername = "MailUsername";
            public const string MailPassword = "MailPassword";
            public const string EnableSsl = "EnableSsl";
        }

        /// <summary>
        /// 获取默认设置对应邮箱地址
        /// </summary>
        /// <returns></returns>
        public string getSysEmail()
        {
            try
            {
                string SysEmail = MailUsername;
                //if (!string.IsNullOrEmpty(Host))
                //{
                //    string[] arry = Host.Split('.');
                //    SysEmail += "@" + arry[1];
                //    SysEmail += "." + arry[2];
                //}
                return SysEmail;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}

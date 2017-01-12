using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Valeo.Lang;
using Valeo.Domain;
using Valeo.Common;
using System.Net.Mail;
using System.Net;
using log4net;

namespace Valeo.Service
{
    public class BaseService
    {

        public ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        protected Database db = new PetaPoco.Database("ConnMySQL");

        /// <summary>
        /// value 可以为0
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <param name="colname"></param>
        /// <param name="isLike"></param>
        public void genSqlWhereZero(ref Sql sql, int value, string colname, int isLike = 0)
        {
            if (value > -1)
            {
                genSqlWhere(ref sql, value.ToString(), colname, isLike);
            }

        }
        /// <summary>
        /// value 大于0
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <param name="colname"></param>
        /// <param name="isLike"></param>
        public void genSqlWhere(ref Sql sql, double value, string colname, int isLike = 0)
        {
            if (value > 0)
            {
                genSqlWhere(ref sql, value.ToString(), colname, isLike);
            }

        }
        /// <summary>
        /// 提出生成SQL对象 where
        /// 0:=
        /// 1:left like
        /// 2:all like
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <param name="colname"></param>
        /// <param name="isLike"></param>
        public void genSqlWhere(ref Sql sql, string value, string colname, int isLike = 0)
        {
            switch (isLike)
            {
                case 0:
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!(value.Equals("-1") || value.Equals("all") || value.Equals("全部")))
                        {
                            sql.Where(colname + " = @0 ", value);
                        }
                    }
                    break;
                case 1:
                    if (!string.IsNullOrEmpty(value))
                    {
                        sql.Where(colname + " like @0 ", value + "%");
                    }
                    break;
                case 2:
                    if (!string.IsNullOrEmpty(value))
                    {
                        sql.Where(colname + " like @0 ", "%" + value + "%");
                    }
                    break;
                default:
                    if (!string.IsNullOrEmpty(value))
                    {
                        sql.Where(colname + " = @0 ", value);
                    }
                    break;
            }
        }

        /// <summary>
        /// 使用状态下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetUsedStatusItem(bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_ENABLE, Value = VarKey.Comon.Enabled.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_DISABLE, Value = VarKey.Comon.Disabled.ToString() });

            return selectItems;
        }


        /// <summary>
        /// 消息类型下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetMessageTypeItem(bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_ENABLE, Value = VarKey.Comon.Enabled.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_DISABLE, Value = VarKey.Comon.Disabled.ToString() });

            return selectItems;
        }

        /// <summary>
        /// 产品名称下拉框-【管理中心】-【我的订单】-部品名称
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetPrdctNmItem(List<OrderListModel> list, bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }
            for (int i = 0; i < list.Count; i++)
            {
                selectItems.Add(new SelectListItem { Text = list[i].ProductName, Value = i.ToString() });

            }

            return selectItems;
        }


        /// <summary>
        /// 【是否启用】下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetIsUseItem(bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = itemAll, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_ENABLE, Value = VarKey.Comon.Enabled.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_DISABLE, Value = VarKey.Comon.Disabled.ToString() });

            return selectItems;
        }

        /// <summary>
        /// 【支付方式】下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetPaymentWayItem()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = VarKey.PaymentWay.ItemAll.ToString(), Selected = true });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_PAYMENTWAY_001, Value = VarKey.PaymentWay.paypal.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_PAYMENTWAY_002, Value = VarKey.PaymentWay.CHyl.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_PAYMENTWAY_003, Value = VarKey.PaymentWay.Zfb.ToString() });

            return selectItems;
        }
        /// <summary>
        /// 【支付状态】下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetPayStatusItem(bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.COM_PAYSTATUS_001, Value = VarKey.PayStatus.fk.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_PAYSTATUS_002, Value = VarKey.PayStatus.mfk.ToString() });

            return selectItems;
        }

        /// <summary>
        /// 【订单】状态下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetOrderStatusItem(bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_DISABLE, Value = VarKey.OrderStatus.wqy.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_ENABLE, Value = VarKey.OrderStatus.qy.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_CANCEL, Value = VarKey.OrderStatus.qx.ToString() });

            return selectItems;
        }

        /// <summary>
        /// 【查询】状态下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetSchStatusItem(bool isDisplayAll = true, string itemAll = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SCHSTATUS_000, Value = VarKey.SchStatus.wcl.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SCHSTATUS_001, Value = VarKey.SchStatus.clz.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SCHSTATUS_002, Value = VarKey.SchStatus.qx.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SCHSTATUS_003, Value = VarKey.SchStatus.wc.ToString() });

            return selectItems;
        }


        /// <summary>
        /// 【称呼】下拉框
        /// </summary>
        /// <param name="isDisplayAll">显示全部</param>
        /// <param name="itemAll">全部内容</param>
        /// <returns></returns>
        public List<SelectListItem> GetSalutationItem(bool isDisplayAll = true, string itemAll = "", string itemselected = "")
        {
            var selectItems = new List<SelectListItem>();

            if (isDisplayAll)
            {
                if (string.IsNullOrEmpty(itemAll)) itemAll = VarKey.Comon.ItemAll.ToString();
                selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = itemAll, Selected = true });
            }

            selectItems.Add(new SelectListItem { Text = BaseRes.MEB_CTL_044, Value = VarKey.Salutation.Mr.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEB_CTL_045, Value = VarKey.Salutation.Mrs.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEB_CTL_046, Value = VarKey.Salutation.Ms.ToString() });

            if (!string.IsNullOrEmpty(itemselected))
            {
                for (int i = 0; i < selectItems.Count; i++)
                {
                    selectItems[i].Selected = false;
                    if (selectItems[i].Text == itemselected)
                    {
                        selectItems[i].Selected = true;
                    }
                }

            }
            return selectItems;
        }



        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="FromMail">发件人</param>
        /// <param name="TomMail">收件人</param>
        /// <param name="Subject">主题</param>
        /// <param name="EmailBody">邮件内容</param>
        /// <param name="AttachPath">附件地址</param>
        /// <param name="server">用于 SMTP 事务的主机的名称或 IP 地址</param>
        public bool DoSendMail(string FromMail, string ToMailstring, string EmailBody, string Subject = "", string AttachPath = "", string server = "smtp.qq.com")
        {
            try
            {

                //*********发送操作对象************//
                SmtpClient smtp = new SmtpClient();
                SetEmailVM m_setEmail = GetEmai();
                //获取或设置用于 SMTP 事务的主机的名称或 IP 地址。
                server = m_setEmail.Host;
                smtp.Host = server;
                //邮箱和密码，【密码】
                //注：对于QQ,【密码】首先到qq邮箱的设置->账号->POP3/IMAP/SMTP/EXCHANGE服务，开启服务POP3/SMTP服务,会得到一个其他字符串，替代密码
                smtp.Credentials = new NetworkCredential(FromMail, m_setEmail.MailPassword);
                //端口
                smtp.Port = Convert.ToInt16(m_setEmail.Port);
                //指定 System.Net.Mail.SmtpClient 是否使用安全套接字层 (SSL) 加密连接。
                smtp.EnableSsl = true;


                //*********发送内容对象************//
                MailMessage myMail = new MailMessage();
                //发送邮箱，一个
                myMail.From = new MailAddress(FromMail);

                //接收邮箱，可添加多个
                myMail.To.Add(new MailAddress(ToMailstring));


                //附件，可添加多个
                if (!string.IsNullOrEmpty(AttachPath)) myMail.Attachments.Add(new Attachment(AttachPath));
                //myMail.Attachments.Add(new Attachment(@"C:\Users\Administrator\Desktop\操作手顺\手持机作业手顺.xls"));



                //发送主题
                myMail.Subject = Subject;
                myMail.SubjectEncoding = Encoding.UTF8;
                //发送内容
                //myMail.Body = "<div style='border:thin solid #00FFFF'> <p>Dear xixifusi,</p> <p>We wanted to take this opportunity to say thank you for your business and to wish you a wonderful holiday season and a very happy New Year.</p> <img alt='' src='http://i.microsoft.com/global/en-us/homepage/PublishingImages/Header/IELogo.png' /></div>";



                myMail.Body = EmailBody;
                myMail.BodyEncoding = Encoding.UTF8;
                //邮件内容是否支持html
                myMail.IsBodyHtml = true;




                //发送
                smtp.Send(myMail);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取默认邮箱设置
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="ToMail"></param>
        /// <param name="Subject"></param>
        /// <param name="ReceiveTime"></param>
        /// <returns></returns>
        public SetEmailVM GetEmai()
        {
            List<SetEmailVM> _list;
            try
            {

                Sql sql = new PetaPoco.Sql();
                sql.Append(string.Format("select * from {0} ", SetEmailVM.VarKey.tablename));
                sql.Append("where 1=1  ");


                //默认邮箱设置，有且仅有一条记录
                _list = db.Fetch<SetEmailVM>(sql);
                return _list[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


    }
}

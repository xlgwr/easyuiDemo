using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
   /// <summary>
    /// 邮件清单
   /// </summary>
  public  class EmailService : BaseService
    {

        public List<EmailListModel> GetEmailRecodList()
        {
            return db.Fetch<EmailListModel>("where 1=1");
        }


        /// <summary>
        /// 查询符合条件的离线查询信息
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="orgNo"></param>
        /// <param name="ApplyID"></param>
        /// <param name="MemberID"></param>
        /// <param name="ApplyType"></param>
        /// <param name="State"></param>
        /// <param name="Begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Page<EmailListModel> GetList(long page, long rows, string ToMail, string Subject, string SendDateStart, string SendDateEnd, string sort, string order)
        {
           
            try
            {
                Sql sql = new PetaPoco.Sql();
                sql.Append(string.Format("select * from {0} ", EmailListModel.VarKey.tablename));
                sql.Append("where 1=1  ");

                if (!string.IsNullOrEmpty(ToMail))
                {
                    string[] _arr = ToMail.Split('@');
                    if (_arr.Length==1)
                    {
                        sql.Append(string.Format(" and ToMail like '%{0}%' ", ToMail));
                    }
                    if (_arr.Length == 2)
                    {
                        sql.Append(string.Format(" and ( ToMail like '%{0}%' and  ToMail like '%{1}%')", _arr[0],_arr[1]));
                    }
                }
                if (!string.IsNullOrEmpty(Subject))
                {
                    sql.Append(string.Format(" and Subject like '%{0}%'", Subject));
                }

                DateTime dt = DateTime.Now;
                if (!string.IsNullOrEmpty(SendDateStart))
                {
                    dt = Convert.ToDateTime(SendDateStart);
                    sql.Append(" and ReceiveTime  >=@0", dt);

                }
                if (!string.IsNullOrEmpty(SendDateEnd))
                {
                    dt = Convert.ToDateTime(SendDateEnd);
                    sql.Append(" and ReceiveTime   <@0", dt.AddDays(1));
                }
                if (string.IsNullOrEmpty(sort))
                {
                    sql.Append(string.Format(" order by  ReceiveTime desc"));
                }
                else
                {
                    sql.Append(string.Format(" order by  {0} {1}",sort,order));
                }

                return db.Page<EmailListModel>(page, rows, sql);
               
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        /// <summary>
        /// 查询符合条件的离线查询信息
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="orgNo"></param>
        /// <param name="ApplyID"></param>
        /// <param name="MemberID"></param>
        /// <param name="ApplyType"></param>
        /// <param name="State"></param>
        /// <param name="Begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Page<MemberModel> GetMenberList(long page, long rows, string serch)
        {

            try
            {
                Sql sql = new PetaPoco.Sql();
                sql.Append(string.Format("select * from {0} ", MemberModel.VarKey.tablename));
                sql.Append("where 1=1  ");
                sql.Append("and  email<>'' ");

                if (!string.IsNullOrEmpty(serch))
                {
                    sql.Append("and ( ");
                    sql.Append(string.Format(" memberid like '%{0}%' ", serch));
                    sql.Append(string.Format(" or membername like '%{0}%' ", serch));
                    sql.Append(string.Format(" or email like '%{0}%' ", serch));
                    sql.Append(") ");
                }
               

                


                return db.Page<MemberModel>(page, rows, sql);

            }
            catch (Exception ex)
            {

                throw ex;
            }  
        }

       
        /// <summary>
        /// 查询符合条件的默认电邮信息
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="serch"></param>
        /// <returns></returns>
        public Page<MemberModel> GetDefaultMemberList(long memberID, long page, long rows, string serch)
        {

            try
            {
                Sql sql = new PetaPoco.Sql();
                sql.Append(@" SELECT ISNULL( m.Email, '') + ISNULL(p.Email, '') AS Email, m.MemberID,
                                m.MemberName,m.MemberGradeID,m.Password,m.RemainingSum,m.InvoiceDate,m.Type,m.MemberComanyID,m.Surname,m.GivenNames,
                                m.Salutation,m.FullName_Cn,m.FullName_Tm,m.IDNumber,m.BuildName,m.Street,m.StreetNumber,m.SeatNO,
                                m.Floor,m.RoomNO,m.HouseNO,m.Area,m.City,m.Province,m.CountryID,m.Address,m.PostalCode,m.AreaCodeHomeTel,
                                m.HomeTel,m.AreaCodeOfficeTel,m.OfficeTel,m.AreaCodeFax,m.Fax,m.AreaCodeMobilePhone,m.MobilePhone, m.Purpose1,
                                m.Purpose2,m.Purpose3,m.Purpose4,m.Purpose5,m.Purpose6,m.Purpose7,m.Purpose7Remark,m.Pathway1,m.Pathway2,m.Pathway3,
                                m.Pathway4,m.Pathway5,m.Pathway6,m.Pathway6Remark,m.PicPath1,m.PicPath2,m.PicPath3,m.PaymentWay,
                                m.AcceptAgreement,m.EmailVerification,m.LastSeachTime,m.Enable,m.adduser,m.addtime,m.upduser,m.updtime,m.SecretKey,
                                m.Remark,m.ErrorNum,m.CheckCode 
                            FROM m_Member AS m LEFT JOIN m_ContactPerson AS p ON m.MemberComanyID = p.MemberComanyID
                            WHERE  m.memberid=@0 AND ( p.[Default] = 1 OR m.MemberComanyID IS NULL ) 
                                    AND ISNULL( m.Email, '') + ISNULL(p.Email, '') <>'' ", memberID);
  
                if (!string.IsNullOrEmpty(serch))
                {
                    sql.Append(" and ( ");
                    sql.Append(string.Format(" m.memberid like '%{0}%' ", serch));
                    sql.Append(string.Format(" or m.membername like '%{0}%' ", serch));
                    sql.Append(string.Format(" or m.email like '%{0}%' ", serch));
                    sql.Append(string.Format(" or p.email like '%{0}%' ", serch));
                    sql.Append(") ");
                }
  
                return db.Page<MemberModel>(page, rows, sql);

            }
            catch (Exception ex)
            { 
                throw ex;
            }
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
                //端口 默认端口 587
                smtp.Port = m_setEmail.Port;
                //指定 System.Net.Mail.SmtpClient 是否使用安全套接字层 (SSL) 加密连接。
                if (m_setEmail.EnableSsl==1)
                {
                    smtp.EnableSsl = true;
                }
                else
                {
                    smtp.EnableSsl = false;
                }
               


                //*********发送内容对象************//
                MailMessage myMail = new MailMessage();
                //发送邮箱，一个
                myMail.From = new MailAddress(FromMail);

                //接收邮箱，可添加多个
                string[] mils = ToMailstring.Split(';');
                for (int i = 0; i < mils.Length; i++)
                {
                    if (mils[i].Length < 3) continue;

                    myMail.To.Add(new MailAddress(mils[i]));
                }
                //myMail.To.Add(new MailAddress(ToMailstring));
                //myMail.To.Add(new MailAddress(TomMailstring2));

                ////抄送邮箱，可添加多个
                //myMail.CC.Add(new MailAddress("517908774@qq.com"));
                //myMail.CC.Add(new MailAddress("lianhailong@szisec.com"));

                //附件，可添加多个
                //if (!string.IsNullOrEmpty(AttachPath)) myMail.Attachments.Add(new Attachment(AttachPath));

                if (AttachPath != null)
                {


                    string[] path = AttachPath.Split(';');
                    for (int i = 0; i < path.Length; i++)
                    {
                        if (path[i].Length < 3) continue;

                        myMail.Attachments.Add(new Attachment((path[i])));


                    }


                }
                //myMail.Attachments.Add(new Attachment(@"C:\Users\Administrator\Desktop\操作手顺\手持机作业手顺.xls"));



                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(EmailBody[0], null, "text/html");
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<div style='border:thin solid #00FFFF'> <p>Dear xixifusi,</p> <p>We wanted to take this opportunity to say thank you for your business and to wish you a wonderful holiday season and a very happy New Year.</p> <img alt='' src='cid:logo' /></div>", null, "text/html");



                //myMail.AlternateViews.Add(htmlView);





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
                smtp.Dispose();
                myMail.Dispose();
                return true;

            }
            catch (Exception)
            {
            
                return false;
            }
        }



      /// <summary>
        /// 新增邮件发送记录
        /// </summary>
        public List<EmailAppModel> DoInserRecod(string FromMailUser, string ToMail, object EmailBody, string Subject = "", string AttachPath = "")
        {
            try
            {
                List<EmailAppModel> emailAppModels = new List<EmailAppModel>();
                string[] arr = ToMail.Split(';');
                db.BeginTransaction();
                Dictionary<int, string> dic = new Dictionary<int, string>();

                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i].Length < 3) continue;

                    //相同账号只存入一条记录
                    if (dic.Values.Contains(arr[i])) continue;

                    EmailAppModel recd = new EmailAppModel
                         {
                             FromMail = FromMailUser,
                             ToMail = arr[i],
                             Subject = Subject,
                             EmailBody = EmailBody.ToString(),
                             AttachPath = AttachPath,
                             ReceiveTime = DateTime.Now.ToString()
                         };

                    db.Insert(EmailAppModel.VarKey.tablename, EmailAppModel.VarKey.EmailID, true, recd);

                    dic.Add(i, arr[i]);
                    emailAppModels.Add(recd);
                }
                db.CompleteTransaction();
                return emailAppModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
  

        #region Common

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

        #endregion

        //public Page<MemberModel> GetMenberList(long page, long rows, string serch)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

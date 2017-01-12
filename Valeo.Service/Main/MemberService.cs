using System;
using System.Collections.Generic;
using Valeo.Domain;
using PetaPoco;
using Valeo.Common;
using System.Linq;
using Valeo.Domain.Message;
using System.Web.Mvc;
using Valeo.Lang;
using Valeo.Domain.Member;
using Valeo.Domain.User;
using System.Net.Mail;
using System.Net;
using System.Text;
using Valeo.Domain.MemberGrade;
using Valeo.Domain.MemberGradeRight;
using App.Lang;

namespace Valeo.Service
{
    /// <summary>
    /// 会员信息服务类
    /// </summary>
    public class MemberService : BaseService
    {
      
        #region 【查询处理】
        
        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public UserModel GetUserBySearchKey(string secretKey)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT   *
                    FROM    m_User
                    WHERE SecretKey=@0 ", secretKey);

            UserModel userInfo = db.FirstOrDefault<UserModel>(sql);

            if (userInfo != null)
            {
                string searchValue = userInfo.SecretKey;

                userInfo.SecretKey = "";
                db.Update(userInfo);

                userInfo.SecretKey = searchValue;
            }

            return userInfo;
        }

        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserModel GetUser(string userName)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT   *
                    FROM    m_User
                    WHERE UserID=@0 ", userName);

            UserModel userInfo = db.FirstOrDefault<UserModel>(sql);

            return userInfo;
        }
        
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="userModel">忘记账号</param>  
        public bool SetSendForget(UserModel userModel, string url)
        {
            //附件地址
            string AttachPath = "";

            //收件人
            string ToMailAddr = "";
            //主题
            string Subject = BaseRes.LGN_MSG_006;
            //邮件内容
            string EmailBody = "";
            //公司信息
            SetEmailModel compEmail = new SetEmailModel();

            Sql sql;

            try
            {

                ToMailAddr = userModel.Email;


                //系统邮箱
                sql = new Sql().Append(@" 
                                SELECT *
                                FROM  m_SetEmail");
                compEmail = db.FirstOrDefault<SetEmailModel>(sql);

                //*********发送操作对象************//
                SmtpClient smtp = new SmtpClient();
                //获取或设置用于 SMTP 事务的主机的名称或 IP 地址。 
                smtp.Host = compEmail.Host;
                //邮箱和密码，【密码】
                //注：对于QQ,【密码】首先到qq邮箱的设置->账号->POP3/IMAP/SMTP/EXCHANGE服务，开启服务POP3/SMTP服务,会得到一个其他字符串，替代密码
                smtp.Credentials = new NetworkCredential(compEmail.getSysEmail(), compEmail.MailPassword);
                //端口  默认 587
                smtp.Port = compEmail.Port;
                //指定 System.Net.Mail.SmtpClient 是否使用安全套接字层 (SSL) 加密连接。
                if (compEmail.EnableSsl==1)
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
                myMail.From = new MailAddress(compEmail.getSysEmail());

                //接收邮箱，可添加多个
                myMail.To.Add(new MailAddress(ToMailAddr));

                //myMail.To.Add(new MailAddress(ToMailstring));
                //myMail.To.Add(new MailAddress(TomMailstring2));

                ////抄送邮箱，可添加多个
                //myMail.CC.Add(new MailAddress("517908774@qq.com"));
                //myMail.CC.Add(new MailAddress("lianhailong@szisec.com"));

                //附件，可添加多个
                //if (!string.IsNullOrEmpty(AttachPath)) myMail.Attachments.Add(new Attachment(AttachPath));

                //if (AttachPath != null)
                //{ 
                //    string[] path = AttachPath.Split(';');
                //    for (int i = 0; i < path.Length; i++)
                //    {
                //        if (path[i].Length < 3) continue;

                //        myMail.Attachments.Add(new Attachment((path[i]))); 
                //    } 
                //}
                //myMail.Attachments.Add(new Attachment(@"C:\Users\Administrator\Desktop\操作手顺\手持机作业手顺.xls"));  
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(EmailBody[0], null, "text/html");
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<div style='border:thin solid #00FFFF'> <p>Dear xixifusi,</p> <p>We wanted to take this opportunity to say thank you for your business and to wish you a wonderful holiday season and a very happy New Year.</p> <img alt='' src='cid:logo' /></div>", null, "text/html");

                //myMail.AlternateViews.Add(htmlView); 

                //发送主题
                myMail.Subject = Subject;
                myMail.SubjectEncoding = Encoding.UTF8;

                userModel.SecretKey = Encryption.Encode(string.Format("{0},{1},{2}"
                                            , DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                                            , userModel.UserID
                                            , DataConvert.GetGuid()));

                //发送内容
                EmailBody = string.Format(@"<div>
                                            <table style=""margin: 0 auto; padding: 0;max-width:612px;"" class=""container"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                <tbody>
                                                    <tr>
                                                        <td style=""padding:20px;"">
                                                            {3} {0}：
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:0 20px 20px;"">
                                                            {4}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:0;"">
                                                            <table style=""margin: 0; padding: 0;max-width:612px;border:1px solid #edf1f1;color:#5f5f5f;"" class=""container"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                                <tbody>
                                                                    <tr style=""background:#edf1f1;"">
                                                                        <td style=""text-align: center; vertical-align: top; font-size: 0;padding:15px 0;"">
                                                                            <!--[if (gte mso 9)|(IE)]><table width=""100%"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""width:50%;text-align:center;"" align=""center""><![endif]-->
                                                                            <div style=""width: 300px; display: inline-block; vertical-align: middle;"">
                                                                                <table align=""center"" width=""80%"">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style=""background:#1F4F82;padding:13px 0;"" align=""center"">
                                                                                                <a href=""{1}{2}"" style=""text-decoration:none;color:#fff;font-size:16px;font-weight:bold;display:block;text-align:center;"">{5}»</a>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div><!--[if (gte mso 9)|(IE)]></td><td style=""width:50%;""><![endif]-->
                                                                            <div style=""width: 300px; display: inline-block; vertical-align: middle;"" class=""gray-small-text"">
                                                                                <table width=""100%"">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style=""font-size:14px;vertical-align:middle;text-align:left;"" class=""gray-text-wrap"">
                                                                                                {6}
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]-->
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:20px 20px 0;"">
                                                           {7}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:16px 20px 25px;"">
                                                            {8}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>", userModel.UserName, url, DataConvert.UrlEncode(userModel.SecretKey)
                                               , BaseRes.LGN_MSG_007, BaseRes.LGN_MSG_008, BaseRes.LGN_MSG_009, BaseRes.LGN_MSG_010, BaseRes.LGN_MSG_011, BaseRes.LGN_MSG_012);


                myMail.Body = EmailBody;
                myMail.BodyEncoding = Encoding.UTF8;
                //邮件内容是否支持html
                myMail.IsBodyHtml = true;

                //发送
                smtp.Send(myMail);

                //邮件发送成功，在数据库插入发送记录信息
                EmailAppModel recd = new EmailAppModel
                {
                    FromMail = compEmail.getSysEmail(),
                    ToMail = ToMailAddr,
                    Subject = Subject,
                    EmailBody = EmailBody.ToString(),
                    AttachPath = AttachPath,
                    ReceiveTime = DateTime.Now.ToString()
                };

                db.Insert(EmailAppModel.VarKey.tablename, EmailAppModel.VarKey.EmailID, true, recd);

                //更新密钥
                db.Update(userModel);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserInfo GetLogin(string userName,string password)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT  *
                    FROM    m_User
                    WHERE UserID=@0 AND Password=@1 ", userName, Encryption.Encode(password));
             
            UserInfo userInfo = db.FirstOrDefault<UserInfo>(sql);
           
            if (userInfo != null)
            {
                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_UserAuthority
                    WHERE UserGradeID=@0  ", userInfo.UserGradeID);

                userInfo.ListUserAuthVM = db.Fetch<UserAuthVM>(sql);

                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_ShortName");
                List<ShortNameVM> lstShortNameVM = db.Fetch<ShortNameVM>(sql);
                //缩写信息（名称）
                userInfo.ListShortNameVM = lstShortNameVM.Where(s => s.Type == 0).ToList<ShortNameVM>();
                //缩写信息（名称）
                userInfo.ListShortAddrVM = lstShortNameVM.Where(s => s.Type == 1).ToList<ShortNameVM>();


//                sql = new Sql().Append(@" 
//                    SELECT *
//                    FROM  m_Word");
//                List<WordVM> lstWordVM = db.Fetch<WordVM>(sql);

//                //文字转换（1:简体-拼音）
//                userInfo.ListWord1VM = lstWordVM.Where(s => s.Type == 1).ToList<WordVM>();
//                //文字转换（2:简体-繁）
//                userInfo.ListWord2VM = lstWordVM.Where(s => s.Type == 2).ToList<WordVM>();
//                //文字转换（3:繁-拼音）
//                userInfo.ListWord3VM = lstWordVM.Where(s => s.Type == 3).ToList<WordVM>();
//                //文字转换（3:汉字-拼音）
//                userInfo.ListWord4VM = lstWordVM.Where(s => s.Type == 1 || s.Type == 3).ToList<WordVM>();

                //系统邮箱基本信息获取
                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_SetEmail");
                List<SetEmailVM> SetEmailVM = db.Fetch<SetEmailVM>(sql);
                userInfo.SetEmailVM = SetEmailVM[0];

            }

            return userInfo;
        }


        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public void GetWord(UserInfo userInfo)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_Word ");
            List<WordVM> lstWordVM = db.Fetch<WordVM>(sql);

            //文字转换（1:简体-拼音）
            userInfo.ListWord1VM = lstWordVM.Where(s => s.Type == 1).ToList<WordVM>();
            //文字转换（2:简体-繁）
            userInfo.ListWord2VM = lstWordVM.Where(s => s.Type == 2).ToList<WordVM>();
            //文字转换（3:繁-拼音）
            userInfo.ListWord3VM = lstWordVM.Where(s => s.Type == 3).ToList<WordVM>();
            //文字转换（3:汉字-拼音）
            userInfo.ListWord4VM = lstWordVM.Where(s => s.Type == 1 || s.Type == 3).ToList<WordVM>();
        }

        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo GetLogin(string userName)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT  *
                    FROM    m_User
                    WHERE UserID=@0 ", userName);

            UserInfo userInfo = db.FirstOrDefault<UserInfo>(sql);

            if (userInfo != null)
            {
                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_UserAuthority
                    WHERE UserGradeID=@0  ", userInfo.UserGradeID);

                userInfo.ListUserAuthVM = db.Fetch<UserAuthVM>(sql);

                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_ShortName");
                List<ShortNameVM> lstShortNameVM = db.Fetch<ShortNameVM>(sql);
                //缩写信息（名称）
                userInfo.ListShortNameVM = lstShortNameVM.Where(s => s.Type == 0).ToList<ShortNameVM>();
                //缩写信息（名称）
                userInfo.ListShortAddrVM = lstShortNameVM.Where(s => s.Type == 1).ToList<ShortNameVM>();


                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_Word");
                List<WordVM> lstWordVM = db.Fetch<WordVM>(sql);

                //文字转换（1:简体-拼音）
                userInfo.ListWord1VM = lstWordVM.Where(s => s.Type == 1).ToList<WordVM>();
                //文字转换（2:简体-繁）
                userInfo.ListWord2VM = lstWordVM.Where(s => s.Type == 2).ToList<WordVM>();
                //文字转换（3:繁-拼音）
                userInfo.ListWord3VM = lstWordVM.Where(s => s.Type == 3).ToList<WordVM>();
                //文字转换（3:汉字-拼音）
                userInfo.ListWord4VM = lstWordVM.Where(s => s.Type == 1 || s.Type == 3).ToList<WordVM>();


                //系统邮箱基本信息获取
                sql = new Sql().Append(@" 
                    SELECT *
                    FROM  m_SetEmail");
                List<SetEmailVM> SetEmailVM = db.Fetch<SetEmailVM>(sql);
                userInfo.SetEmailVM = SetEmailVM[0];

            }

            return userInfo;
        }
        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<MessageVM> GetPageMessageList(GridPager pager, string fromdate = ""
                                                    , string todate = "", string msgType = "")
        {
             

            Sql sql = new Sql().Append(@" 
                    SELECT   REC.MemberID, REC.MessageID, REC.IsRead, MSG.UserID, MSG.MessageTitle, MSG.Message, MSG.SendTime
                    FROM    dbo.t_Recipient AS REC LEFT OUTER JOIN
                            dbo.t_Message AS MSG ON REC.MessageID = MSG.MessageID ");


            if (!string.IsNullOrEmpty(fromdate))
            {
                sql.Where(" MSG.SendTime >= @0  ", fromdate);
            }

            if (!string.IsNullOrEmpty(todate))
            {
                sql.Where(" MSG.SendTime <= @0  ", todate);
            }

            if (!string.IsNullOrEmpty(msgType) && !msgType.Equals(VarKey.Comon.ItemAll))
            {
                //sql.Where(" MSG.SendTime <= @0  ", todate);
            }
  
            sql.Append(" ORDER BY MSG.SendTime DESC ");

            var list = db.Page<MessageVM>(pager.page, pager.pageSize, sql); 
            return list;
        }

        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<MessageVM> GetPageMessageList(int startIndex, int pageSize, string condition = "")
        {


            Sql sql = new Sql().Append(@" 
                    SELECT   REC.MemberID, REC.MessageID, REC.IsRead, MSG.UserID, MSG.MessageTitle, MSG.Message, MSG.SendTime
                    FROM    dbo.t_Recipient AS REC LEFT OUTER JOIN
                            dbo.t_Message AS MSG ON REC.MessageID = MSG.MessageID " );
 
            sql.Append(" ORDER BY MSG.SendTime DESC ");

            var list = db.Page<MessageVM>(startIndex, pageSize, sql);
             
            return list;
        }

        /// <summary>
        /// 获取界面下拉框
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> getPageSelectList()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = VarKey.Page.All.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LGN_CTL_008, Value = VarKey.Page.Login.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_019, Value = VarKey.Page.Register.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_001, Value = VarKey.Page.Buy.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_002, Value = VarKey.Page.OnlineCourt.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_003, Value = VarKey.Page.OnlineLand.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_004, Value = VarKey.Page.OnlineCompany.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_005, Value = VarKey.Page.OnlineCredit.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_006, Value = VarKey.Page.OnlineCommont.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_007, Value = VarKey.Page.AutoMinitor.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_008, Value = VarKey.Page.OfflineCourt.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_009, Value = VarKey.Page.OfflineLand.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_010, Value = VarKey.Page.OfflineCompany.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_011, Value = VarKey.Page.OfflineOther.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_012, Value = VarKey.Page.MainInfo.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_013, Value = VarKey.Page.ManagerPWD.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_014, Value = VarKey.Page.MyOrder.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_015, Value = VarKey.Page.ProductDetails.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_016, Value = VarKey.Page.SearchRecord.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_017, Value = VarKey.Page.MessageManager.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_018, Value = VarKey.Page.Exit.ToString() });
            return selectItems;
        }

        /// <summary>
        /// 按条件查询会员操作日志
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="memberLog"></param>
        /// <returns></returns>
        public Page<MemberLogVM> GetMemberLogList(long page, long rows, MemberLogVM memberLog, string sort, string order)
        {
            var sql = new Sql();
            sql.Append(@"SELECT
                        t_LogRecord.LogID,
                        m_Member.MemberID,
                        m_Member.MemberName,
                        m_Member.FullName_Cn,
                        m_Member.FullName_Tm,
                        m_Member.Surname,
                        m_Member.GivenNames,
                        t_LogRecord.Content,
                        t_LogRecord.Form,
                        t_LogRecord.AddDateTime,
                        t_LogRecord.Type,
						m_MemberComany.FullName_Cn as CFullName_Cn,
						m_MemberComany.FullName_Tm as CFullName_Tm,
						m_MemberComany.FullName_En as CFullName_En
                        FROM
                        m_Member
                        INNER JOIN t_LogRecord ON t_LogRecord.MemberID = m_Member.MemberID
						left JOIN m_MemberComany on m_Member.memberComanyid=m_MemberComany.memberComanyid");
            sql.Where("t_LogRecord.Type=1");
            if (!string.IsNullOrEmpty(memberLog.MemberName))
            {
                sql.Where("m_Member.MemberName like @0",  memberLog.MemberName + "%");
            }
            
            if (!string.IsNullOrEmpty(memberLog.Form)&&!memberLog.Form.Equals("0"))
            {
                sql.Where("t_LogRecord.Form=@0", memberLog.Form);
            }
            if (!string.IsNullOrEmpty(memberLog.Content))
            {
                sql.Where("t_LogRecord.Content like @0", memberLog.Content+"%");
            }
           
            if (!string.IsNullOrEmpty(memberLog.AddDateTimeS))
            {
                sql.Where("t_LogRecord.AddDateTime> @0  ", memberLog.AddDateTimeS + " 00:00:00:000");
            }

            if (!string.IsNullOrEmpty(memberLog.AddDateTimeE))
            {
                sql.Where(" t_LogRecord.AddDateTime< @0  ", memberLog.AddDateTimeE + " 23:59:59:999");
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.Equals("FullName_En"))
                {
                    sort = "Surname";  
                }
                sql.OrderBy(sort + " " + order);
            }
            else
            {
                sql.OrderBy(" t_LogRecord.LogID DESC");
            }
            
            return db.Page<MemberLogVM>(page, rows, sql);
        }

        /// <summary>
        /// 获取用户级别
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Page<MemberGradeModel> GetMemberGradelList(long page, long rows, string sort, string order)
        {
            var sql = new Sql();
            sql.Append(@"SELECT MemberGradeID,MemberGrade,Remark,adduser,upduser,CONVERT(varchar(20), updtime, 20) AS updtime,SetDefault,CONVERT(varchar(20), addtime, 20) AS addtime FROM m_MemberGrade");
            if (!string.IsNullOrEmpty(sort))
            {
                sql.OrderBy(sort + " " + order);
            }
            else
            {
                sql.OrderBy(" Addtime DESC");
            }
            
            return db.Page<MemberGradeModel>(page, rows, sql);
        }

        /// <summary>
        /// 获取权限参数
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetRightModelList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            Sql sql = new Sql().Append(@"SELECT * from m_RightKey WHERE Type=1 ORDER BY DspNo ASC");
            List<RightKeyModel> list = db.Fetch<RightKeyModel>(sql);
            foreach (RightKeyModel item in list)
            {
                selectItems.Add(new SelectListItem { Text = COMKey.COM(item.LanguageCode), Value = item.RightKey.ToString() });
            }
            return selectItems;
        }

        /// <summary>
        /// 获取级别权限KEY
        /// </summary>
        /// <param name="memberGradeID"></param>
        /// <returns></returns>
        public List<RightKeyVM> GetRightKeyList(string memberGradeID)
        {
             List<RightKeyVM> list=new List<RightKeyVM>();
            Sql sql = new Sql().Append(@"SELECT
							m_MemberGrade.MemberGradeID,
                            m_MemberGrade.MemberGrade,
                            m_MemberGradeRight.RightKey,
                            m_MemberGradeRight.LanguageCode,
                            m_MemberGrade.Remark,m_MemberGrade.addtime,m_MemberGrade.adduser,m_MemberGradeRight.MemberGradeRightID,
                            m_MemberGradeRight.RightValue,m_MemberGradeRight.DspNo
                            FROM
                            m_MemberGrade
                            INNER JOIN m_MemberGradeRight ON m_MemberGradeRight.MemberGradeID = m_MemberGrade.MemberGradeID
                            where m_MemberGrade.MemberGradeID=@0 and m_MemberGradeRight.RightValue=1", memberGradeID);
            list = db.Fetch<RightKeyVM>(sql);
            return list;
        }

        /// <summary>
        /// 获取会员权限
        /// </summary>
        /// <param name="memberGradeID"></param>
        /// <returns></returns>
        public List<RightKeyVM> GetMemberGradeList(string memberGradeID)
        {
            List<RightKeyVM> list = new List<RightKeyVM>();
            Sql sql = new Sql().Append(@"SELECT
                            m_MemberGrade.MemberGradeID,
                            m_MemberGrade.MemberGrade,
                            m_MemberGrade.Remark,m_MemberGrade.addtime,m_MemberGrade.adduser
                            FROM
                            m_MemberGrade where MemberGradeID=@0", memberGradeID);
            list = db.Fetch<RightKeyVM>(sql);
            return list;
        }

        #endregion

        #region 【新增处理】

        /// <summary>
        /// 新增消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Int16 AddMessage(MessageModel model)
        {

            Int16 rtnValue = -1;

            using (var scope = db.GetTransaction())
            {
                try
                {
                    //查看该消息是否存在
                    var result = db.Fetch<MessageModel>(string.Format(@"
                        SELECT * FROM t_Message WHERE MessageTitle='{0}'", model.MessageTitle));

                    if (result.Count > 0)
                    {
                        rtnValue = 1;
                    }
                    else
                    {
                        db.Insert("t_Message", "MessageID", false, model); 
                        scope.Complete();

                        rtnValue = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rtnValue;
        }

        /// <summary>
        /// 新增用户级别
        /// </summary>
        /// <param name="MGM"></param>
        /// <returns></returns>
        public long AddMemberGrade(MemberGradeModel MGM)
        {
            long rtnValue = -1;
            using (var scope = db.GetTransaction())
            {
                try
                {
                     //查看该消息是否存在
                    var result = db.Fetch<MemberGradeModel>(string.Format(@"
                        SELECT * from m_MemberGrade where MemberGrade='{0}'", MGM.MemberGrade));
                    if (result.Count > 0)
                    {
                        rtnValue = -1;
                    }
                    else
                    {

                        rtnValue = DataConvert.Value2long(db.Insert("m_MemberGrade", "MemberGradeID", MGM));
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 新增用户级别权限表
        /// </summary>
        /// <param name="MGR"></param>
        /// <returns></returns>
        public Int16 AddMemberGradeRiht(MemberGradeRightModel MGR)
        {
            Int16 rtnValue = -1;
            using (var scope = db.GetTransaction())
            {
                try
                {
                    MGR.RightValue = 1;
                    db.Insert("m_MemberGradeRight", "MemberGradeRightID", MGR);
                    scope.Complete();

                    rtnValue = 0;
               
                }
                catch (Exception ex) 
                {
                    rtnValue = 1;
                    throw ex;
                }
            }
            return rtnValue;
        }
        #endregion

        #region 【修改处理】

        /// <summary>
        /// 修改消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Int16 ModifyMessage(MessageModel model)
        {
            Int16 rtnValue = -1;

            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Update("t_Message", "MessageID", model, model.MessageID);
                    scope.Complete();

                    rtnValue = 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rtnValue;
        }

        /// <summary>
        /// 修改会员级别
        /// </summary>
        /// <param name="MGM"></param>
        /// <returns></returns>
        public Int16 EditMemberGrade(MemberGradeModel MGM)
        {
            Int16 rtnValue = -1;
            using (var scope = db.GetTransaction())
            {
                try
                {
                    //查看该消息是否存在
                    var result = db.Fetch<MemberGradeModel>(string.Format(@"
                        SELECT * FROM m_MemberGrade WHERE MemberGrade='{0}'", MGM.MemberGrade));
                    if (result.Count > 0)
                    {
                        if (result[0].MemberGradeID.Equals(MGM.MemberGradeID))
                        {
                            db.Update("m_MemberGrade", "MemberGradeID", MGM, MGM.MemberGradeID);
                            scope.Complete();
                            rtnValue = 1;
                        }
                        else
                        {
                            rtnValue = 0;
                        }
                        
                    }
                    else
                    {
                        db.Update("m_MemberGrade", "MemberGradeID", MGM, MGM.MemberGradeID);
                        scope.Complete();
                        rtnValue = 1;
                    }
                    
                }
                catch (Exception)
                {
                    rtnValue = -1;
                    throw;
                }
            }

            return rtnValue;
        }

        /// <summary>
        /// 修改会员级别权限
        /// </summary>
        /// <param name="MGRM"></param>
        /// <returns></returns>
        public Int16 EditMemberGradeRight(List<MemberGradeRightModel> MGRM,long MemberGradeId)
        {
            
            Int16 rtnValue = -1;
            List<MemberGradeRightModel> CommonList = new List<MemberGradeRightModel>();
            using (var scope = db.GetTransaction())
            {
                try
                {
                    var result = db.Fetch<MemberGradeRightModel>(string.Format(@"
                        SELECT * from m_MemberGradeRight where MemberGradeID={0}", MemberGradeId));
                    if (result.Count > 0)
                    {
                        //修改
                        for (int i = 0; i < result.Count; i++)
                        {
                            for (int k = 0; k < MGRM.Count; k++)
                            {

                                if (MGRM[k].RightKey.Equals(result[i].RightKey))
                                {
                                    MemberGradeRightModel MGM = new MemberGradeRightModel();
                                    MGM.MemberGradeID = MemberGradeId;
                                    MGM.MemberGradeRightID = result[i].MemberGradeRightID;
                                    MGM.Remark = MGRM[k].Remark;
                                    MGM.RightKey = MGRM[k].RightKey;
                                    MGM.RightValue = MGRM[k].RightValue;
                                    CommonList.Add(MGM);
                                    db.Update("m_MemberGradeRight", "MemberGradeRightID", MGM, MGM.MemberGradeRightID);
                                    rtnValue = 1;
                                }
                            }
                        }
                        
                        if (MGRM.Count==CommonList.Count)
	                    {
                            MGRM = new List<MemberGradeRightModel>();
                        }
                        else
                        {
                            for (int i = 0; i < MGRM.Count; i++)
                            {
                                for (int k = 0; k < CommonList.Count; k++)
                                {
                                    if (MGRM[i].RightKey.Equals(CommonList[k].RightKey))
                                    {
                                        MGRM.Remove(MGRM[i]);
                                    }

                                }
                            }
                        }
                        

                        

                        for (int i = 0; i < MGRM.Count; i++)
                        {
                            MemberGradeRightModel MGM = new MemberGradeRightModel();
                            var result1 = db.Fetch<MemberGradeRightVM>(string.Format(@"
                            SELECT * from m_RightKey WHERE RightKey='{0}'", MGRM[i].RightKey));
                            //查看多语言KEY                 
                            MGM.RightValue = 1;
                            MGM.RightKey = MGRM[i].RightKey;
                            MGM.Remark = MGRM[i].Remark;
                            MGM.MemberGradeID = MGRM[i].MemberGradeID;
                            db.Insert("m_MemberGradeRight", "MemberGradeRightID", MGM);
                            rtnValue = 1;
                        }

                        List<MemberGradeRightModel> CommonList1 = new List<MemberGradeRightModel>();
                        for (int k = 0; k < CommonList.Count; k++)
                        {
                            for (int i = 0; i < result.Count; i++)
                            {
                                if (result[i].RightKey.Equals(CommonList[k].RightKey))
                                {
                                    result.Remove(result[i]);
                                }
                            }
                        }
                        

                        for (int i = 0; i < result.Count; i++)
                        {
                            MemberGradeRightModel MGM = new MemberGradeRightModel();
                            MGM.MemberGradeID = MemberGradeId;
                            MGM.MemberGradeRightID = result[i].MemberGradeRightID;
                            MGM.Remark = result[i].Remark;
                            MGM.RightKey = result[i].RightKey;
                            MGM.RightValue = 0;                
                            db.Update("m_MemberGradeRight", "MemberGradeRightID", MGM, MGM.MemberGradeRightID);
                            rtnValue = 1;
                        }


                    }
                    else
                    {
                        //新增
                        for (int i = 0; i < MGRM.Count; i++)
                        {
                            MemberGradeRightModel MGM = new MemberGradeRightModel();
                            var result1 = db.Fetch<MemberGradeRightVM>(string.Format(@"
                        SELECT * from m_RightKey WHERE RightKey='{0}'", MGRM[i].RightKey));
                            //查看多语言KEY
                            MGM.RightValue = 1;
                            MGM.RightKey = MGRM[i].RightKey;
                            MGM.Remark = MGRM[i].Remark;
                            MGM.MemberGradeID = MGRM[i].MemberGradeID;
                            db.Insert("m_MemberGradeRight", "MemberGradeRightID", MGM);
                            rtnValue = 1;
                        }


                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
            
            return rtnValue;
        }

        /// <summary>
        /// 修改会员级别权限
        /// </summary>
        /// <param name="MGRM"></param>
        /// <returns></returns>
        public Int16 EditMemberGradeRightKey(List<MemberGradeRightModel> MGRM, long MemberGradeId)
        {
            try
            {
                using (var scope = db.GetTransaction())
                {
                    //删除
                    var result = db.Fetch<MemberGradeRightModel>(string.Format(@"
                        delete from m_MemberGradeRight where MemberGradeID={0}", MemberGradeId));

                    foreach (var item in MGRM)
                    {
                        db.Insert("m_MemberGradeRight", "MemberGradeRightID", item);
//                        Sql _sql = new Sql().Append(@"update m_MemberRight set RightValue=@0 where RightKey=@1 and MemberID in(
//                            select MemberID from m_Member as m
//                            inner join m_MemberGrade as mg on mg.MemberGradeID=m.MemberGradeID
//                            where mg.MemberGradeID=@2)", item.RightValue, item.RightKey, item.MemberGradeID);

//                        db.Execute(_sql);
                    }
                    scope.Complete();
                }

                return 1;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
          

//            List<MemberGradeRightModel> CommonList = new List<MemberGradeRightModel>();
//            using (var scope = db.GetTransaction())
//            {
//                try
//                {
//                    var result = db.Fetch<MemberGradeRightModel>(string.Format(@"
//                        SELECT * from m_MemberGradeRight where MemberGradeID={0}", MemberGradeId));
//                    if (result.Count > 0)
//                    {
//                        //修改
//                        for (int i = 0; i < result.Count; i++)
//                        {
//                            for (int k = 0; k < MGRM.Count; k++)
//                            {

//                                if (MGRM[k].RightKey.Equals(result[i].RightKey))
//                                {
//                                    MemberGradeRightModel MGM = new MemberGradeRightModel();
//                                    MGM.MemberGradeID = MemberGradeId;
//                                    MGM.MemberGradeRightID = result[i].MemberGradeRightID;
//                                    MGM.Remark = MGRM[k].Remark;
//                                    MGM.RightKey = MGRM[k].RightKey;
//                                    MGM.RightValue = MGRM[k].RightValue;
//                                    CommonList.Add(MGM);
//                                    db.Update("m_MemberGradeRight", "MemberGradeRightID", MGM, MGM.MemberGradeRightID);
//                                    rtnValue = 1;
//                                }
//                            }
//                        }

//                        if (MGRM.Count == CommonList.Count)
//                        {
//                            MGRM = new List<MemberGradeRightModel>();
//                        }
//                        else
//                        {
//                            for (int i = 0; i < MGRM.Count; i++)
//                            {
//                                for (int k = 0; k < CommonList.Count; k++)
//                                {
//                                    if (MGRM[i].RightKey.Equals(CommonList[k].RightKey))
//                                    {
//                                        MGRM.Remove(MGRM[i]);
//                                    }

//                                }
//                            }
//                        }




//                        for (int i = 0; i < MGRM.Count; i++)
//                        {
//                            MemberGradeRightModel MGM = new MemberGradeRightModel();
//                            var result1 = db.Fetch<MemberGradeRightVM>(string.Format(@"
//                            SELECT * from m_RightKey WHERE RightKey='{0}'", MGRM[i].RightKey));
//                            //查看多语言KEY                 
//                            MGM.RightValue = 1;
//                            MGM.RightKey = MGRM[i].RightKey;
//                            MGM.Remark = MGRM[i].Remark;
//                            MGM.MemberGradeID = MGRM[i].MemberGradeID;
//                            db.Insert("m_MemberGradeRight", "MemberGradeRightID", MGM);
//                            rtnValue = 1;
//                        }

//                        List<MemberGradeRightModel> CommonList1 = new List<MemberGradeRightModel>();
//                        for (int k = 0; k < CommonList.Count; k++)
//                        {
//                            for (int i = 0; i < result.Count; i++)
//                            {
//                                if (result[i].RightKey.Equals(CommonList[k].RightKey))
//                                {
//                                    result.Remove(result[i]);
//                                }
//                            }
//                        }


//                        for (int i = 0; i < result.Count; i++)
//                        {
//                            MemberGradeRightModel MGM = new MemberGradeRightModel();
//                            MGM.MemberGradeID = MemberGradeId;
//                            MGM.MemberGradeRightID = result[i].MemberGradeRightID;
//                            MGM.Remark = result[i].Remark;
//                            MGM.RightKey = result[i].RightKey;
//                            MGM.RightValue = 0;
//                            db.Update("m_MemberGradeRight", "MemberGradeRightID", MGM, MGM.MemberGradeRightID);
//                            rtnValue = 1;
//                        }


//                    }
//                    else
//                    {
//                        //新增
//                        for (int i = 0; i < MGRM.Count; i++)
//                        {
//                            MemberGradeRightModel MGM = new MemberGradeRightModel();
//                            var result1 = db.Fetch<MemberGradeRightVM>(string.Format(@"
//                        SELECT * from m_RightKey WHERE RightKey='{0}'", MGRM[i].RightKey));
//                            //查看多语言KEY
//                            MGM.RightValue = 1;
//                            MGM.RightKey = MGRM[i].RightKey;
//                            MGM.Remark = MGRM[i].Remark;
//                            MGM.MemberGradeID = MGRM[i].MemberGradeID;
//                            db.Insert("m_MemberGradeRight", "MemberGradeRightID", MGM);
//                            rtnValue = 1;
//                        }


                    //}
                    //scope.Complete();
            //    }
            //    catch (Exception ex)
            //    {

            //        throw ex;
            //    }
            //}

            //return rtnValue;
        }

        public Int16 EditMemberGradeRight(long MemberGradeId )
        {
            Int16 rtnValue = -1;
            using (var scope = db.GetTransaction())
            {
                try
                {
                     var result = db.Fetch<MemberGradeRightModel>(string.Format(@"
                        SELECT * from m_MemberGradeRight where MemberGradeID={0}", MemberGradeId));
                     if (result.Count > 0)
                     {
                         for (int i = 0; i < result.Count; i++)
                         {

                            MemberGradeRightModel MGM = new MemberGradeRightModel();
                            MGM.MemberGradeID = result[i].MemberGradeID;
                            MGM.MemberGradeRightID = result[i].MemberGradeRightID;
                            MGM.Remark = result[i].Remark;
                            MGM.RightKey = result[i].RightKey;
                            MGM.RightValue = 0;
                            db.Update("m_MemberGradeRight", "MemberGradeRightID", MGM, MGM.MemberGradeRightID);
                            rtnValue = 1;
                           
                         }
                         scope.Complete();
                     }
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            return rtnValue;

        }
        #endregion

        #region 【删除处理】

        /// <summary>
        /// 删除消息信息
        /// </summary>
        /// <param name="MessageID"></param>
        /// <returns></returns>
        public Int16 DeleteMessage(string MessageID)
        {
            Int16 rtnValue = -1;

            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Delete("t_Message", "MessageID", null, MessageID);
                    scope.Complete();

                    rtnValue = 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rtnValue;
        }

        /// <summary>
        /// 删除用户级别
        /// </summary>
        /// <param name="memberGradeId"></param>
        /// <returns></returns>
        public Int16 DeleteMemberGrade(string[] memberGradeId)
        {
            Int16 rtnValue = -1;
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in memberGradeId)
                    {
                        //查看用户级别是否启用于级别权限
                        var result = db.Fetch<Valeo.Domain.Member.MemberModel>(string.Format(@"
                        SELECT * from m_Member where MemberGradeID={0} ", item));
                        if (result.Count > 0)
                        {
                            rtnValue = 0;
                        }
                        else
                        {
                            db.Delete("m_MemberGrade", "MemberGradeID", null, item);
                            db.Delete("m_MemberGradeRight", "MemberGradeID", null, item);
                            rtnValue = 1;
                        }
                    }
                     
                    scope.Complete();
                   
                }
                catch (Exception)
                {
                    rtnValue = -1;
                    throw;
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 删除会员操作日志
        /// </summary>
        /// <param name="messageIds"></param>
        public void DeleteLog(string[] messageIds)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var messgeId in messageIds)
                    {
                        db.Delete<LogRecordModel>("Where LogID=@0", messgeId); 
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        
        #endregion
    }
}
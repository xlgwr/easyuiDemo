using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valeo;
using log4net;
using Valeo.Domain;
using Valeo.Service;
using System.Threading;
using System.Globalization;
using Valeo.Common;

namespace Valeo.Controllers
{
    [Localization]
    public class Base2Controller : Controller
    {
        UsersOperationhistoryService logservice = new UsersOperationhistoryService();
        public UserInfo LoginUser { get; set; }

        public LogHelper log;

        public Base2Controller()
        {
            log = new LogHelper(this.GetType());
        }

        /// <summary>
        /// 设置多语言
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public ActionResult Setlanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

                /// 把设置保存进cookie
                HttpCookie _cookie = new HttpCookie("Valeo.CurrentUICulture2", Thread.CurrentThread.CurrentUICulture.Name);
                _cookie.Expires = DateTime.MaxValue;
                Response.SetCookie(_cookie);
            }

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// 执行控制器方法之前先执行该方法
        /// 获取自定义SessionId的值，然后从Memcache中取出
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isExt = false;
              
            var cookie = Request.Cookies["ValeoSessionIdback"];

            if (cookie != null)
            {
                //获取自定义的SessionId
                string sessionId = cookie.Value;
                object obj = Common.MemberHelper.GetCache(sessionId);
                if (obj != null)
                {
                    cookie.Expires = DateTime.Now.AddMinutes(90);
                    Response.SetCookie(cookie);

                    LoginUser = Valeo.Common.SerializerHelper.DeserializeToObject<UserInfo>(obj.ToString());
                    //用户ID
                    ViewBag.Uid = LoginUser.UserID;
                    //用户名称
                    ViewBag.Uname = LoginUser.UserName;
                    isExt = true;
                }
            }

            if (!isExt) //用户没登录
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }


        /// <summary>
        ///  获取指定权限
        /// </summary>
        /// <param name="key"></param> 
        /// <returns>1:true, 0: false</returns>
        public bool GetRightValue(string key)
        {

            bool retValue = false;

            try
            {
                if (LoginUser == null) return retValue;

                //List<MemberRightVM> lstMemberRight = LoginUser.ListMemberRight.Where(t => t.RightKey.Equals(key)).ToList<MemberRightVM>();
                //if (lstMemberRight != null && lstMemberRight.Count > 0)
                //{
                //    retValue = lstMemberRight[0].RightValue == 1 ? true : false;
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return retValue;
        }

        /// <summary>
        ///  缩写信息（名称）
        /// </summary>
        /// <param name="original"></param>
        /// <param name="convertFormat"></param>
        /// <returns></returns>
        public string GetShortName(string original, Enums.ConvertFormat convertFormat)
        {

            string retValue;

            try
            {
                retValue = original.Trim() + " ";

                if (convertFormat == Enums.ConvertFormat.Abb2Full)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortNameVM)
                    {
                        retValue = retValue.Replace(item.ShortName + " ", item.LongName + " ");
                    }

                }
                else if (convertFormat == Enums.ConvertFormat.Full2Abb)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortNameVM)
                    {
                        retValue = retValue.Replace(item.LongName + " ", item.ShortName + " ");

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retValue.Trim();
        }

        /// <summary>
        ///  缩写信息（地址）
        /// </summary>
        /// <param name="original"></param>
        /// <param name="convertFormat"></param>
        /// <returns></returns>
        public string GetShortAddr(string original, Enums.ConvertFormat convertFormat)
        {

            string retValue;

            try
            {
                retValue = original.Trim() + " ";

                if (convertFormat == Enums.ConvertFormat.Abb2Full)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortAddrVM)
                    {
                        retValue = retValue.Replace(item.ShortName + " ", item.LongName + " ");
                    }

                }
                else if (convertFormat == Enums.ConvertFormat.Full2Abb)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortAddrVM)
                    {
                        retValue = retValue.Replace(item.LongName + " ", item.ShortName + " ");

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retValue.Trim();
        }

        /// <summary>
        /// 文字转换（1:简体-拼音；2:简体-繁 3:繁-拼音）
        /// </summary>
        /// <param name="original"></param>
        /// <param name="convertFormat"></param>
        /// <returns></returns>
        public string GetWord3(string original, Enums.ConvertFormat convertFormat)
        {

            string retValue;

            try
            {
                retValue = original.Trim() + " ";

                switch (convertFormat)
                {
                    case Enums.ConvertFormat.Simp2Trad:
                        //2:简体-繁
                        foreach (WordVM item in LoginUser.ListWord2VM)
                        {
                            retValue = retValue.Replace(item.WordKey + " ", item.WordValue + " ");
                        }
                        break;

                    case Enums.ConvertFormat.Trad2Simp:
                        //2:简体-繁
                        foreach (WordVM item in LoginUser.ListWord2VM)
                        {
                            retValue = retValue.Replace(item.WordValue + " ", item.WordKey + " ");
                        }
                        break;

                    case Enums.ConvertFormat.Trad2Alph:
                        //3:繁-拼音
                        foreach (WordVM item in LoginUser.ListWord3VM)
                        {
                            retValue = retValue.Replace(item.WordKey + " ", item.WordValue + " ");
                        }
                        break;

                    case Enums.ConvertFormat.Simp2Alph:
                        //1:简体-拼音
                        foreach (WordVM item in LoginUser.ListWord1VM)
                        {
                            retValue = retValue.Replace(item.WordKey + " ", item.WordValue + " ");
                        }
                        break;

                }

            }
            catch (Exception)
            {
                throw;
            }

            return retValue.Trim();
        }
        /// <summary>
        /// 类别(0:用户日志 1:会员日志)
        /// 操作分类(0:新建 1:修改 2:删除 3:查询等等)
        /// </summary>
        /// <param name="Type">类别(0:用户日志 1:会员日志)</param>
        /// <param name="operateType">操作分类(0:新建 1:修改 2:删除 3:查询等等)</param>
        /// <param name="msg"></param>
        /// <param name="from"></param>
        public void addLog(int type,int operateType, string msg, string from)
        {
            try
            {
                LogRecordModel tmpLog = new LogRecordModel();
                tmpLog.AddDateTime = DateTime.Now;
                tmpLog.Content = msg;
                tmpLog.Form = from;
                tmpLog.OperateType = operateType;
                tmpLog.Type = type;
                tmpLog.UserID = LoginUser.UserID;
                logservice.Add(tmpLog);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }


        /// <summary>
        /// 类别(0:用户日志 1:会员日志)
        /// 操作分类(0:新建 1:修改 2:删除 3:查询等等)
        /// </summary>
        /// <param name="Type">类别(0:用户日志 1:会员日志)</param>
        /// <param name="operateType">操作分类(0:新建 1:修改 2:删除 3:查询等等)</param>
        /// <param name="msg"></param>
        /// <param name="from"></param>
        public void addLog(int type, int operateType, string msg, string from, string UserID)
        {
            try
            {
                LogRecordModel tmpLog = new LogRecordModel();
                tmpLog.AddDateTime = DateTime.Now;
                tmpLog.Content = msg;
                tmpLog.Form = from;
                tmpLog.OperateType = operateType;
                tmpLog.Type = type;
                tmpLog.UserID = UserID;
                logservice.Add(tmpLog);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }
    }
}

using Valeo.Common;
using Valeo.Controllers;
using Valeo.Domain;
using Valeo.Domain.User;
using Valeo.Lang;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Main
{
    public class LoginController : Controller
    {
        MemberService service = new MemberService();

        public LogHelper log;

        public string memberid;
        // GET: Login
        public ActionResult Index()
        {
            var httpCookie = Response.Cookies["ValeoSessionIdback"];
            if (httpCookie != null) 
                httpCookie.Value = "";
            var cookie = Response.Cookies["ValeoVerificationIdback"];
            if (cookie != null)
                cookie.Value = "";
 
            return View();
        }

        #region 用户登录
        
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            string message = "";
            memberid = userName;
            try
            { 
                //对用户名、密码进行过滤
                var userInfo = service.GetLogin(userName, password);

                if (userInfo == null)
                {
                    message = BaseRes.LGN_MSG_003;

                    addLog(0, 3, message, VarKey.ServicePage.Login.ToString());

                    return Json(JsonHandler.CreateMessage(0, message));
                }
                else
                {

                    service.GetWord(userInfo);

                    #region 利用Memcache模拟Session进行共享用户Session信息

                    //自己创建的SessionId，作为Memcache的Key
                    string sessionId = Guid.NewGuid().ToString();

                    //将用户的信息存储到Memcache中
                    //Common.MemberHelper.SetCache(sessionId, Common.SerializerHelper.SerializerToString(userInfo),DateTime.Now.AddMinutes(600));

                    //设置Cache滑动过期时间为1小时
                    Common.MemberHelper.SetCache(sessionId, Common.SerializerHelper.SerializerToString(userInfo), new TimeSpan(0, 24, 0, 0));

                    //然后将自创的SessionId以Cookie的形式返回给浏览器，存储到浏览器端的内存中。
                    var httpCookie = Response.Cookies["ValeoSessionIdback"];
                    
                    if (httpCookie != null)
                    {
                        httpCookie.Value = sessionId;
                        httpCookie.Expires = DateTime.Now.AddMinutes(60);
                    }

                    var cookie = Response.Cookies["ValeoVerificationIdback"];
                    if (cookie != null)
                    {
                        cookie.Value = Encryption.Encode(userName);
                        cookie.Expires = DateTime.MaxValue;
                    }
 
                    #endregion

                }

                if (string.IsNullOrEmpty(message))
                {

                    addLog(0, 3,"登录成功", VarKey.ServicePage.Login.ToString());
                    return Json(JsonHandler.CreateMessage(1, ""));
                }
                else
                {
                    addLog(0, 3, message, VarKey.ServicePage.Login.ToString());
                    return Json(JsonHandler.CreateMessage(0, message));
                }

            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_QUERY_FAIL));
            }
        }
  
        #endregion

        #region 忘记密码

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="userName"></param> 
        /// <returns></returns>
        [HttpPost]
        public JsonResult Forget(string userName)
        {
            bool isSuc = false;
            memberid=userName;
            try
            {

                string url = string.Format("{0}://{1}:{2}/PswdReset/Index?key="
                    , Request.Url.OriginalString.Substring(0, Request.Url.OriginalString.IndexOf(':'))
                    , Request.Url.Host
                    , Request.Url.Port);

                //对用户名、密码进行过滤
                UserModel userInfo = service.GetUser(userName);
                //userInfo.Password = "";

                addLog(0, 3, "忘记密码", VarKey.ServicePage.Login.ToString());
                if (userInfo == null)
                {
                    return Json(JsonHandler.CreateMessage(0, BaseRes.LGN_MSG_004));
                }
                else
                {
                    //发送邮件处理 
                    isSuc = service.SetSendForget(userInfo, url);
                    if (isSuc)
                    {
                        return Json(JsonHandler.CreateMessage(1, BaseRes.LGN_MSG_005));
                    }
                    else
                    {
                        return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_EMAIL_FAIL));
                    }
                }

            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_QUERY_FAIL));
            }
        }

        #endregion

        #region 展示验证码
        //public ActionResult ValidateCode()
        //{
        //    //CZBK.HeiMaOA.Common.ValidateCode validateCode = new CZBK.HeiMaOA.Common.ValidateCode();
        //    //string code = validateCode.CreateValidateCode(4);
        //    //Session["validateCode"] = code;
        //    //byte[] buffer = validateCode.CreateValidateGraphic(code);
        //    //return File(buffer, "image/jpeg");
        //}
        #endregion

        public void addLog(int type, int operateType, string msg, string from)
        {
            try
            {
                UsersOperationhistoryService logservice = new UsersOperationhistoryService();
                LogRecordModel tmpLog = new LogRecordModel();
                tmpLog.AddDateTime = DateTime.Now;
                tmpLog.Content = msg;
                tmpLog.Form = from;
                tmpLog.OperateType = operateType;
                tmpLog.Type = type;
                tmpLog.UserID = memberid;
                logservice.Add(tmpLog);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
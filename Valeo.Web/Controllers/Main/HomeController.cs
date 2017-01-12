using Valeo.Controllers;
using Valeo.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valeo.Service.Main;

namespace Valeo.Main
{
    public class HomeController : BaseController
    {
        private readonly HomeService _homeService = new HomeService();
        // GET: Home
        public ActionResult Index()
        {
            var userSysModule = _homeService.GetUserSysModule(LoginUser.UserGradeID);
            ViewBag.userInfo = LoginUser;
            ViewBag.UserSysModule = userSysModule;
            return View();
        }


        /// <summary>
        /// 注销处理
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLogout()
        {
 
            try
            {

                string msg = string.Format("用户注销成功");
                addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString());
               
                var json = new
                {
                    memberName = LoginUser.UserID 
                };

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { result = BaseRes.COM_MSG_QUERY_FAIL });
            }
        }


        /// <summary>
        /// 一览数据查询
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLoginUser()
        { 

            try
            {
          
                var json = new
                {
                    memberName = LoginUser.UserID,
                    memberGradeID = LoginUser.UserGradeID, 
                };

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { result = BaseRes.COM_MSG_QUERY_FAIL });
            }
        }
  
    }
}
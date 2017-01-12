using Valeo.Service.User;
using Valeo.Service.UserGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valeo.Domain;
using Valeo.Service;
using PetaPoco;

namespace Valeo.Controllers.Email
{
    public class EmailRecodController : BaseController
    {
        //业务处理对象
        EmailService Service = new EmailService();
       

        // GET: EmialRecod
        #region 【查询】

        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "EmailRecod").ToList();
            return View();
        }

        public JsonResult EmailRecodPage(string Email, string Style, string SendDateStart, string SendDateEnd, string sort, string order, long page = 1, long rows = 10)
        {

            try
            {
                 Page<EmailListModel> list = Service.GetList(page, rows, Email, Style, SendDateStart,SendDateEnd, sort, order);
            var emailRecod = list.Items;
            var emailRecodlist = from recod in emailRecod
                           select new
                           {
                               recod.ToMail,
                               recod.Subject,
                               recod.FromMail,
                               recod.ReceiveTime_Text,
                               
                           };
            //日志   
            var msg = "邮件管理:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.EmailManager.ToString());

            var result = new
            {
                total = list.TotalItems,
                rows = emailRecodlist
            };
            return Json(result);
            }
            catch (Exception ex)
            { 
                //日志   
                var msg = "邮件管理:" + "查询失败";
                addLog(0, 3, msg, VarKey.ServicePage.EmailManager.ToString());
                
                throw ex;
            }
        }
      
        #endregion

        #region 【添加】

        #endregion

        #region 【修改】

        #endregion

        #region 【共通】

        #endregion
    }
}
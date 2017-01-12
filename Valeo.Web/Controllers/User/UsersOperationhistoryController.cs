using Valeo.Common;
using Valeo.Domain.User;
using Valeo.Lang;
using Valeo.Service;
using Valeo.Service.UserGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.User
{
    /// <summary>
    /// 用户操作记录
    /// </summary>
    public class UsersOperationhistoryController : BaseController
    {

        UsersOperationhistoryService logservice = new UsersOperationhistoryService();
        UserGradeService userGradeService = new UserGradeService();
        #region 【查询处理】

        // GET: UsersOperationhistory
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "UsersOperationhistory").ToList();
            var userGrades = userGradeService.GetUserGradeList();
            ViewBag.UserGrades = userGrades;

            List<SelectListItem> PageList = logservice.getServicePageSelectList();
            ViewBag.pageList = PageList;
            return View();
        }
        public JsonResult ListPage(LogRecordSearchModel condition, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            //var msg = "";
            var pageModel = logservice.GetListPages(page, rows, sort, order, condition);
            List<SelectListItem> PageList = logservice.getServicePageSelectList();

            foreach (var item in pageModel.Items)
            {
                var tmpDD = PageList.Where(a => a.Value == item.Form).SingleOrDefault();
                if (tmpDD != null)
                {
                    item.FormVM = tmpDD.Text;
                }
                else
                {
                    item.FormVM = item.Form;
                }

                item.AddDateTime = DataConvert.DateTime2Format(item.AddDateTime, Enums.DateTimeFormat.DateTime);
            }
            var result = new
            {
                total = pageModel.TotalItems,
                rows = pageModel.Items
            };

            //msg = "用户操作记录:" + "-->查询成功,条件：" + "用户ID:" + condition.UserID
            //    + ",用户级别:" + condition.UserGradeID
            //    + ",访问页面:" + condition.Form
            //    + ",开始时间:" + condition.AddDateTimeB
            //    + ",结束时间:" + condition.AddDateTimeE;
            //addLog(0, 3, msg, VarKey.ServicePage.UserInfoManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【删除操作记录】
        /// <summary>
        /// 删除操作记录
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Deletes(string id)
        {
            var msg = "";
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Json(JsonHandler.CreateMessage(0, "提交的数据为空"), JsonRequestBehavior.AllowGet);

                }

                if (logservice.Deletes(id))
                {
                    msg = "用户操作记录:" + "-->删除成功:ids:" + id;
                    addLog(0, 2, msg, VarKey.ServicePage.UserInfoManager.ToString());
                    return Json(JsonHandler.CreateMessage(1, BaseRes.COM_MSG_DEL_SUC, id.ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "用户操作记录:" + "-->删除失败:ids:" + id;
                    addLog(0, 2, msg, VarKey.ServicePage.UserInfoManager.ToString());
                    return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_DEL_FAIL), JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
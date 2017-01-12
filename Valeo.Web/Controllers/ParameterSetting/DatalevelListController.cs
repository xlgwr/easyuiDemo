using Valeo.Controllers;
using Valeo.Domain;
using Valeo.Lang;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.ParameterSetting
{
    /// <summary>
    /// 数据级别设定
    /// </summary>
    public class DatalevelListController : BaseController
    {

        DataGradeService dgservice = new DataGradeService();

        #region 【查询处理】
        // GET: DatalevelList
        public ActionResult Index()
        {            
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "DatalevelList").ToList();
            return View();
        }
        public JsonResult ListPage(DataGradeModel condition, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            var pageModel = dgservice.GetListPages(page, rows, sort, order, condition);

            var result = new
            {
                total = pageModel.TotalItems,
                rows = pageModel.Items
            };

            var msg = BaseRes.DLL_MSG_004+":"+BaseRes.DLL_MSG_002 + BaseRes.DLL_MSG_003;
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());

            return Json(result);
        }
        #endregion
        [HttpPost]
        public JsonResult Add(DataGradeModel model)
        {
            var msg = "";
            try
            {
                dgservice.Add(model);

                msg = BaseRes.DLL_MSG_004 + ":" + BaseRes.DLL_MSG_005;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 1, Msg = BaseRes.DLL_MSG_005 });//"添加成功!"
            }
            catch
            {
                msg = BaseRes.DLL_MSG_004 + ":" + BaseRes.DLL_MSG_006;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.DLL_MSG_006 }); //"添加失败，用户名重复!" 
            }
        }
        [HttpPost]
        public JsonResult Edit(DataGradeModel model)
        {
            var msg = "";
            try
            {
                dgservice.Edit(model);

                msg = BaseRes.DLL_MSG_004 + ":" + BaseRes.DLL_MSG_007;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 1, Msg = BaseRes.DLL_MSG_007 });// "修改成功!"
            }
            catch
            {
                msg = BaseRes.DLL_MSG_004 + ":" + BaseRes.DLL_MSG_008;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.DLL_MSG_009 });//"错误，请稍后在试!" 
            }
        }

        public JsonResult Delete(long dataGradeID)
        {
            var msg = "";
            if (dataGradeID > 0)
            {
                try
                {
                    dgservice.Deletes(dataGradeID);

                    msg = BaseRes.DLL_MSG_004 + ":" + BaseRes.DLL_MSG_010;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());

                    return Json(new { result = 1, Msg = BaseRes.DLL_MSG_010 });//""
                }
                catch (Exception)
                {
                    msg = BaseRes.DLL_MSG_004 + ":" + BaseRes.DLL_MSG_011;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());

                    return Json(new { result = 0, Msg = BaseRes.DLL_MSG_011 });//"删除失败!" 
                }
            }
            else
            {
                return Json(new { result = 0, Msg = BaseRes.DLL_MSG_011 });//"删除失败!"
            }

        }

    }
}
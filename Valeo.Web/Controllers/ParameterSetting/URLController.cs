using Valeo.Domain;
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
    /// 采集网址查看
    /// </summary>
    public class URLController : BaseController
    {

        URLService service = new URLService();
        #region 【查询处理】

        // GET: URL
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "URL").ToList();
            return View();
        }
        public JsonResult ListPage(URLModel condition, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            var pageModel = service.GetListPages(page, rows, sort, order, condition);

            var result = new
            {
                total = pageModel.TotalItems,
                rows = pageModel.Items
            };
            return Json(result);
        }
        #endregion

        [HttpGet]
        public JsonResult getModel(long id)
        {
            var result = service.GetModel(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(URLModel model)
        {
            var msg = "";
            try
            {
                if (string.IsNullOrEmpty(model.URL) || string.IsNullOrEmpty(model.URLname))
                {
                    return Json(new { result = 0, Msg = BaseRes.SPS_MSG_001 });//"错误，请输入正确的参数名称、值!" 
                }
                //检查是否已加
                var checkModel = service.GetModel(model.URLid);
                if (checkModel != null)
                {
                    return Json(new { result = 0, Msg = BaseRes.SPS_MSG_014 });//"错误，请输入正确的参数名称、值!" 

                }
                //新增
                service.Add(model);

                msg = "采集网址" + ":" + BaseRes.SPS_MSG_002;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());


                return Json(new { result = 1, Msg = BaseRes.SPS_MSG_002 }, JsonRequestBehavior.AllowGet);// "添加成功！"
            }
            catch (Exception ex)
            {
                log.Error(ex);

                msg = "采集网址" + ":" + BaseRes.SPS_MSG_003;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_005 }, JsonRequestBehavior.AllowGet);//"错误，请稍后在试!" 
            }
        }
        [HttpPost]
        public JsonResult Edit(URLModel model)
        {
            var msg = "";
            try
            {
                if (string.IsNullOrEmpty(model.URL) || string.IsNullOrEmpty(model.URLname))
                {
                    return Json(new { result = 0, Msg = BaseRes.SPS_MSG_005 }, JsonRequestBehavior.AllowGet);//"错误，请输入正确的参数名称、值!" 
                }

                //修改
                service.Edit(model);

                msg = "采集网址" + ":" + BaseRes.SPS_MSG_004;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());


                return Json(new { result = 1, Msg = BaseRes.SPS_MSG_004 }, JsonRequestBehavior.AllowGet);// "修改成功!"
            }
            catch (Exception ex)
            {
                log.Error(ex);

                msg = "采集网址" + ":" + BaseRes.SPS_MSG_006;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_005 }, JsonRequestBehavior.AllowGet);//"错误，请稍后在试!" 
            }
        }

        public JsonResult Delete(long id)
        {
            var msg = "";
            if (id > 0)
            {
                try
                {
                    service.Delete(id);

                    msg = "采集网址" + ":" + BaseRes.DLL_MSG_010;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());

                    return Json(new { result = 1, Msg = BaseRes.DLL_MSG_010 });//""
                }
                catch (Exception ex)
                {
                    log.Error(ex);

                    msg = "采集网址" + ":" + BaseRes.DLL_MSG_011;
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
using Valeo.Domain;
using Valeo.Lang;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.ParameterSetting
{
    /// <summary>
    /// 系统参数设定
    /// </summary>
    public class SysParameterController : BaseController
    {
        SysParameterService service = new SysParameterService();
        // GET: SysParameter
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "SysParameter").ToList();
            return View();
        }
        public JsonResult ListPage(SysParameterModel condition, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            var pageModel = service.GetListPages(page, rows, sort, order, condition);

            var result = new
            {
                total = pageModel.TotalItems,
                rows = pageModel.Items
            };
            return Json(result);
        }
        [HttpGet]
        public JsonResult getModel(string id)
        {
            var result = service.GetModel(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(SysParameterModel model)
        {
            var msg = "";
            try
            {
                if (string.IsNullOrEmpty(model.Paramkey) || string.IsNullOrEmpty(model.Paramvalue))
                {
                    return Json(new { result = 0, Msg = BaseRes.SPS_MSG_001 });//"错误，请输入正确的参数名称、值!" 
                }
                //check the same
                var check = service.GetModel(model.Paramkey);
                if (check != null)
                {
                    return Json(new { result = 0, Msg = BaseRes.SPS_MSG_014 });//"错误，请输入正确的参数名称、值!" 
                }

                model.adduser = LoginUser.UserID;
                model.upduser = LoginUser.UserID;


                //新增
                service.Add(model);

                msg = BaseRes.SPS_TIT_001 + ":" + BaseRes.SPS_TIT_001 + ":" + BaseRes.SPS_MSG_002;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());


                return Json(new { result = 1, Msg = BaseRes.SPS_MSG_002 }, JsonRequestBehavior.AllowGet);// "添加成功！"
            }
            catch (Exception ex)
            {
                log.Error(ex);

                msg = BaseRes.SPS_TIT_001 + ":" + BaseRes.SPS_TIT_001 + ":" + BaseRes.SPS_MSG_003;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_005 }, JsonRequestBehavior.AllowGet);//"错误，请稍后在试!" 
            }
        }
        [HttpPost]
        public JsonResult Edit(SysParameterModel model)
        {
            var msg = "";
            try
            {
                if (string.IsNullOrEmpty(model.Paramkey) || string.IsNullOrEmpty(model.Paramvalue))
                {
                    return Json(new { result = 0, Msg = BaseRes.SPS_MSG_005 }, JsonRequestBehavior.AllowGet);//"错误，请输入正确的参数名称、值!" 
                }

                model.upduser = LoginUser.UserID;
                //修改
                service.Edit(model);

                msg = BaseRes.SPS_TIT_001 + ":" + BaseRes.SPS_MSG_004;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());


                return Json(new { result = 1, Msg = BaseRes.SPS_MSG_004 }, JsonRequestBehavior.AllowGet);// "修改成功!"
            }
            catch(Exception ex)
            {
                log.Error(ex);

                msg = BaseRes.SPS_TIT_001 + ":" + BaseRes.SPS_MSG_006;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_005 }, JsonRequestBehavior.AllowGet);//"错误，请稍后在试!" 
            }
        }

        public JsonResult Delete(string id)
        {
            var msg = "";
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    service.Delete(id);

                    msg = BaseRes.SPS_TIT_001 + ":" + BaseRes.DLL_MSG_010;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());

                    return Json(new { result = 1, Msg = BaseRes.DLL_MSG_010 });//""
                }
                catch (Exception ex)
                {
                    log.Error(ex);

                    msg = BaseRes.SPS_TIT_001 + ":" + BaseRes.DLL_MSG_011;
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
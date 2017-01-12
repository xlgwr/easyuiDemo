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
    /// 邮件参数设定
    /// </summary>
    public class SetEmailController : BaseController
    {
        SetEmailService service = new SetEmailService();
        // GET: SetEmail
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "SetEmail").ToList();
            return View();
        }
        [HttpGet]
        public JsonResult getModel()
        {
            var result = service.GetModel();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Change(SetEmailModel model)
        {
            var msg = "";
            try
            {
                if (model.SetEmailID > 0)
                {
                    //修改
                    service.Edit(model);

                    msg = BaseRes.SEM_TIT_001 + ":" + BaseRes.SEM_MSG_001;
                    addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                }
                else
                {
                    //新增
                    service.Add(model);
                    msg = BaseRes.SEM_TIT_001 + ":" + BaseRes.SEM_MSG_002;
                    addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                }



                return Json(new { result = 1, Msg = BaseRes.SEM_MSG_001 });// "修改成功!"
            }
            catch
            {
                msg = BaseRes.SEM_TIT_001 + ":" + BaseRes.DLL_MSG_008;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());

                return Json(new { result = 0, Msg = BaseRes.DLL_MSG_009 });//"错误，请稍后在试!" 
            }
        }
    }
}
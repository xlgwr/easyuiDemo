using Valeo.Common;
using Valeo.Lang;
using Valeo.Domain.Valeo;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valeo.Domain;

namespace Valeo.Controllers
{
    public class LableModelManageController : BaseController
    {
        v_lableModelService v_lableModelService = new v_lableModelService();

        // GET: v_lableModel
        #region 【查询处理】

        /// <summary>
        /// 获取标签模板
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllv_lableModel()
        {
            try
            {
                var jsonModel = v_lableModelService.GetAllv_lableModel();
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取标签模板出错"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "LableModelManage").ToList();
            return View();
        }

        public JsonResult Page(v_lableModel condition, string sort, string order, long page = 1, long rows = 2)
        {
            var pageModels = v_lableModelService.GetPagev_lableModels(page, rows, sort, order, condition);

            var trans = getCombox(EnumCombox.Unit);
            var josns = pageModels.Items;


            var result = new
            {
                total = pageModels.TotalItems,
                rows = josns
            };

            var msg = "标签模板管理:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【添加标签模板】

        /// <summary>
        /// 验证标签模板名
        /// </summary>
        /// <param name="v_lableModelName"></param>
        /// <returns></returns>
        public JsonResult Verification(string id = "")
        {
            if (v_lableModelService.IslableID(id.Trim()))
            {
                return Json(new { result = 1, Msg = "标签模板名称重复!" });
            }
            else
            {
                return Json(new { result = 0, Msg = "标签模板名称可用!" });
            }

        }
        public JsonResult getModel(string id)
        {
            var tmpModel = v_lableModelService.Getv_lableModel(id);
            if (tmpModel != null)
            {
                return Json(new { result = 1, Msg = "标签模板获取成功!", value = tmpModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0, Msg = id + "：标签模板不存在!", value = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Add()
        {
            ViewBag.isAdd = true;
            ViewBag.isSearch = false;

            v_lableModel v_lableModel = new v_lableModel();

            return View(v_lableModel);
        }

        public JsonResult AddSave(v_lableModel model)
        {
            try
            {
                if (v_lableModelService.IslableID(model.lableName))
                {
                    throw new Exception("添加失败，标签模板名称重复");
                }

                model.adduser = LoginUser.UserID;
                v_lableModelService.Add(model);
                var msg = "标签模板管理:" + "添加成功：" + model.lableID;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch (Exception ex)
            {
                var msg = "标签模板管理:" + "添加失败";
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = "添加失败，标签模板名称重复!" }); //"添加失败，标签模板名重复!" 
            }
        }
        #endregion

        #region 【修改标签模板】
        public ActionResult Edit(string id, int id2 = 0)
        {
            ViewBag.isAdd = false;
            var isSearch = false;
            if (id2 > 0)
            {
                isSearch = true;
            }
            ViewBag.isSearch = isSearch;
            v_lableModel v_lableModel = v_lableModelService.Getv_lableModel(id);
            return View("Add", v_lableModel);
        }

        public JsonResult EditSave(v_lableModel model)
        {
            try
            {
                model.upduser = LoginUser.UserID;
                v_lableModelService.Edit(model);
                var msg = "标签模板管理:" + "修改成功：" + model.lableID;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                var msg = "标签模板管理:" + "修改失败：" + model.lableID;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion



        #region【删除标签模板】

        public JsonResult Deletes(double[] id)
        {
            if (id.Length > 0)
            {
                try
                {
                    v_lableModelService.Deletes(id);
                    var msg = string.Format("标签模板管理，删除成功：{0}.", id);
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = string.Format("标签模板管理，删除失败：{0}.", id);
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1, Msg = BaseRes.USE_MSG_018 });//"" 删除失败!
                }
            }
            else
            {
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_018 });//""删除失败!
            }

        }
        #endregion

        #region 共通

        #endregion

    }
}
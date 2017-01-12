using Valeo.Common;
using Valeo.Lang;
using Valeo.Domain.Valeo;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers
{
    public class CartonManageController : BaseController
    {
        v_cartonService v_cartonService = new v_cartonService();

        // GET: v_carton
        #region 【查询处理】

        /// <summary>
        /// 获取纸箱
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllv_carton()
        {
            try
            {
                var jsonModel = v_cartonService.GetAllv_carton();
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取纸箱出错"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "CartonManage").ToList();
            return View();
        }

        public JsonResult Page(v_carton condition, string sort, string order, long page = 1, long rows = 2)
        {
            var pageModels = v_cartonService.GetPagev_cartons(page, rows, sort, order, condition);
            var result = new
            {
                total = pageModels.TotalItems,
                rows = pageModels.Items
            };
            var msg = "纸箱设定:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【添加纸箱】

        /// <summary>
        /// 验证纸箱名
        /// </summary>
        /// <param name="v_cartonName"></param>
        /// <returns></returns>
        public JsonResult Verification(string id)
        {
            if (v_cartonService.IscartonNO(id))
            {
                return Json(new { result = 1, Msg = "纸箱名称重复!" });
            }
            else
            {
                return Json(new { result = 0, Msg = "纸箱名称可用!" });
            }

        }
        public JsonResult getModel(string id)
        {
            var tmpModel = v_cartonService.Getv_carton(id);
            if (tmpModel != null)
            {
                return Json(new { result = 1, Msg = "纸箱获取成功!", value = tmpModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0, Msg = id + "：纸箱不存在!", value = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Add()
        {
            ViewBag.isAdd = true;
            ViewBag.isSearch = false;

            v_carton v_carton = new v_carton();

            return View(v_carton);
        }

        public JsonResult AddSave(v_carton model)
        {
            try
            {
                if (v_cartonService.IscartonNO(model.cartonNO))
                {
                    throw new Exception("添加失败，纸箱型号重复");
                }

                model.adduser = LoginUser.UserID;
                v_cartonService.Add(model);
                var msg = "纸箱设定:" + "添加成功：" + model.cartonNO;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch
            {
                var msg = "纸箱设定:" + "添加失败";
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = "添加失败，纸箱型号重复!"  }); //"添加失败，纸箱名重复!" 
            }
        }
        #endregion

        #region 【修改纸箱】
        public ActionResult Edit(string id,int id2)
        {
            ViewBag.isAdd = false;
            var isSearch = false;
            if (id2 > 0)
            {
                isSearch = true;
            }
            ViewBag.isSearch = isSearch;
            v_carton v_carton = v_cartonService.Getv_carton(id);
            return View("Add", v_carton);
        }

        public JsonResult EditSave(v_carton model)
        {
            try
            {
                model.upduser = LoginUser.UserID;
                v_cartonService.Edit(model);
                var msg = "纸箱设定:" + "修改成功：" + model.cartonNO;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                var msg = "纸箱设定:" + "修改失败：" + model.cartonNO;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion



        #region【删除纸箱】

        public JsonResult Deletes(string[] id)
        {
            if (id.Length > 0)
            {
                try
                {
                    v_cartonService.Deletes(id);
                    var msg = string.Format("纸箱设定，删除成功：{0}.", id);
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = string.Format("纸箱设定，删除失败：{0}.", id);
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
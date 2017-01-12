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
    public class MaterialManageController : BaseController
    {
        v_materialService v_materialService = new v_materialService();

        // GET: v_material
        #region 【查询处理】

        /// <summary>
        /// 获取物料
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllv_material()
        {
            try
            {
                var jsonModel = v_materialService.GetAllv_material();
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取物料出错"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "MaterialManage").ToList();
            return View();
        }

        public JsonResult Page(v_material condition, string sort, string order, long page = 1, long rows = 2)
        {
            var pageModels = v_materialService.GetPagev_materials(page, rows, sort, order, condition);

            var trans = getCombox(EnumCombox.Unit);
            var josns = from v in pageModels.Items
                        from a in trans
                        where v.unit == a.ComboxListKey.ToString()
                        select new
                        {
                            partNO = v.partNO,
                            partName = v.partName,
                            customerPartNO = v.customerPartNO,
                            cartonType = v.cartonType,
                            pcsNumber = v.pcsNumber,
                            weight = v.weight,
                            unit = v.unit,
                            remark = v.remark,
                            adduser = v.adduser,
                            upduser = v.upduser,
                            addtime = v.addtime,
                            updtime = v.updtime,
                            unitVM = a.ComboxListName
                        };

            var result = new
            {
                total = pageModels.TotalItems,
                rows = josns
            };

            var msg = "物料管理:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【添加物料】

        /// <summary>
        /// 验证物料名
        /// </summary>
        /// <param name="v_materialName"></param>
        /// <returns></returns>
        public JsonResult Verification(string id)
        {
            if (v_materialService.IspartNO(id))
            {
                return Json(new { result = 1, Msg = "物料名称重复!" });
            }
            else
            {
                return Json(new { result = 0, Msg = "物料名称可用!" });
            }

        }
        public JsonResult getModel(string id)
        {
            var tmpModel = v_materialService.Getv_material(id);
            if (tmpModel != null)
            {
                return Json(new { result = 1, Msg = "物料获取成功!", value = tmpModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0, Msg = id + "：物料不存在!", value = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Add()
        {
            ViewBag.isAdd = true;
            ViewBag.isSearch = false;

            v_material v_material = new v_material();

            return View(v_material);
        }

        public JsonResult AddSave(v_material model)
        {
            try
            {
                if (v_materialService.IspartNO(model.partNO))
                {
                    throw new Exception("添加失败，物料型号重复");
                }

                model.adduser = LoginUser.UserID;
                v_materialService.Add(model);
                var msg = "物料管理:" + "添加成功：" + model.partNO;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch
            {
                var msg = "物料管理:" + "添加失败";
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = "添加失败，物料型号重复!" }); //"添加失败，物料名重复!" 
            }
        }
        #endregion

        #region 【修改物料】
        public ActionResult Edit(string id,int id2=0)
        {
            ViewBag.isAdd = false;
            var isSearch = false;
            if (id2>0)
            {
                isSearch = true;
            }
            ViewBag.isSearch = isSearch;
            v_material v_material = v_materialService.Getv_material(id);
            return View("Add", v_material);
        }

        public JsonResult EditSave(v_material model)
        {
            try
            {
                model.upduser = LoginUser.UserID;
                v_materialService.Edit(model);
                var msg = "物料管理:" + "修改成功：" + model.partNO;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                var msg = "物料管理:" + "修改失败：" + model.partNO;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion



        #region【删除物料】

        public JsonResult Deletes(string[] id)
        {
            if (id.Length > 0)
            {
                try
                {
                    v_materialService.Deletes(id);
                    var msg = string.Format("物料管理，删除成功：{0}.", id);
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = string.Format("物料管理，删除失败：{0}.", id);
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
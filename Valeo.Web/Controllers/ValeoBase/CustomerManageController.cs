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
    public class CustomerManageController : BaseController
    {
        v_customerService v_customerService = new v_customerService();
        v_customer_docService v_customer_docService = new v_customer_docService();

        // GET: v_customer
        #region 【查询处理】

        /// <summary>
        /// 获取客户
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllv_customer()
        {
            try
            {
                var jsonModel = v_customerService.GetAllv_customer();
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取客户出错"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "CustomerManage").ToList();
            return View();
        }

        public JsonResult Page(v_customer condition, string sort, string order, long page = 1, long rows = 2)
        {
            var pageModels = v_customerService.GetPagev_customers(page, rows, sort, order, condition);

            var trans = getCombox(EnumCombox.Unit);
            var josns = pageModels.Items;


            var result = new
            {
                total = pageModels.TotalItems,
                rows = josns
            };

            var msg = "客户管理:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【添加客户】

        /// <summary>
        /// 验证客户名
        /// </summary>
        /// <param name="v_customerName"></param>
        /// <returns></returns>
        public JsonResult Verification(string id = "")
        {
            if (v_customerService.IscustomerNo(id.Trim()))
            {
                return Json(new { result = 1, Msg = "客户名称重复!" });
            }
            else
            {
                return Json(new { result = 0, Msg = "客户名称可用!" });
            }

        }
        public JsonResult getModel(string id)
        {
            var tmpModel = v_customerService.Getv_customer(id);
            var tmpModelList1 = v_customer_docService.GetAllv_customer_doc(id);
            if (tmpModel != null)
            {
                return Json(new { result = 1, Msg = "客户获取成功!", value = tmpModel, valueList = tmpModelList1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0, Msg = id + "：客户不存在!", value = "" , valueList = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Add()
        {
            ViewBag.isAdd = true;
            ViewBag.isSearch = false;

            v_customer v_customer = new v_customer();

            return View(v_customer);
        }

        public JsonResult AddSave(v_customer model, List<v_customer_doc> modelsDetails)
        {
            try
            {
                if (v_customerService.IscustomerNo(model.customerNo))
                {
                    throw new Exception("添加失败，客户名称重复");
                }

                model.adduser = LoginUser.UserID;
                v_customerService.Add(model, modelsDetails);

                var msg = "客户管理:" + "添加成功：" + model.customerNo;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch (Exception ex)
            {
                var msg = "客户管理:" + "添加失败";
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = "添加失败，客户名称重复!" }); //"添加失败，客户名重复!" 
            }
        }
        #endregion

        #region 【修改客户】
        public ActionResult Edit(string id, int id2 = 0)
        {
            ViewBag.isAdd = false;
            var isSearch = false;
            if (id2 > 0)
            {
                isSearch = true;
            }
            ViewBag.isSearch = isSearch;
            v_customer v_customer = v_customerService.Getv_customer(id);
            return View("Add", v_customer);
        }

        public JsonResult EditSave(v_customer model, List<v_customer_doc> modelsDetails)
        {
            try
            {
                model.upduser = LoginUser.UserID;
                v_customerService.Edit(model, modelsDetails);
                var msg = "客户管理:" + "修改成功：" + model.customerNo;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                var msg = "客户管理:" + "修改失败：" + model.customerNo;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion



        #region【删除客户】

        public JsonResult Deletes(string[] id)
        {
            if (id.Length > 0)
            {
                try
                {
                    v_customerService.Deletes(id);
                    var msg = string.Format("客户管理，删除成功：{0}.", id);
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = string.Format("客户管理，删除失败：{0}.", id);
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
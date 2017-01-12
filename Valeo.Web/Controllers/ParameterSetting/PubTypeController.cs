using Valeo.Domain.Models;
using Valeo.Domain.ParameterSetting;
using Valeo.Lang;
using Valeo.Service.ParameterSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.ParameterSetting
{
    public class PubTypeController : BaseController
    {

        PubTypeService _Service = new PubTypeService();
        // GET: PubType
        public ActionResult Index()
        {            
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "PubType").ToList();
            return View();
        }
        public JsonResult GetPubTypeList(long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            var pubTypePage = _Service.GetPubTypeList(page, rows, sort, order);
            var pubTypeList = pubTypePage.Items;
            //_invoiceService.SetProductList(productList);
            var result = new
            {
                total = pubTypePage.TotalItems,
                rows = pubTypeList
            };
            var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_024;
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
            return Json(result);
        }


        public ActionResult Add()
        {
            List<SelectListItem> TableList = _Service.GetTableList();
            ViewBag.TableList = TableList;
            List<PublicTableModel> PubTab = new List<PublicTableModel>();
            ViewBag.PubTab = PubTab;
            PublicTypeModel PTM = new PublicTypeModel();
            return View("_Edit", PTM);
        }
        public JsonResult AddSave(PubTypeVM PM)
        {
            long result = _Service.AddSave(PM);
            if (result == 0)
            {
                return Json(new { result = 0, Msg = BaseRes.PTC_COL_002 });// "重复!"
            }
            else if (result == 1)
            {
                var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_029;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_001 });// "添加成功!"
            }
            else
            {
                var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_030;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_002 });// "添加失败!"
            }
        }

        public ActionResult Edit(long PublicTypeID, bool isEdit = true)
        {
            ViewBag.IsEdit = isEdit;
            List<SelectListItem> TableList = _Service.GetTableList();
            ViewBag.TableList = TableList;
            List<PublicTableModel> PubTab = _Service.GetSelectTableList(PublicTypeID);
            ViewBag.PubTab = PubTab;
            PublicTypeModel PTM = _Service.GetSinglePubType(PublicTypeID);
            return View("_Edit",PTM);
        }
      
        public JsonResult EditSave(PubTypeVM PM)
        {
            long result = _Service.EditSave(PM);
            if (result == 0)
            {
                return Json(new { result = 0, Msg = BaseRes.PTC_COL_002 });// "重复!"
            }
            else if (result == 1)
            {
                var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_025;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_003 });// "添加成功!"
            }
            else
            {
                var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_026;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_004 });// "添加失败!"
            }
        }

        #region 删除处理
        public JsonResult Delete(long[] PublicTypeID)
        {
            try
            {
                long result = _Service.Delete(PublicTypeID);
                if (result == 0)
                {
                    return Json(new { result = 0, Msg = BaseRes.OEC_COL_189 });
                }
                else if (result == 1)
                {
                    var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_028;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1, Msg = BaseRes.COM_MSG_DEL_SUC });// "删除成功!"
                }
                else
                {
                    var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_027;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 0, Msg = BaseRes.COM_MSG_DEL_FAIL });
                }

            }
            catch (Exception)
            {
                var msg = BaseRes.PTC_COL_001 + BaseRes.MGC_CTL_027;
                addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.COM_MSG_DEL_FAIL });// "删除失败!"
                throw;
            }
        }
        #endregion
    }
}
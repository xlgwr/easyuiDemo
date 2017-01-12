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
    public class CompanyInfoSetController : BaseController
    {
        ComParameterService _Service = new ComParameterService();

        #region 查询处理
        // GET: CompanyInfoSet
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "CompanyInfoSet").ToList();
            return View();
        }

        public JsonResult GetComList(ComParameterModel CPM, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            try
            {
                var comboxMode = _Service.GetAllComList(CPM, page, rows,sort,order);
                var comboxs = comboxMode.Items;
                var comboxList = from item in comboxs
                                 select new
                                 {
                                     item.Comkey,
                                     item.Comvalue,
                                     item.Remark,
                                     item.Comtype,
                                     item.DspNo
                                 };
                var result = new
                {
                    total = comboxMode.TotalItems,
                    rows = comboxList
                };
                var msg = BaseRes.CIS_COL_001 + BaseRes.MGC_CTL_024;
                addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(result);
            }
            catch (Exception)
            {

                throw;

            }
        }

        #endregion

        #region 新增处理
        public ActionResult Add()
        {
            ComParameterModel CPM = new ComParameterModel();
            return View("_Edit",CPM);
        }
        public JsonResult AddSave(ComParameterModel CPM)
        {
            long result = _Service.AddSave(CPM);
            if (result==0)
            {
                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_014 });// "重复!"
            }
            else if (result == 1)
            {
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_001 });// "添加成功!"
            }
            else
            {
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_002 });// "添加失败!"
            }
            
        }

        #endregion

        #region 修改处理

        public ActionResult Edit(string Comkey, bool isEdit = true)
        {
            ViewBag.IsEdit = isEdit;
            ComParameterModel CPM = _Service.GetSingleComParament(Comkey);
            return View("_Edit", CPM);
        }

        public JsonResult EditSave(ComParameterModel CPM)
        {
            long result = _Service.EditSave(CPM);
            if (result == 0)
            {
                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_014 });// "重复!"
            }
            else if (result == 1)
            {
                var msg = BaseRes.CIS_COL_001 + BaseRes.MGC_CTL_025;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_003 });// "添加成功!"
            }
            else
            {
                var msg = BaseRes.CIS_COL_001 + BaseRes.MGC_CTL_026;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_004 });// "添加失败!"
            }
            
        }
        #endregion

        #region 删除处理
        public JsonResult DeleteData(string Comkey)
        {
            try
            {
                _Service.Delete(Comkey);
                return Json(new { result = 1, Msg = BaseRes.COM_MSG_DEL_SUC });// "删除成功!"
            }
            catch (Exception)
            {
                return Json(new { result = 1, Msg = BaseRes.COM_MSG_DEL_FAIL });// "删除成功!"
                throw;
            }
        }
        #endregion
    }
}
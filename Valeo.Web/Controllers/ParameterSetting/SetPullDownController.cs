using Valeo.Domain;
using Valeo.Domain.Combox;
using Valeo.Lang;
using Valeo.Service.ParameterSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.ParameterSetting
{
    public class SetPullDownController :BaseController
    {
        SetPullDownService _Service = new SetPullDownService();
        #region 查询处理
        // GET: SetPullDown
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "SetPullDown").ToList();
            List<ComboxModel> ComboxList = _Service.GetALLCombox();
            ViewBag.ComboxList = ComboxList;
            return View();
        }

        public JsonResult GetComboxList(string ComboxId, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            try
            {
                var comboxMode = _Service.GetAllComBoxList(ComboxId, page, rows, sort, order);
               var comboxs=comboxMode.Items;
               var comboxList = from item in comboxs
                                select new
                                {
                                    item.ComboxId,
                                    item.ComboxListId,
                                    item.ComboxListKey,
                                    item.ComboxListName,
                                    item.DspNo,
                                    item.LanguageCode,
                                    item.ComboxName,
                                    item.Remark
                                };
               var result = new
               {
                   total = comboxMode.TotalItems,
                   rows = comboxList
               };
               var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_024;
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

        public ActionResult Add(string comboxId)
        {
             //List<SelectListItem> ListCombox=_Service.GetComboxList();
             
             //ViewBag.ListCombox = ListCombox;
             //ComboxListVM CLM = new ComboxListVM();

            ComboxListVM CLM = _Service.getMaxComboxList(comboxId);
            if (CLM==null)
            {
                CLM = new ComboxListVM();
                //ComboxModel CM = _Service.GetComboxId(comBoxName);

                CLM.ComboxId = Convert.ToInt32(comboxId);
                CLM.DspNo = 1;
                CLM.ComboxListKey = 1;
            }
            else
            {
                CLM.DspNo += 1;
                CLM.ComboxListKey += 1;
            }
            return View("_Edit",CLM);

        }

        public JsonResult AddSave(ComboxListVM model)
        {

            try
            {
                _Service.AddSave(model);
                var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_029;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_001 });// "添加成功!"
            }
            catch (Exception)
            {
                var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_030;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_002 });// "添加失败!"
                throw;
            
              
            }
           
        }
        #endregion

        #region 修改处理
        public ActionResult Edit(long ComboxListId,bool isEdit = true)
        {
            ViewBag.IsEdit = isEdit;
            List<SelectListItem> ListCombox = _Service.GetComboxList();
            ViewBag.ListCombox = ListCombox;
            ComboxListVM CLM = _Service.GetComboxVM(ComboxListId);
            return View("_Edit", CLM);
        }

        public JsonResult EditSave(ComboxListModel model)
        {
            try
            {
                _Service.EditSave(model);
                var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_025;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_003 });// "修改成功!"
            }
            catch (Exception)
            {
                var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_026;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_004 });// "修改成功!"
                throw;
            }
           
        }
        #endregion

        #region 删除处理
        public JsonResult DeleteData(long [] ComboxListId)
        {
            try
            {
                _Service.Delete(ComboxListId);
                var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_027;
                addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.COM_MSG_DEL_SUC });// "删除成功!"
            }
            catch (Exception)
            {
                var msg = BaseRes.SPD_COL_014 + BaseRes.MGC_CTL_028;
                addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                 return Json(new { result = 1, Msg = BaseRes.COM_MSG_DEL_FAIL });// "删除成功!"
                throw;
            }
        }
        #endregion
    }

    
}
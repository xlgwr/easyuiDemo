using Valeo.Domain.Models;
using Valeo.Lang;
using Valeo.Service.ParameterSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.ParameterSetting
{
    public class CourtInfoSetController : BaseController
    {
        CourtInfoSetService _Service = new CourtInfoSetService();

        #region 查询处理
        // GET: CourtInfoSet
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "CourtInfoSet").ToList();
            return View();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="CM"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult GetCourtInfoList(CourtModel CM, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            try
            {
                var comboxMode = _Service.GetCourtPage(CM, page, rows,sort,order);
                var comboxs = comboxMode.Items;
                var comboxList = from item in comboxs
                                 select new
                                 {
                                     item.CourtID,
                                     item.CourtCode,
                                     item.Remark,
                                     item.CourtName_En,
                                     item.CourtName_Cn,
                                     item.Address,
                                     item.Type,
                                     item.Tel
                                 };
                var result = new
                {
                    total = comboxMode.TotalItems,
                    rows = comboxList
                };
                var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_024;
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
            CourtModel CM=new CourtModel();
            return View("_Edit", CM);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="CM"></param>
        /// <returns></returns>
        public JsonResult AddSave(CourtModel CM)
        {
            long result = _Service.AddSave(CM);
            if (result == 0)
            {
                return Json(new { result = 0, Msg = BaseRes.CSC_COL_002 });// "重复!"
            }
            else if (result == 1)
            {
                var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_029;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_001 });// "添加成功!"
            }
            else
            {
                var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_030;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_002 });// "添加失败!"
            }
        }
      
        
        #endregion


        #region 修改处理
        public ActionResult Edit(long CourtID, bool isEdit = true)
        {
            ViewBag.IsEdit = isEdit;
            CourtModel CM = _Service.GetSingleCourt(CourtID);
            return View("_Edit", CM);
        }

        public JsonResult EditSave(CourtModel CM)
        {
            long result = _Service.EditSave(CM);
            if (result == 0)
            {
                return Json(new { result = 0, Msg = BaseRes.CSC_COL_002 });// "重复!"
            }
            else if (result == 1)
            {
                var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_025;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.INV_BAC_003 });// "添加成功!"
            }
            else
            {
                var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_026;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.INV_BAC_004 });// "添加失败!"
            }

        }
        #endregion

        #region 删除处理
        public JsonResult DeleteData(long[] CourtID)
        {
            try
            {
                long result = _Service.Delete(CourtID);
                if (result==0)
                {
                    return Json(new { result = 0, Msg = BaseRes.OEC_COL_189 });
                }
                else if (result == 1)
                {
                    var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_028;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1, Msg = BaseRes.COM_MSG_DEL_SUC });// "删除成功!"
                }
                else
                {
                    var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_027;
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 0, Msg = BaseRes.COM_MSG_DEL_FAIL });
                }
               
            }
            catch (Exception)
            {
                var msg = BaseRes.CSC_COL_001 + BaseRes.MGC_CTL_027;
                addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.COM_MSG_DEL_FAIL });// "删除失败!"
                throw;
            }
        }
        #endregion
    }
}
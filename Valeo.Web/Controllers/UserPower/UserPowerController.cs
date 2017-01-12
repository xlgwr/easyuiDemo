using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.UserGrade;
using Valeo.Lang;
using Valeo.Service;
using Valeo.Service.UserGrade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.UserPower
{
    public class UserPowerController : BaseController
    {
        UserPowerService userPowerService = new UserPowerService();
        UserGradeService userGradeService = new UserGradeService();

        // GET: UserPower
        public ActionResult Index()
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "UserPower").ToList();

            List<UserGradeModelVM> UserGrade = userGradeService.GetUserGradeList();
            ViewBag.UserGrades = UserGrade;
            List<SysModuleVM> ModeuleList = userPowerService.GetUserPowerList(UserGrade[0].UserGradeID.ToString());
            ViewBag.ModeuleList = ModeuleList;

            return View();
        }
        public JsonResult UserPowerPage(string UserGradeID, long page = 1, long rows = 10)
        {
            List<SysModuleVM> ModeuleList = userPowerService.GetUserPowerList(UserGradeID);
            var result = new
            {
                total = ModeuleList.Count,
                rows = ModeuleList
            };
            return Json(result);
        }


        #region 【修改处理】

        public JsonResult EditSave(string UserGradeID, List<SysModuleVM> model)
        {
            try
            {
                userPowerService.EditSave(UserGradeID, model);
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }

        #endregion


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="module"></param>
        [HttpPost]
        public void ExportPower(string UserGradeID)
        {
            SysModuleVM model = new SysModuleVM();
            try
            {
                List<SysModuleVM> ModeuleList = userPowerService.GetUserPowerList(UserGradeID);
                var allUserGrade = userGradeService.GetUserGradeDic();

                var dt_temp = DataConvert.List2DataTable(ModeuleList);
                DataTable dt_UserPower = new DataTable();
                dt_UserPower.Columns.Add("Mod_nm");
                dt_UserPower.Columns.Add("Search");
                dt_UserPower.Columns.Add("Create");
                dt_UserPower.Columns.Add("Edit");
                dt_UserPower.Columns.Add("Delete");
                dt_UserPower.Columns.Add("Check");
                dt_UserPower.Columns.Add("Import");
                dt_UserPower.Columns.Add("Export");

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    DataRow dr = dt_UserPower.NewRow();
                    dr["Mod_nm"] = dt_temp.Rows[i]["Mod_nm"].ToString();
                    for (int k = 2; k < dt_temp.Columns.Count - 1; k++)
                    {
                        if (dt_temp.Rows[i][k].ToString() == "0")
                        {
                            dr[k - 1] = "×";
                        }
                        else if (dt_temp.Rows[i][k].ToString() == "1")
                        {
                            dr[k - 1] = "√";
                        }
                        else
                        {
                            dr[k - 1] = "○";
                        }
                    }
                    dt_UserPower.Rows.Add(dr);
                }
                string str_url = @"\App_Data\UserPower.xls";

                var titNmae = UserGradeID + "级别用户权限表";
                if (allUserGrade.ContainsKey(UserGradeID))
                {
                    titNmae = allUserGrade[UserGradeID] + "级别用户权限表";
                }

                new ExcelHelper().ExportExcelall(dt_UserPower, str_url, HttpContext, titNmae + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            }
            catch
            {
                Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
    }
}
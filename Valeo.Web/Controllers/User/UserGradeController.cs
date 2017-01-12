using Valeo.Domain;
using Valeo.Domain.UserGrade;
using Valeo.Lang;
using Valeo.Service.UserGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.User
{
    public class UserGradeController : BaseController
    {
        UserGradeService userGradeService = new UserGradeService();

        // GET: UserGrade
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "UserGrade").ToList();
            return View();
        }

        public JsonResult UserGradePage(UserGradeModel model,long page=1,long rows=10)
        {
            var userGrades = userGradeService.GetUserGradeList();

            var result = new
            {
                total = userGrades.Count,
                rows = userGrades
            };
            return Json(result);
        }


        public JsonResult DataGrade(DataGradeModel model, long page = 1, long rows = 10) 
        {
            var dataGrades = userGradeService.GetDataGradeList();

            var result = new
            {
                total = dataGrades.Count,
                rows = dataGrades
            };
            return Json(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="UserGradeID"></param>
        /// <returns></returns>
        public JsonResult DataGradeEdit(long? UserGradeID)
        {
            //所有的数据级别
            var dataGradesAll = userGradeService.GetDataGradeList();

            List<UserDataRelationModel> dataRelation = new List<UserDataRelationModel>();
            var dataGrades = userGradeService.GetUserGradeModel(UserGradeID, ref dataRelation);//已保存的数据级别

            List<DataGradeModelBing> dataList = new List<DataGradeModelBing>();
            
            foreach (var item in dataGradesAll)//所有的 
            {
                DataGradeModelBing temp = new DataGradeModelBing();
                temp.DataGradeID = item.DataGradeID;
                temp.DataGrade = item.DataGrade;
                temp.Remark = item.Remark;
                for (int i = 0; i < dataRelation.Count; i++)//现有的
                {
                    if (item.DataGradeID == dataRelation[i].DataGradeID) 
                    {
                        temp.Status = 1;
                        break;
                    }
                }
                dataList.Add(temp);
            }


            var result = new
            {
                total = dataList.Count,
                rows = dataList
            };
            return Json(result);
        } 

        #region 【添加用户级别】

        /// <summary>
        /// 验证级别名称
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JsonResult Verification(string UserGrade) 
        {
            if (userGradeService.IsGradeName(UserGrade))
            {
                return Json(new { result = 1, Msg = "级别名称重复!" });
            }
            else 
            {
                return Json(new { result = 0, Msg = "级别名称可用！" });
            }
        }


        public ActionResult Add(long? UserGradeID,bool edit=false)
        {
            ViewBag.isEdit = edit;
            if (edit)
            {//修改
                List<UserDataRelationModel> dataRelation = new List<UserDataRelationModel>();
                ViewBag.DataGrades = userGradeService.GetUserGradeModel(UserGradeID, ref dataRelation);
                return View("_Edit");
                
            }
            else
            {
                ViewBag.DataGrades = userGradeService.GetDataGradeList();
                return View("_Create");
            }
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddSave(long? UserGradeID, string UserGrade, int Status, string Remark, int[] dataGradeIDs) 
        {
            try
            { 
                UserGradeModel model =new UserGradeModel();
                model.UserGrade = UserGrade;
                model.Status = Status;
                model.Remark = Remark;
                //验证级别名称
                if (userGradeService.IsGradeName(model.UserGrade))
                {
                    return Json(new { result = 1, Msg = "级别名称重复!" });
                }
                if (dataGradeIDs == null) 
                {
                    return Json(new { result = 0, Msg = "请勾选数据级别！" });
                }
                model.AddUser = LoginUser.UserID;
                model.AddTime = DateTime.Now;//.ToString("yyyy-MM-dd HH:ss");
                userGradeService.Add(model, dataGradeIDs);
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });
            }
            catch
            {
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_014 });
            }
        }
        #endregion

        #region 修改用户级别

        /// <summary>
        /// 修改用户级别
        /// </summary>
        public ActionResult EditSave(long UserGradeID, string UserGrade, int Status, string Remark, int[] dataGradeIDs)
        {
            try
            {
                UserGradeModel model = new UserGradeModel();
                model.UserGrade = UserGrade;
                model.Status = Status;
                model.Remark = Remark;
                model.UserGradeID = UserGradeID;

                if (dataGradeIDs == null)
                {
                    return Json(new { result = 0, Msg = "请勾选数据级别！" });
                }
                model.AddUser = LoginUser.UserID;
                model.AddTime = DateTime.Now;//.ToString("yyyy-MM-dd HH:ss");
                model.UpdUser = LoginUser.UserID;
                model.UpdTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss");
                userGradeService.Edit(model, dataGradeIDs);
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });
            }
            catch
            {
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_014 });
            }
            
        }

        #endregion

        #region 【删除用户级别】

        public JsonResult Deletes(string[] gradeIds) 
        {
            if (gradeIds.Length > 0)
            {
                try
                {
                    userGradeService.Deletes(gradeIds);
                    return Json(new { result = 1 });
                }
                catch (Exception)
                {
                    return Json(new { result = 1, Msg = BaseRes.USE_MSG_019 });
                }
            }
            else
            {
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_019 });
            }
        }
        #endregion
    }
}
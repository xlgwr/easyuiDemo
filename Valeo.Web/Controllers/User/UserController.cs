using Valeo.Common;
using Valeo.Domain.User;
using Valeo.Domain.UserGrade;
using Valeo.Lang;
using Valeo.Service.User;
using Valeo.Service.UserGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.User
{
    public class UserController : BaseController
    {
        UserService userService = new UserService();
        UserGradeService userGradeService = new UserGradeService();

        // GET: User
        #region 【查询处理】

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllUser()
        {
            try
            {
                var jsonModel = userService.GetAllUser();
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取用户出错"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index(string userName = "")
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "User").ToList();
            var userGrades = userGradeService.GetUserGradeList();
            ViewBag.UserGrades = userGrades;
            return View();
        }

        public JsonResult UserPage(UserSearchModel condition, string sort, string order, long page = 1, long rows = 2)
        {
            var pageUserModel = userService.GetPageUsers(page, rows, sort, order, condition);
            var userGrades = userGradeService.GetUserGradeList();
            var users = pageUserModel.Items;
            var userlist = from user in users
                           select new
                           {
                               user.UserID,
                               user.UserName,
                               user.Email,
                               user.FullName_Cn,
                               user.FullName_En,
                               user.FullName_Tm,
                               user.IDDocumentNo,
                               user.IDDocumentType,
                               user.MobilePhone,
                               user.Status,
                               user.Remark,
                               user.Tel,
                               user.UserGradeID,
                               UserGradeName = userGradeService.GetUserGradeName(userGrades, user.UserGradeID)
                           };
            var result = new
            {
                total = pageUserModel.TotalItems,
                rows = userlist
            };
            var msg = "用户信息查看:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.UserInfoManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【添加用户】

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public JsonResult Verification(string userId)
        {
            if (userService.IsUserName(userId))
            {
                return Json(new { result = 1, Msg = "用户名称重复!" });
            }
            else
            {
                return Json(new { result = 0, Msg = "用户名称可用!" });
            }

        }

        public ActionResult Add()
        {
            var userGrades = userGradeService.GetUserGradeList();
            ViewBag.UserGrades = userGrades;
            return View("_Add");
        }

        public JsonResult AddSave(UserModel model)
        {
            try
            {
                userService.Add(model);
                var msg = "用户信息查看:" + "添加成功：" + model.UserID;
                addLog(0, 0, msg, VarKey.ServicePage.UserInfoManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch
            {
                var msg = "用户信息查看:" + "添加失败";
                addLog(0, 0, msg, VarKey.ServicePage.UserInfoManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_014 }); //"添加失败，用户名重复!" 
            }
        }
        #endregion

        #region 【修改用户】
        public ActionResult Edit(string userId)
        {
            var userGrades = userGradeService.GetUserGradeList();
            ViewBag.UserGrades = userGrades;
            UserModel userModel = userService.GetUserModel(userId);
            userModel.Password = Encryption.Decode(userModel.Password);
            return View("_Edit", userModel);
        }

        public JsonResult EditSave(UserModel model)
        {
            try
            {
                userService.Edit(model);
                var msg = "用户信息查看:" + "修改成功：" + model.UserID;
                addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                var msg = "用户信息查看:" + "修改失败：" + model.UserID;
                addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion



        #region【删除用户】

        public JsonResult Deletes(string[] userIds)
        {
            if (userIds.Length > 0)
            {
                try
                {
                    userService.Deletes(userIds);
                    var msg = "用户信息查看:" + "删除成功：" + userIds.ToString();
                    addLog(0, 2, msg, VarKey.ServicePage.UserInfoManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = "用户信息查看:" + "删除失败：" + userIds.ToString();
                    addLog(0, 2, msg, VarKey.ServicePage.UserInfoManager.ToString());
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
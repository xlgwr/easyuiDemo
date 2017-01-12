using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.User;
using Valeo.Lang;
using Valeo.Service;
using Valeo.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.User
{
    public class PwdResetController : BaseController
    {

        UserService userService = new UserService();

        #region 【修改用户密码】
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "PwdReset").ToList();
            UserModel userModel = new UserModel();
            userModel.UserID = LoginUser.UserID;

            return View(userModel);
        }

        [HttpPost]
        public JsonResult Index(UserModel model)
        {
            var msg = "";
            try
            {
                if (!LoginUser.UserID.Equals("admin"))
                {
                    //非管理员，无法修改别人密码
                    if (!model.UserID.Equals(LoginUser.UserID))
                    {
                        msg = "密码修改："+"系统不允许 用户:" + LoginUser.UserID + " 尝试修改：" + model.UserID + " 的密码. ";
                        addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString());
                        return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
                    }
                    else
                    {
                        msg = "修改自已的密码。";
                    }
                }
                else
                {
                    msg = "管理员 admin 修改 用户:" + model.UserID + " 的密码。";
                }

                UserModel userModel = userService.GetUserModel(model.UserID);
                userModel.Password = model.Password;

                userService.Edit(userModel);

                msg += "-->成功.";
                addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString());

                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                msg = "密码修改：" + "修改密码-->发生错误.";
                addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion
    }
}
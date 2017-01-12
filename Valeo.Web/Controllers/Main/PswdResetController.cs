using Valeo.Common;
using Valeo.Controllers;
using Valeo.Domain;
using Valeo.Domain.User;
using Valeo.Lang;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Valeo.Service.User;

namespace Valeo.Main
{
    public class PswdResetController : Base2Controller
    {
        MemberService service = new MemberService();
        private UserService userService = new UserService();
        
        #region 【查询处理】
         
        // GET: Login
        public ActionResult Index(string key)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
             
            string SecretKey = Encryption.Decode(key);
            DateTime dtExp;
            UserModel member = new UserModel();

            if (SecretKey != null)
            {
                string[] lstSecretKey = SecretKey.Split(',');

                ViewBag.Data = JsonHandler.CreateMessage(1, BaseRes.COM_MSG_UPD_SUC);

                if (lstSecretKey.Length == 3)
                {
                    if (lstSecretKey[0] == null || string.IsNullOrEmpty(lstSecretKey[0])
                        || lstSecretKey[1] == null || string.IsNullOrEmpty(lstSecretKey[1])
                        || lstSecretKey[2] == null || string.IsNullOrEmpty(lstSecretKey[2])
                        )
                    {
                        //非法链接
                        ViewBag.Data = JsonHandler.CreateMessage(0, BaseRes.COM_MSG_LINK_ERR);
                    }
                    else
                    {
                        if (DateTime.TryParse(lstSecretKey[0].ToString(), out dtExp))
                        {
                            if (dtExp.CompareTo(DateTime.Now) > 0)
                            {
                                //查询用户信息 
                                member = service.GetUserBySearchKey(key);

                                if (member == null)
                                {
                                    //密码重置链接已失效
                                    ViewBag.Data = JsonHandler.CreateMessage(0, BaseRes.LGN_MSG_014);
                                }
                            }
                            else
                            {

                                //密码重置链接超时失效 
                                ViewBag.Data = JsonHandler.CreateMessage(0, BaseRes.LGN_MSG_013);
                            }
                        }
                        else
                        {
                            //非法链接
                            ViewBag.Data = JsonHandler.CreateMessage(0, BaseRes.COM_MSG_LINK_ERR);
                        }
                    }
                }
                else
                {
                    //非法链接
                    ViewBag.Data = JsonHandler.CreateMessage(0, BaseRes.COM_MSG_LINK_ERR);
                }
            }
            else
            {
                //非法链接
                ViewBag.Data = JsonHandler.CreateMessage(0, BaseRes.COM_MSG_LINK_ERR);
            }
 
            ViewBag.Data = jss.Serialize(ViewBag.Data);
            return View(member);
        }

        #endregion

        #region 【修改处理】

        /// <summary>
        /// 编辑处理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(string userId, string newPsw1, string newPsw2)
        {


            try
            {
                //输入检查
                string message = string.Empty;
                //bool isCHKSuc = false;
                var userModel = userService.GetUserModel(userId);
                userModel.Password = newPsw1;
                userService.Edit(userModel);
                //isCHKSuc = service2.GetEditCheck(memberID, newPsw1, newPsw2, ref message);
                //if (!isCHKSuc)
                //{

                //    return Json(JsonHandler.GetJsonValue(0, message), JsonRequestBehavior.AllowGet);
                //}

                ////执行修改
                //List<MemberModel> Member = service2.GetMemberList(memberID.ToString());
                //Member[0].MemberID = memberID.ToString();
                //Member[0].Password = Encryption.Encode(newPsw1);
                //Member[0].upduser = memberID.ToString();
                //Member[0].updtime = DateTime.Now;


                //if (service2.ModifyMember(Member[0]) == 0)
                //{
                //    return Json(JsonHandler.GetJsonValue(1, BaseRes.COM_MSG_UPD_SUC), JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(JsonHandler.GetJsonValue(0, BaseRes.COM_MSG_UPD_FAIL), JsonRequestBehavior.AllowGet);
                //}
                string msg = string.Format("用户>>{0}>>密码重置成功", userId);
                addLog(0, 1, msg, VarKey.ServicePage.UserInfoManager.ToString(), userId);

                return Json(JsonHandler.GetJsonValue(1, BaseRes.COM_MSG_UPD_SUC), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }



        #endregion
    }
}
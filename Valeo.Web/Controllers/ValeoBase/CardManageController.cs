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
    public class CardManageController : BaseController
    {
        v_cardService v_cardService = new v_cardService();

        // GET: v_card
        #region 【查询处理】

        /// <summary>
        /// 获取卡板
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllv_card()
        {
            try
            {
                var jsonModel = v_cardService.GetAllv_card();
                var trans = getCombox(EnumCombox.transType);
                var josns = from v in jsonModel
                            from a in trans
                            where v.transType == a.ComboxListKey.ToString()
                            select new
                            {
                                cardNO = v.cardNO,
                                cardSize = v.cardSize,
                                transType = v.transType,
                                remark = v.remark,
                                adduser = v.adduser,
                                upduser = v.upduser,
                                addtime = v.addtime,
                                updtime = v.updtime,
                                transTypeVM = a.ComboxListName
                            };
                return Json(josns, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取卡板出错"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {

            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "CardManage").ToList();
            return View();
        }

        public JsonResult Page(v_card condition, string sort, string order, long page = 1, long rows = 2)
        {
            var pageModels = v_cardService.GetPagev_cards(page, rows, sort, order, condition);

            var trans = getCombox(EnumCombox.transType);
            var josns = from v in pageModels.Items
                        from a in trans
                        where v.transType == a.ComboxListKey.ToString()
                        select new
                        {
                            cardNO = v.cardNO,
                            cardSize = v.cardSize,
                            transType = v.transType,
                            remark = v.remark,
                            adduser = v.adduser,
                            upduser = v.upduser,
                            addtime = v.addtime,
                            updtime = v.updtime,
                            transTypeVM = a.ComboxListName
                        };

            var result = new
            {
                total = pageModels.TotalItems,
                rows = josns
            };
            var msg = "卡板设定:" + "查询成功";
            addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
            return Json(result);
        }
        #endregion

        #region 【添加卡板】

        /// <summary>
        /// 验证卡板名
        /// </summary>
        /// <param name="v_cardName"></param>
        /// <returns></returns>
        public JsonResult Verification(string id)
        {
            if (v_cardService.IscardNO(id))
            {
                return Json(new { result = 1, Msg = "卡板名称重复!" });
            }
            else
            {
                return Json(new { result = 0, Msg = "卡板名称可用!" });
            }

        }
        public JsonResult getModel(string id)
        {
            var tmpModel = v_cardService.Getv_card(id);
            if (tmpModel != null)
            {
                return Json(new { result = 1, Msg = "卡板获取成功!", value = tmpModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0, Msg = id + "：卡板不存在!", value = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Add()
        {
            ViewBag.isAdd = true;
            ViewBag.isSearch = false;

            v_card v_card = new v_card();

            return View(v_card);
        }

        public JsonResult AddSave(v_card model)
        {
            try
            {
                if (v_cardService.IscardNO(model.cardNO))
                {
                    throw new Exception("添加失败，卡板型号重复");
                }

                model.adduser = LoginUser.UserID;
                v_cardService.Add(model);
                var msg = "卡板设定:" + "添加成功：" + model.cardNO;
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch
            {
                var msg = "卡板设定:" + "添加失败";
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = "添加失败，卡板型号重复!" }); //"添加失败，卡板名重复!" 
            }
        }
        #endregion

        #region 【修改卡板】
        public ActionResult Edit(string id, int id2 = 0)
        {
            ViewBag.isAdd = false;
            var isSearch = false;
            if (id2 > 0)
            {
                isSearch = true;
            }
            ViewBag.isSearch = isSearch;
            v_card v_card = v_cardService.Getv_card(id);
            return View("Add", v_card);
        }

        public JsonResult EditSave(v_card model)
        {
            try
            {
                model.upduser = LoginUser.UserID;
                v_cardService.Edit(model);
                var msg = "卡板设定:" + "修改成功：" + model.cardNO;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch
            {
                var msg = "卡板设定:" + "修改失败：" + model.cardNO;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion



        #region【删除卡板】

        public JsonResult Deletes(string[] id)
        {
            if (id.Length > 0)
            {
                try
                {
                    v_cardService.Deletes(id);
                    var msg = string.Format("卡板设定，删除成功：{0}.", id);
                    addLog(0, 2, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = string.Format("卡板设定，删除失败：{0}.", id);
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
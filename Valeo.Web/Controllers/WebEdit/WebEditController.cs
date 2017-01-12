using Valeo.Common;
using Valeo.Domain.ManageCenter.WebEdit;
using Valeo.Lang;
using Valeo.Service.ManageCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.WebEdit
{
    public class WebEditController : BaseController
    {
        WebEditService _WEService = new WebEditService();
        #region 页面初始化
        // GET: WebEdit
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "WebEdit").ToList();
            //获取语言KEY
            List<string> langList = new List<string>();
            string LangValue = System.Configuration.ConfigurationManager.AppSettings["Valeo.LangKey"].ToString();
            if (!string.IsNullOrEmpty(LangValue))
            {
                string[] LangValues = LangValue.Split(new char[] { ',' });
                for (int i = 0; i < LangValues.Length; i++)
                {
                    langList.Add(LangValues[i]);
                }
            }
            List<SelectListItem> itemLang = new List<SelectListItem>();
            itemLang = _WEService.GetPageAllLang(langList);
            //获取界面ID内容
            List<SelectListItem> item = new List<SelectListItem>();
            item = _WEService.GetPageAllTitle();




            ViewBag.itemLang = itemLang;
            ViewBag.selectItem = item;
            return View();
        }
        
        #endregion

        #region 查询操作
        public JsonResult GetHtmlData(HtmlDataModel htmls)
        {
            string message = " ";
            try
            {
                List<HtmlDataModel> ListHtml = new List<HtmlDataModel>();
                ListHtml = _WEService.GetHtml(htmls);
                var msg = BaseRes.WBF_COL_007 + BaseRes.MGC_CTL_024;
                addLog(0, 3, msg, VarKey.ServicePage.ParamManager.ToString());
                if (ListHtml.Count > 0)
                {
                    for (int i = 0; i < ListHtml.Count; i++)
                    {
                        HtmlDataModel htmlModel = ListHtml[i];
                        var htmlJson = new
                        {
                            type = 1,
                            content = htmlModel.HtmlContent
                        };
                        return Json(htmlJson);
                    }
                    return Json(JsonHandler.CreateMessage(0, message));
                }
                else
                {
                    var htmlJson = new
                    {
                        type = 1,
                        content = BaseRes.WBF_COL_001
                    };
                    return Json(htmlJson);
                }
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, message));
                throw;
            }
           
           

        }
        #endregion

        #region 新增或修改操作
         [ValidateInput(false)]
        public JsonResult AddWebData(HtmlDataModel html)
        {
            string message = " ";
            try
            {
                string htmlBody = HttpUtility.UrlDecode(html.HtmlContent);
                string memberName = ViewBag.Uname;
                if (_WEService.AddOrEditHtml(html, htmlBody, memberName, ref message))
                {
                    var htmlJson = new
                    {
                        type = 1,
                        message = message
                    };
                    var msg = BaseRes.WBF_COL_007 + BaseRes.MGC_CTL_025;
                    addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(htmlJson);
                }
                else
                {
                    var msg = BaseRes.WBF_COL_007 + BaseRes.MGC_CTL_026;
                    addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(JsonHandler.CreateMessage(0, message));
                }
            }
            catch (Exception)
            {
                var msg = BaseRes.WBF_COL_007 + BaseRes.MGC_CTL_026;
                addLog(0, 1, msg, VarKey.ServicePage.ParamManager.ToString());
                return Json(JsonHandler.CreateMessage(0, message));
                throw;
            }
            
            
        }
        #endregion
    }
}
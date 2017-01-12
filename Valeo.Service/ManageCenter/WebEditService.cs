using Valeo.Domain.ManageCenter.WebEdit;
using Valeo.Lang;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace Valeo.Service.ManageCenter
{
    public class WebEditService:BaseService
    {
        Sql _sql;

        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="html"></param>
        /// <param name="htmlBody"></param>
        /// <param name="memberName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddOrEditHtml(HtmlDataModel html, string htmlBody, string memberName, ref string message)
        {

            if (!string.IsNullOrEmpty(html.FuncPrentId))
            {
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        HtmlDataModel htmlData = new HtmlDataModel();
                        _sql = new Sql();
                        _sql.Append(@"SELECT * FROM m_HtmlData where FuncPrentId=@0 AND LangKey=@1", html.FuncPrentId, html.LangKey);
                         var result = db.Fetch<HtmlDataModel>(_sql);
                         if (result.Count > 0)
                         {
                            //修改 
                             htmlData.FuncPrentId = result[0].FuncPrentId;
                             htmlData.LangKey = result[0].LangKey;
                             htmlData.Remark = result[0].Remark;
                             htmlData.AddUser = result[0].AddUser;
                             htmlData.AddTime = result[0].AddTime;
                             htmlData.HtmlContent = htmlBody;
                             htmlData.UpdTime = DateTime.Now;
                             htmlData.UpdUser = memberName;
                             db.Update("m_HtmlData", "RecdId", htmlData, result[0].RecdId);
                             message =  BaseRes.DLL_MSG_007;
                         }
                         else
                         {
                             //新增
                             htmlData.FuncPrentId = html.FuncPrentId;
                             htmlData.LangKey = html.LangKey;
                             htmlData.HtmlContent = htmlBody;
                             htmlData.Remark = html.Remark;
                             htmlData.AddUser = memberName;
                             htmlData.AddTime = DateTime.Now;
                             db.Insert("m_HtmlData", "RecdId", htmlData);
                             message = BaseRes.DLL_MSG_005;
                             
                         }
                         scope.Complete();
                        
                    }
                    catch (Exception ex)

                    {
                        message = ex.Message;
                        return false;
                        throw;
                    }
                } 
            }
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public List<HtmlDataModel> GetHtml(HtmlDataModel html)
        {
             List<HtmlDataModel> htmlDate = new List<HtmlDataModel>();
             if (!string.IsNullOrEmpty(html.LangKey) && !string.IsNullOrEmpty(html.FuncPrentId))
             {
                 try
                 {
                     using (var scope = db.GetTransaction())
                     {
                         _sql = new Sql();
                         _sql.Append(@"SELECT * FROM m_HtmlData where FuncPrentId=@0 AND LangKey=@1", html.FuncPrentId, html.LangKey);
                         htmlDate = db.Fetch<HtmlDataModel>(_sql);
                         scope.Complete();

                     }

                 }
                 catch (Exception)
                 {
                     throw;
                 }
             }

             return htmlDate;
        }
       
        /// <summary>
        /// 选中所有标题的下拉框
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPageAllTitle()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_008, Value = VarKey.PageTitle.Home.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_009, Value = VarKey.PageTitle.About.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_010, Value = VarKey.PageTitle.Srevice.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_011, Value = VarKey.PageTitle.Member.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_012, Value = VarKey.PageTitle.Payment.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_004, Value = VarKey.PageTitle.UserLimit.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_005, Value = VarKey.PageTitle.PrivatyPolicy.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_006, Value = VarKey.PageTitle.Disclaimer.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_013, Value = VarKey.PageTitle.RegistrationAgreement.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_014, Value = VarKey.PageTitle.OnlineCourt.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_015, Value = VarKey.PageTitle.OnlineLand.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_016, Value = VarKey.PageTitle.OnlineCompany.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_017, Value = VarKey.PageTitle.OnlineLoan.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_018, Value = VarKey.PageTitle.OnlinePublic.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_019, Value = VarKey.PageTitle.OnlineAuto.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_020, Value = VarKey.PageTitle.OfflineCourt.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_021, Value = VarKey.PageTitle.OfflineCompany.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_022, Value = VarKey.PageTitle.OfflineLand.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_023, Value = VarKey.PageTitle.OfflineOther.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_024, Value = VarKey.PageTitle.ContactAs.ToString() });
            return selectItems;
        }

        public List<SelectListItem> GetPageAllLang(List<string> langList)
        {
            var selectItems = new List<SelectListItem>();
            for (int i = 0; i < langList.Count; i++)
            {
                if (langList[i].Equals(VarKey.PageLang.zhCN))
                {
                    selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_001, Value = VarKey.PageLang.zhCN.ToString() });
                }
                else if (langList[i].Equals(VarKey.PageLang.zhTW))
                {
                    selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_002, Value = VarKey.PageLang.zhTW.ToString() });
                }
                else if (langList[i].Equals(VarKey.PageLang.enUS))
                {
                    selectItems.Add(new SelectListItem { Text = BaseRes.MEU_CTL_003, Value = VarKey.PageLang.enUS.ToString() });
                }
            }
            return selectItems;
        }
    }
}

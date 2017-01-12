using App.Lang;
using Valeo.Domain;
using Valeo.Domain.Combox;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Valeo.Service.ParameterSetting
{
   public class SetPullDownService:BaseService
   {
       #region 查询处理
       /// <summary>
       /// 获取All下拉项目
       /// </summary>
       /// <returns></returns>
       public List<ComboxModel> GetALLCombox()
       {
           var comboxList = new List<ComboxModel>();
           try
           {
               comboxList = db.Fetch<ComboxModel>("SELECT * from m_Combox ORDER BY DspNo ASC");
           }
           catch (Exception)
           {
               
               throw;
           }
           return comboxList;
       }

       public ComboxModel GetComboxId(string ComboxName)
       {
           Sql sql = new Sql();
           sql.Append(@"SELECT * from m_Combox where ComboxName=@0", ComboxName);
           return db.FirstOrDefault<ComboxModel>(sql);
       }

       public ComboxListVM getMaxComboxList(string ComboxId)
       {
           Sql sql = new Sql();
           sql.Append(@"SELECT m_ComboxList.ComboxId,
           ComboxListKey,
           m_ComboxList.DspNo,ComboxName  from m_ComboxList INNER JOIN m_Combox on m_Combox.ComboxId=m_ComboxList.ComboxId
           where m_Combox.ComboxId=@0 ORDER BY m_ComboxList.ComboxListId DESC", ComboxId);
           return db.FirstOrDefault<ComboxListVM>(sql);
       }

       /// <summary>
       /// 分页获取下拉内容
       /// </summary>
       /// <param name="comboxId"></param>
       /// <param name="page"></param>
       /// <param name="rows"></param>
       /// <returns></returns>
       public Page<ComboxListVM> GetAllComBoxList(string ComboxId, long page, long rows, string sort, string order)
       {
           var comboxList = new Page<ComboxListVM>();
           try
           {
               Sql sql = new Sql().Append(@"SELECT m_Combox.ComboxId,m_Combox.ComboxName, 
                m_ComboxList.ComboxListId,m_ComboxList.ComboxListName,m_ComboxList.ComboxListKey,
                m_ComboxList.LanguageCode,m_ComboxList.DspNo,m_ComboxList.Remark
                from m_ComboxList  
                LEFT JOIN m_Combox on m_Combox.ComboxId=m_ComboxList.ComboxId 
                where m_Combox.ComboxId=@0  ", ComboxId);
               if (!string.IsNullOrEmpty(sort))
               {
                   if (sort.Equals("DspNo"))
                   {
                       sort = "m_ComboxList.DspNo";
                   }
                   if (sort.Equals("LanguageCode"))
                   {
                       sort = "m_ComboxList.LanguageCode";
                   }
                   if (sort.Equals("Remark"))
                   {
                       sort = "m_ComboxList.Remark";
                   }
                   sql.OrderBy(sort + " " + order);
               }
               else
               {
                   sql.OrderBy(" m_ComboxList.DspNo ASC");
               }
               return db.Page<ComboxListVM>(page, rows, sql);
               ///comboxList = db.Fetch<ComboxListModel>("SELECT * from m_ComboxList where ComboxId=@0 ORDER BY DspNo ASC");
           }
           catch (Exception)
           {

               throw;
           }
           
       }

       /// <summary>
       /// 获取下拉
       /// </summary>
       /// <returns></returns>
       public List<SelectListItem> GetComboxList()
       {
           List<SelectListItem> selectItems = new List<SelectListItem>();
           try
           {
               Sql sql = new Sql().Append("SELECT * from m_Combox ORDER BY DspNo ASC");
               List<ComboxModel> ListCountry = db.Fetch<ComboxModel>(sql);

               foreach (ComboxModel item in ListCountry)
               {
                   selectItems.Add(new SelectListItem { Text = COMKey.COM(item.LanguageCode), Value = item.ComboxId.ToString() });
               }
           }
           catch (Exception)
           {
               throw;
           }
           return selectItems;
       }

       /// <summary>
       /// 获取combox+comboxList
       /// </summary>
       /// <param name="ComboxListId"></param>
       /// <returns></returns>
       public ComboxListVM GetComboxVM(long ComboxListId)
       {
           try
           {
                Sql sql=new Sql();
                sql.Append(@"SELECT m_Combox.ComboxId,m_Combox.ComboxName, 
                m_ComboxList.ComboxListId,m_ComboxList.ComboxListName,m_ComboxList.ComboxListKey,
                m_ComboxList.LanguageCode,m_ComboxList.DspNo,m_ComboxList.Remark
                from m_ComboxList  
                LEFT JOIN m_Combox on m_Combox.ComboxId=m_ComboxList.ComboxId 
                where m_ComboxList.ComboxListId=@0",ComboxListId);
                return db.Single<ComboxListVM>(sql);
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 获取comboxList
       /// </summary>
       /// <param name="ComboxListId"></param>
       /// <returns></returns>
       public ComboxListModel GetComBoxList(long ComboxListId)
       {
           try
           {
                Sql sql=new Sql();
                sql.Append(@"Select * from m_ComboxList 
                where ComboxListId=@0",ComboxListId);
                return db.Single<ComboxListModel>(sql);
           }
           catch (Exception)
           {
               
               throw;
           }
       }
       #endregion

       #region 新增处理
       public void AddSave(ComboxListVM comboxListVM)
       {
           ComboxListModel CLM = new ComboxListModel();
           CLM.ComboxId = comboxListVM.ComboxId;
           CLM.ComboxListKey = comboxListVM.ComboxListKey;
           CLM.ComboxListName = comboxListVM.ComboxListName;
           CLM.LanguageCode = comboxListVM.LanguageCode;
           CLM.Remark = comboxListVM.Remark;
           CLM.DspNo = comboxListVM.DspNo;
           using (var scope = db.GetTransaction())
           {
               try
               {
                   db.Insert("m_ComboxList", "ComboxListId", CLM);
                   scope.Complete();
               }
               catch (Exception)
               {
                   
                   throw;
               }
           }
       }

       #endregion

       #region 修改处理
       public void EditSave(ComboxListModel CLM)
       {

           var oldModel = GetComBoxList(CLM.ComboxListId);
           oldModel.ComboxId = CLM.ComboxId;
           oldModel.ComboxListName = CLM.ComboxListName;
           oldModel.ComboxListKey = CLM.ComboxListKey;
           oldModel.DspNo = CLM.DspNo;
           oldModel.LanguageCode = CLM.LanguageCode;
           oldModel.Remark = CLM.Remark;
           using (var scope = db.GetTransaction())
           {
               try
               {
                   db.Update(oldModel);
                   scope.Complete();
               }
               catch (Exception)
               {
                   
                   throw;
               }
               
           }
          
       }
       #endregion

       #region 删除处理
       public void Delete(long[] ComboxListId)
       {
           using (var scope = db.GetTransaction())
           {
               try
               {
                   foreach (var item in ComboxListId)
                   {
                       db.Delete("m_ComboxList", "ComboxListId", null, item);
                   }
                   scope.Complete();
               }
               catch (Exception)
               {

                   throw;
               }
           }
           
           
       }
       #endregion
   }
}

using App.Lang;
using Valeo.Common;
using Valeo.Domain.Models;
using Valeo.Domain.ParameterSetting;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Valeo.Service.ParameterSetting
{
    public class PubTypeService : BaseService
    {
        #region  查询处理

        /// <summary>
        /// 获取所有的公共分类
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Page<PublicTypeModel> GetPubTypeList(long page, long rows, string sort, string order)
        {
            var sql = new Sql();
            sql.Append(@"select * from m_PublicType");
            if (!string.IsNullOrEmpty(sort))
            {
                
                sql.OrderBy(sort + " " + order);
            }
            return db.Page<PublicTypeModel>(page, rows, sql);
        }

        /// <summary>
        /// 修改查询OLD数据
        /// </summary>
        /// <param name="PublicTypeID"></param>
        /// <returns></returns>
        public PublicTypeModel GetSinglePubType(long PublicTypeID)
        {
            Sql sql = new Sql().Append(@"select * from m_PublicType where PublicTypeID=@0", PublicTypeID);
            return db.Single<PublicTypeModel>(sql);
        }

        /// <summary>
        /// 获取所有的表名
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetTableList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            Sql sql = new Sql().Append(@"SELECT TableID,LanguageCode from m_Table");
            List<TableModel> list = db.Fetch<TableModel>(sql);
            foreach (TableModel item in list)
            {
                selectItems.Add(new SelectListItem { Text = COMKey.COM(item.LanguageCode), Value = item.TableID.ToString() });
            }
            return selectItems;
        }

        /// <summary>
        /// 获取选中的表ID
        /// </summary>
        /// <returns></returns>
        public List<PublicTableModel> GetSelectTableList(long PublicTypeID)
        {
            Sql sql = new Sql().Append(@"SELECT PublicTableID,TableId,PublicTypeID from m_PublicTable WHERE PublicTypeID=@0", PublicTypeID);
            List<PublicTableModel> list = db.Fetch<PublicTableModel>(sql);
            return list;
        }
        #endregion

        #region 新增处理
        public long AddSave(PubTypeVM PM)
        {
            var result = db.Fetch<CaseTypeModel>(string.Format(@"select * from m_PublicType where TypeName='{0}'", PM.TypeName));
            if (result.Count > 0)
            {
                return 0;
            }
            else
            {
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        PublicTypeModel PTM = new PublicTypeModel();
                        PTM.TypeName = PM.TypeName;
                        PTM.LanguageCode = PM.LanguageCode;
                        long PublicTypeID =DataConvert.Value2long(db.Insert("m_PublicType", "PublicTypeID", PTM));
                       
                        if (PM.TableIdS != null)
                        {
                            var TabidS = PM.TableIdS;
                            for (int i = 0; i < TabidS.Length; i++)
                            {
                                PublicTableModel Tab = new PublicTableModel();
                                Tab.PublicTypeID = PublicTypeID;
                                Tab.TableId = TabidS[i];
                                db.Insert(Tab); 
                            }
                        }
                        scope.Complete();
                        return 1;

                       
                    }
                    catch (Exception)
                    {
                        return -1;
                        throw;
                    }
                }

            }
        }

        #endregion


        #region 修改处理
        public long EditSave(PubTypeVM PM)
        {
            var result = db.Fetch<PublicTypeModel>(string.Format(@"select * from m_PublicType where TypeName='{0}'", PM.TypeName));
            if (result.Count > 0 && result[0].PublicTypeID != PM.PublicTypeID)
            {
                return 0;
            }
            else
            {
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        PublicTypeModel PTM = new PublicTypeModel();
                        PTM.PublicTypeID = PM.PublicTypeID;
                        PTM.TypeName = PM.TypeName;
                        PTM.LanguageCode = PM.LanguageCode;
                        db.Update(PTM);

                        List<PublicTableModel> Table = GetSelectTableList(PM.PublicTypeID);
                        List<long> allTable = new List<long>();
                        
                        if (PM.TableIdS != null)
                        {
                            foreach (var item in PM.TableIdS)
                            {
                                allTable.Add(item);
                            }
                            //if (Table!=null)
                            //{
                            //    foreach (var item in Table)
                            //    {
                            //         allTable.Add(item.TableId);
                            //    }

                            //    for (int i = 0; i < allTable.Count; i++)
                            //    {
                            //        for (int j = allTable.Count-1; j >i; j--)
                            //        {
                            //            if (allTable[i]==allTable[j])
                            //            {
                            //                allTable.RemoveAt(j);
                            //            }
                            //        }
                            //    }
                            //}


                            foreach (var item in PM.TableIdS)
                            {

                                var isItem = Table.FirstOrDefault(o => o.TableId == item);
                                PublicTableModel Tab = new PublicTableModel();
                                Tab.PublicTypeID = PM.PublicTypeID;
                                Tab.TableId = item;


                                if (isItem == null)
                                {
                                    db.Insert(Tab);
                                }
                                else
                                {
                                    db.Update(Tab);
                                }
                            }
                            bool isTrue = true;
                            foreach (var item in Table)
                            {
                                for (int i = 0; i < allTable.Count; i++)
                                {
                                    if (item.TableId == allTable[i])
                                    {
                                        isTrue = false;
                                    }
                                }
                                if (isTrue)
                                {
                                    var a = item.TableId;
                                    db.Delete(new PublicTableModel { PublicTableID = item.PublicTableID });
                                   
                                }
                                isTrue = true;
                            }
                           // foreach (var item in from item in allTable let isItem = Table.FirstOrDefault(o => o.TableId == item) where isItem == null select item)
                           //{
                           //    db.Delete(new PublicTableModel { TableId = item });
                           //}
                            
                            

                        }
                        else
                        {
                            //删除所有记录
                            db.Delete("m_PublicTable", "PublicTypeID", null, PM.PublicTypeID);
                        }
                        scope.Complete();
                        return 1;
                    }
                    catch (Exception)
                    {
                        return -1;
                        throw;
                    }
                }
            }
           
        }
        #endregion


        #region 删除处理
        public long Delete(long[] PublicTypeIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in PublicTypeIDs)
                    {
                        var CM = db.Fetch<PublicTableModel>(string.Format(@"
                            SELECT PublicTypeID from m_PublicTable where PublicTypeID={0}", item));

                        if (CM.Count > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            db.Delete("m_PublicType", "PublicTypeID", null, item);
                            
                        }
                    }
                    

                    scope.Complete();
                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                    throw;
                }
            }
        }
        #endregion
    }
       
}

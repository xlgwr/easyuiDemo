using Valeo.Domain.Models;
using Valeo.Domain.ParameterSetting;
using Valeo.Lang;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Valeo.Service.ParameterSetting
{
    public class CaseTypeService:BaseService
    {
       
      
        #region 查询处理

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="CM"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Page<CaseTypeVM> GetCourtPage(CaseTypeModel CM, long page, long rows, string sort, string order)
        {
            try
            {
                Sql sql = new Sql().Append(@"SELECT CaseTypeID,CaseType,CaseType_En,CaseType_Cn
                ,CourtCode,m_CaseType.CourtID,Tykpe FROM m_CaseType LEFT JOIN m_Court on m_Court.CourtID=m_CaseType.CourtID");
                if (!string.IsNullOrEmpty(CM.CaseType))
                {
                    sql.Where("CaseType=@0", CM.CaseType);
                }
                if (!string.IsNullOrEmpty(CM.CaseType_En))
                {
                    sql.Where("CaseType_En LIKE @0",  CM.CaseType_En + "%");
                }
                if (!string.IsNullOrEmpty(CM.CaseType_Cn))
                {
                    sql.Where("CaseType_Cn LIKE @0",  CM.CaseType_Cn + "%");
                }
                if (CM.CourtID!=-1)
                {
                    sql.Where("m_CaseType.CourtID = @0", CM.CourtID);
                }
                if (CM.Tykpe!=-1)
                {
                    sql.Where("Tykpe = @0", CM.Tykpe);
                }
                if (!string.IsNullOrWhiteSpace(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }

                return db.Page<CaseTypeVM>(page, rows, sql);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取法院下拉框
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCourtList(long type)
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT CourtID,CourtCode from m_Court ");
                List<CourtModel> ListCourt= db.Fetch<CourtModel>(sql);
                if (type==0)
                {
                    selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = "-1" });
                }
                foreach (CourtModel item in ListCourt)
                {
                    selectItems.Add(new SelectListItem { Text = item.CourtCode, Value = item.CourtID.ToString() });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }

        /// <summary>
        /// 获取单一CaseType
        /// </summary>
        /// <param name="CaseTypeID"></param>
        /// <returns></returns>
        public CaseTypeModel GetSingleCaseType(long CaseTypeID)
        {
            Sql sql = new Sql().Append(@"SELECT * from m_CaseType  where CaseTypeID=@0", CaseTypeID);
            return db.Single<CaseTypeModel>(sql);
        }

        /// <summary>
        /// 获取单一CaseTypeVM 
        /// </summary>
        /// <param name="CaseTypeID"></param>
        /// <returns></returns>
        public CaseTypeVM GetSingleCaseTypeVM(long CaseTypeID)
        {
            Sql sql = new Sql().Append(@"SELECT CaseTypeID,CaseType,CaseType_En,CaseType_Cn
                ,CourtCode,m_CaseType.CourtID,Tykpe FROM m_CaseType LEFT JOIN m_Court on m_Court.CourtID=m_CaseType.CourtID  where CaseTypeID=@0", CaseTypeID);
            return db.Single<CaseTypeVM>(sql);
        }
        #endregion

        #region 新增处理
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="CTM"></param>
        /// <returns></returns>
        public long AddSave(CaseTypeModel CTM)
        {
            var result = db.Fetch<CaseTypeModel>(string.Format(@"SELECT * FROM m_CaseType WHERE CaseType='{0}'", CTM.CaseType));
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
                        db.Insert(CTM);
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

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="CPM"></param>
        public long EditSave(CaseTypeModel CTM)
        {
            var result = db.Fetch<CaseTypeModel>(string.Format(@"SELECT * FROM m_CaseType WHERE CaseType='{0}'", CTM.CaseType));
            if (result.Count > 0 && result[0].CaseTypeID != CTM.CaseTypeID)
            {
                return 0;
            }
            else
            {
                var oldModel = GetSingleCaseType(CTM.CaseTypeID);
                oldModel.CaseType = CTM.CaseType;
                oldModel.CaseType_En = CTM.CaseType_En;
                oldModel.CaseType_Cn = CTM.CaseType_Cn;
                oldModel.CourtID = CTM.CourtID;
                oldModel.Tykpe = CTM.Tykpe;
                
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        db.Update(oldModel);
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
        public long Delete(long[] CaseTypeIDs)
        {
            using (var scope = db.GetTransaction())
            {

                try
                {
                    foreach (var item in CaseTypeIDs)
                    {
                        var CM = db.Fetch<CaseModel>(string.Format(@"
                            SELECT CaseTypeID from t_Case where CaseTypeID={0}", item));

                        if (CM.Count > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            db.Delete("m_CaseType", "CaseTypeID", null, item);
                            
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

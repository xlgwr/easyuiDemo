using Valeo.Domain.Models;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service.ParameterSetting
{
    public class CourtInfoSetService:BaseService
    {

        public Page<CourtModel> GetCourtPage(CourtModel CM, long page, long rows, string sort , string order)
        {
            try
            {
                Sql sql = new Sql().Append("select CourtID,CourtName_En,CourtName_Cn,CourtCode,Address,Tel,Remark,Type from m_Court");
                if (!string.IsNullOrEmpty(CM.CourtCode))
                {
                    sql.Where("CourtCode =@0",  CM.CourtCode );
                }
                if (!string.IsNullOrEmpty(CM.CourtName_En))
                {
                    sql.Where("CourtName_En LIKE @0",  CM.CourtName_En + "%");
                }
                if (!string.IsNullOrEmpty(CM.CourtName_Cn))
                {
                    sql.Where("CourtName_Cn LIKE @0",  CM.CourtName_Cn + "%");
                }
                if (!string.IsNullOrEmpty(CM.Address))
                {
                    sql.Where("Address LIKE @0",  CM.Address + "%");
                }
                if (!string.IsNullOrEmpty(CM.Tel))
                {
                    sql.Where("Tel = @0", CM.Tel);
                }
                if (CM.Type!=0)
                {
                    sql.Where("Type = @0", CM.Type);
                }
                if (!string.IsNullOrWhiteSpace(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy("CourtID DESC");
                }
                
                return db.Page<CourtModel>(page, rows, sql);    
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public CourtModel GetSingleCourt(long CourtID)
        {
            Sql sql = new Sql().Append(@"SELECT * from m_Court  where CourtID=@0", CourtID);
          
            return db.Single<CourtModel>(sql);
        }


        public long AddSave(CourtModel CM)
        {
            var result = db.Fetch<CourtModel>(string.Format(@"SELECT * from m_Court where CourtCode='{0}'", CM.CourtCode));
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
                        db.Insert(CM);
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


        #region 修改处理

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="CPM"></param>
        public long EditSave(CourtModel CM)
        {
            var result = db.Fetch<CourtModel>(string.Format(@"SELECT * from m_Court where CourtCode='{0}'", CM.CourtCode));
            if (result.Count > 0 && !result[0].CourtCode.Equals(CM.CourtCode))
            {
                return 0;
            }
            else
            {
                var oldModel = GetSingleCourt(CM.CourtID);
                oldModel.CourtCode = CM.CourtCode;
                oldModel.CourtName_En = CM.CourtName_En;
                oldModel.CourtName_Cn = CM.CourtName_Cn;
                oldModel.Address = CM.Address;
                oldModel.Tel = CM.Tel;
                oldModel.Remark = CM.Remark;
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
        public long Delete(long[] CourtIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in CourtIDs)
                    {
                        var CM = db.Fetch<CaseModel>(string.Format(@"
                            SELECT CourtID from t_Case where CourtID={0}", item));
                        var CTM = db.Fetch<CaseTypeModel>(string.Format(@"
                            SELECT CourtID from m_CaseType where CourtID={0}", item));
                        if (CM.Count > 0 || CTM.Count > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            db.Delete("m_Court", "CourtID", null, item);
                            
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

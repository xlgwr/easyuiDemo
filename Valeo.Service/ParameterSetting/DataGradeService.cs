using Valeo.Domain;
using PetaPoco;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    public class DataGradeService : BaseService
    {
        /// <summary>
        /// 查询分页操作记录表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<DataGradeModel> GetListPages(long page, long rows, string sort, string order, DataGradeModel condition)
        {
            List<Object> listPara = new List<object>();

            var sql = new Sql().Append(@"select * from dbo.m_DataGrade");

            if (!string.IsNullOrEmpty(sort))
            {
                sql.OrderBy(sort + " " + order);
            }

            return db.Page<DataGradeModel>(page, rows, sql);
        }

        public void Add(DataGradeModel model)
        {
            var maxId = db.FirstOrDefault<long>("select ISNULL(max(DataGradeID), 0)+1 from dbo.m_DataGrade;");
            model.DataGradeID = maxId;
            db.Insert("m_DataGrade", "DataGradeID", false, model);
        }
        public void Edit(DataGradeModel model)
        {
            db.Update("m_DataGrade", "DataGradeID", model);
        }
        public void Deletes(long dataGradeIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {

                    db.Delete("m_DataGrade", "DataGradeID", new DataGradeModel() { DataGradeID = dataGradeIDs });

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Valeo.Domain;
using PetaPoco;
using Valeo.Common;


namespace Valeo.Service
{
    public class ShortNameService : BaseService
    {
        /// <summary>
        /// 查询分页操作记录表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<ShortNameModelVM> GetListPages(long page, long rows, string sort, string order, ShortNameModel condition)
        {
            List<Object> listPara = new List<object>();

            var sql = new Sql().Append(@"select *, ");
            //类别(0:名称/1:地址)
            sql.Append(@" (CASE Type WHEN 0 THEN N'名称' WHEN 1 THEN '地址' ELSE N'其它' END) as TypeVM ");
            sql.Append(@" from m_ShortName ");

            if (condition.Type > -1)
            {
                sql.Where(" Type = @0  ", condition.Type);
            }

            if (!string.IsNullOrEmpty(condition.ShortName))
            {
                sql.Where(" ShortName  like  @0  ",  condition.ShortName + "%");
            }

            if (!string.IsNullOrEmpty(condition.LongName))
            {
                sql.Where(" LongName  like  @0  ",  condition.LongName + "%");
            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToLower().Equals("typevm"))
                {
                    sql.OrderBy(" Type " + order);
                }
                else
                {
                    sql.OrderBy(sort + " " + order);
                }
            }

            return db.Page<ShortNameModelVM>(page, rows, sql);
        }

        public ShortNameModel GetModel(long id)
        {
            var userModel = db.FirstOrDefault<ShortNameModel>("select  * from m_ShortName where Shortid=@0", id);

            return userModel;

        }
        public ShortNameModel GetModel(ShortNameModel id)
        {
            var userModel = db.FirstOrDefault<ShortNameModel>("select  * from m_ShortName where ShortName=@0 and Type=@1", id.ShortName, id.Type);

            return userModel;

        }
        public void Add(ShortNameModel model)
        {
            db.Insert(model);
        }
        public void Edit(ShortNameModel model)
        {
            db.Update(model);
        }
        public void Delete(long id)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Delete(new ShortNameModel() { Shortid = id });
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

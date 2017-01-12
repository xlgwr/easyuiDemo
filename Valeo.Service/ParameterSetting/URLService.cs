using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.User;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    /// <summary>
    /// 采集网址查看
    /// </summary>
    public class URLService : BaseService
    {
        /// <summary>
        /// 采集网址查看
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<URLModel> GetListPages(long page, long rows, string sort, string order, URLModel condition)
        {
            List<Object> listPara = new List<object>();

            var sql = new Sql().Append(@"select * from m_URL m ");

            //用户名称
            if (!string.IsNullOrEmpty(condition.URLname))
            {
                sql.Where(" m.URLname  like  @0  ",  condition.URLname + "%");

            }
            //用户级别
            if (!string.IsNullOrEmpty(condition.URL))
            {
                sql.Where(" m.URL  like  @0  ",  condition.URL + "%");

            }


            if (!string.IsNullOrEmpty(sort))
            {
                sql.OrderBy(sort + " " + order);
            }
            else
            {
                sql.OrderBy(" m.URLid ");
            }

            return db.Page<URLModel>(page, rows, sql);
        }

        public URLModel GetModel(long id)
        {
            var userModel = db.FirstOrDefault<URLModel>("select  * from dbo.m_URL where URLid=@0", id);

            return userModel;

        }
        public void Add(URLModel model)
        {
            db.Insert(model);
        }
        public void Edit(URLModel model)
        {
            db.Update(model);
        }
        public void Delete(long id)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {

                    db.Delete(new URLModel() {  URLid = id });

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

using Valeo.Common;
using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Valeo.Domain.Valeo;

namespace Valeo.Service
{
    public class v_customer_docService : BaseService
    {

        /// <summary>
        /// 查询分页客户文档表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_customer_doc> GetPagev_customer_docs(long page, long rows, string sort, string order, v_customer_doc condition)
        {

            try
            {


                var sql = new Sql().Append(@"SELECT * from v_customer_doc ");

                genSqlWhere(ref sql, condition.docID, "docID", 0);
                genSqlWhere(ref sql, condition.customerNo, "customerNo", 0);
                genSqlWhere(ref sql, condition.docName, "docName", 2);

                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" customerNo DESC,sortNo ASC ");
                }

                return db.Page<v_customer_doc>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有客户文档
        /// </summary>
        /// <returns></returns>
        public List<v_customer_doc> GetAllv_customer_doc(string customerNo = "")
        {

            try
            {
                if (string.IsNullOrEmpty(customerNo))
                {
                    return db.Fetch<v_customer_doc>("select * from v_customer_doc order by customerNo DESC,sortNo ASC");
                }
                else
                {
                    return db.Fetch<v_customer_doc>("select * from v_customer_doc where customerNo=@0 order by sortNo ASC", customerNo);
                }

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到客户文档
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        public v_customer_doc Getv_customer_doc(string docID)
        {

            try
            {
                return db.FirstOrDefault<v_customer_doc>("SELECT * from v_customer_doc WHERE docID=@0 order by customerNo DESC,sortNo ASC", docID);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个客户文档
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        public bool IsdocID(string docID)
        {

            try
            {

                var v_customer_doc = db.FirstOrDefault<v_customer_doc>(@"SELECT * from v_customer_doc where docID=@0", docID);
                if (v_customer_doc == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        public void Add(v_customer_doc model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.addtime = DateTime.Now;
                    db.Insert(model);


                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Edit(v_customer_doc model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "customerNo","docName","sortNo",
                        "remark", "upduser", "updtime" });

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(double[] docIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in docIDs)
                    {
                        db.Delete(new v_customer_doc() { docID = item });
                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }

        }
    }
}

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
    public class v_customerService : BaseService
    {

        /// <summary>
        /// 查询分页客户表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_customer> GetPagev_customers(long page, long rows, string sort, string order, v_customer condition)
        {

            try
            {


                var sql = new Sql().Append(@"SELECT * from v_customer ");

                genSqlWhere(ref sql, condition.customerNo, "customerNo", 0);
                genSqlWhere(ref sql, condition.customerName, "customerName", 2);
                genSqlWhere(ref sql, condition.abbreviation, "abbreviation", 2);
                genSqlWhere(ref sql, condition.tel, "tel", 2);
                genSqlWhere(ref sql, condition.fax, "fax", 2);
                genSqlWhere(ref sql, condition.contacts, "contacts", 2);

                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" customerNo DESC ");
                }

                return db.Page<v_customer>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <returns></returns>
        public List<v_customer> GetAllv_customer()
        {

            try
            {

                return db.Fetch<v_customer>("select * from v_customer order by customerNo");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到客户
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public v_customer Getv_customer(string customerNo)
        {

            try
            {
                return db.FirstOrDefault<v_customer>("SELECT * from v_customer WHERE customerNo=@0", customerNo);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个客户
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public bool IscustomerNo(string customerNo)
        {

            try
            {

                var v_customer = db.FirstOrDefault<v_customer>(@"SELECT * from v_customer where customerNo=@0", customerNo);
                if (v_customer == null)
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

        public void Add(v_customer model, List<v_customer_doc> modelsDetails)
        {

            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.addtime = DateTime.Now;
                    db.Insert(model);
                    if (modelsDetails != null)
                    {
                        foreach (var item in modelsDetails)
                        {
                            item.customerNo = model.customerNo;
                            item.addtime = DateTime.Now;
                            item.adduser = model.adduser;
                            db.Insert(item);
                        }
                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }
        public void Add(v_customer model)
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
        public void Edit(v_customer model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "customerName","abbreviation","tel",
                        "fax","contacts","address",
                        "remark", "upduser", "updtime" });

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }
        public void Edit(v_customer model, List<v_customer_doc> modelsDetails)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "customerName","abbreviation","tel",
                        "fax","contacts","address",
                        "remark", "upduser", "updtime" });

                    //明细新增
                    //先删除，再新增
                    db.Execute("DELETE FROM v_customer_doc WHERE (customerNo=@0)", model.customerNo);
                    if (modelsDetails != null)
                    {
                        foreach (var item in modelsDetails)
                        {
                            item.customerNo = model.customerNo;
                            item.addtime = DateTime.Now;
                            item.upduser = model.upduser;
                            db.Insert(item);
                        }
                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(string[] customerNos)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in customerNos)
                    {
                        db.Delete(new v_customer() { customerNo = item });
                        //删除明细
                        db.Execute("DELETE FROM v_customer_doc WHERE (customerNo=@0)", item);
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

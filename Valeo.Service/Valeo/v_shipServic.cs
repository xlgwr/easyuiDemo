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
    public class v_shipService : BaseService
    {

        /// <summary>
        /// 查询分页物流商表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_ship> GetPagev_ships(long page, long rows, string sort, string order, v_ship condition)
        {

            try
            {


                var sql = new Sql().Append(@"SELECT * from v_ship ");

                genSqlWhere(ref sql, condition.shipNo, "shipNo", 0);
                genSqlWhere(ref sql, condition.shipName, "shipName", 2);
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
                    sql.OrderBy(" shipNo DESC ");
                }

                return db.Page<v_ship>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有物流商
        /// </summary>
        /// <returns></returns>
        public List<v_ship> GetAllv_ship()
        {

            try
            {

                return db.Fetch<v_ship>("select * from v_ship order by shipNo");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到物流商
        /// </summary>
        /// <param name="shipNo"></param>
        /// <returns></returns>
        public v_ship Getv_ship(string shipNo)
        {

            try
            {
                return db.FirstOrDefault<v_ship>("SELECT * from v_ship WHERE shipNo=@0", shipNo);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个物流商
        /// </summary>
        /// <param name="shipNo"></param>
        /// <returns></returns>
        public bool IsshipNo(string shipNo)
        {

            try
            {

                var v_ship = db.FirstOrDefault<v_ship>(@"SELECT * from v_ship where shipNo=@0", shipNo);
                if (v_ship == null)
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

        public void Add(v_ship model)
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

        public void Edit(v_ship model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "shipName","abbreviation","tel",
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

        public void Deletes(string[] shipNos)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in shipNos)
                    {
                        db.Delete(new v_ship() { shipNo = item });
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

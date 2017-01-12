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
    public class v_carsupportService : BaseService
    {

        /// <summary>
        /// 查询分页车辆信息表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_carsupport> GetPagev_carsupports(long page, long rows, string sort, string order, v_carsupport condition)
        {

            try
            {


                var sql = new Sql().Append(@"SELECT * from v_carsupport ");

                genSqlWhere(ref sql, condition.carNo, "carNo", 0);
                genSqlWhere(ref sql, condition.carNumber, "carNumber", 2);
                genSqlWhere(ref sql, condition.driver, "driver", 2);
                genSqlWhere(ref sql, condition.driverTel, "driverTel", 2);


                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" carNo DESC ");
                }

                return db.Page<v_carsupport>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有车辆信息
        /// </summary>
        /// <returns></returns>
        public List<v_carsupport> GetAllv_carsupport()
        {

            try
            {

                return db.Fetch<v_carsupport>("select * from v_carsupport order by carNo");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到车辆信息
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public v_carsupport Getv_carsupport(string carNo)
        {

            try
            {
                return db.FirstOrDefault<v_carsupport>("SELECT * from v_carsupport WHERE carNo=@0", carNo);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个车辆信息
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public bool IscarNo(string carNo)
        {

            try
            {

                var v_carsupport = db.FirstOrDefault<v_carsupport>(@"SELECT * from v_carsupport where carNo=@0", carNo);
                if (v_carsupport == null)
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

        public void Add(v_carsupport model)
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

        public void Edit(v_carsupport model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "carNumber","driver","driverTel",
                        "status",
                        "remark", "upduser", "updtime" });

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(string[] carNos)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in carNos)
                    {
                        db.Delete(new v_carsupport() { carNo = item });
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

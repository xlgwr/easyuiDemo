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
    public class v_cartonService : BaseService
    {

        /// <summary>
        /// 查询分页纸箱表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_carton> GetPagev_cartons(long page, long rows, string sort, string order, v_carton condition)
        {

            try
            {

                var sql = new Sql().Append(@"SELECT * from v_carton ");

                genSqlWhere(ref sql, condition.cartonNO, "cartonNO", 0);
                genSqlWhere(ref sql, condition.cardType, "cardType", 0);
                genSqlWhere(ref sql, condition.cartonSize, "cartonSize", 2);

                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" cartonNO DESC ");
                }

                return db.Page<v_carton>(page, rows, sql);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }
        }

        /// <summary>
        /// 获取所有纸箱
        /// </summary>
        /// <returns></returns>
        public List<v_carton> GetAllv_carton()
        {

            try
            {

                return db.Fetch<v_carton>("select * from v_carton order by cartonNO");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到纸箱
        /// </summary>
        /// <param name="cartonNO"></param>
        /// <returns></returns>
        public v_carton Getv_carton(string cartonNO)
        {

            try
            {
                return db.FirstOrDefault<v_carton>("SELECT * from v_carton WHERE cartonNO=@0", cartonNO);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个纸箱
        /// </summary>
        /// <param name="cartonNO"></param>
        /// <returns></returns>
        public bool IscartonNO(string cartonNO)
        {

            try
            {

                var v_carton = db.FirstOrDefault<v_carton>(@"SELECT * from v_carton where cartonNO=@0", cartonNO);
                if (v_carton == null)
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

        public void Add(v_carton model)
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

        public void Edit(v_carton model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;

                    db.Update(model, new List<string>() { 
                        "cartonSize","cardType","pcs",
                        "remark", "upduser", "updtime" });

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(string[] cartonNOs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in cartonNOs)
                    {
                        db.Delete(new v_carton() { cartonNO = item });
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

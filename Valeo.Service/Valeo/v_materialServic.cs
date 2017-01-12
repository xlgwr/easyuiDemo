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
    public class v_materialService : BaseService
    {

        /// <summary>
        /// 查询分页材料表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_material> GetPagev_materials(long page, long rows, string sort, string order, v_material condition)
        {

            try
            {


                var sql = new Sql().Append(@"SELECT * from v_material ");

                genSqlWhere(ref sql, condition.partNO, "partNO", 0);
                genSqlWhere(ref sql, condition.cartonType, "cartonType", 0);
                genSqlWhere(ref sql, condition.customerPartNO, "customerPartNO", 2);
                genSqlWhere(ref sql, condition.partName, "partName", 2);

                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" partNO DESC ");
                }

                return db.Page<v_material>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有材料
        /// </summary>
        /// <returns></returns>
        public List<v_material> GetAllv_material()
        {

            try
            {

                return db.Fetch<v_material>("select * from v_material order by partNO");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到材料
        /// </summary>
        /// <param name="partNO"></param>
        /// <returns></returns>
        public v_material Getv_material(string partNO)
        {

            try
            {
                return db.FirstOrDefault<v_material>("SELECT * from v_material WHERE partNO=@0", partNO);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个材料
        /// </summary>
        /// <param name="partNO"></param>
        /// <returns></returns>
        public bool IspartNO(string partNO)
        {

            try
            {

                var v_material = db.FirstOrDefault<v_material>(@"SELECT * from v_material where partNO=@0", partNO);
                if (v_material == null)
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

        public void Add(v_material model)
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

        public void Edit(v_material model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "partName","customerPartNO","cartonType",
                        "pcsNumber","weight","unit",
                        "remark", "upduser", "updtime" });

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(string[] partNOs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in partNOs)
                    {
                        db.Delete(new v_material() { partNO = item });
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

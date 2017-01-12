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
    public class v_lableModelService : BaseService
    {

        /// <summary>
        /// 查询分页标签模板表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_lableModel> GetPagev_lableModels(long page, long rows, string sort, string order, v_lableModel condition)
        {

            try
            {


                var sql = new Sql().Append(@"SELECT * from v_lableModel ");

                genSqlWhere(ref sql, condition.lableID, "lableID", 0);
                genSqlWhere(ref sql, condition.lableName, "lableName", 0);
                genSqlWhere(ref sql, condition.path, "path", 2);

                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" lableID DESC ");
                }

                return db.Page<v_lableModel>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有标签模板
        /// </summary>
        /// <returns></returns>
        public List<v_lableModel> GetAllv_lableModel()
        {

            try
            {

                return db.Fetch<v_lableModel>("select * from v_lableModel order by lableID");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }
        /// <summary>
        /// 根据ID得到标签模板
        /// </summary>
        /// <param name="lableID"></param>
        /// <returns></returns>
        public v_lableModel Getv_lableModel(string lableID)
        {

            try
            {
                return db.FirstOrDefault<v_lableModel>("SELECT * from v_lableModel WHERE lableID=@0", lableID);

            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 判断是否有这个标签模板
        /// </summary>
        /// <param name="lableID"></param>
        /// <returns></returns>
        public bool IslableID(string lableID)
        {

            try
            {

                var v_lableModel = db.FirstOrDefault<v_lableModel>(@"SELECT * from v_lableModel where lableName=@0", lableID);
                if (v_lableModel == null)
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

        public void Add(v_lableModel model)
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

        public void Edit(v_lableModel model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { 
                        "lableName","path",
                        "remark", "upduser", "updtime" });


                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(double[] lableIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in lableIDs)
                    {
                        db.Delete(new v_lableModel() { lableID = item });
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

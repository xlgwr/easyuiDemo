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
    public class v_cardService : BaseService
    {
        /// <summary>
        /// 查询分页卡板表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<v_card> GetPagev_cards(long page, long rows, string sort, string order, v_card condition)
        {

            try
            {
                var sql = new Sql().Append(@"SELECT * from v_card ");

                genSqlWhere(ref sql, condition.cardNO, "cardNO", 0);
                genSqlWhere(ref sql, condition.transType, "transType", 0);
                genSqlWhere(ref sql, condition.cardSize, "cardSize", 2);

                if (!string.IsNullOrEmpty(sort))
                {
                    sql.OrderBy(sort + " " + order);
                }
                else
                {
                    sql.OrderBy(" cardNO DESC ");
                }

                return db.Page<v_card>(page, rows, sql);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }

        }

        /// <summary>
        /// 获取所有卡板
        /// </summary>
        /// <returns></returns>
        public List<v_card> GetAllv_card()
        {

            try
            {
                return db.Fetch<v_card>("select * from v_card order by cardNO");
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }
        }
        /// <summary>
        /// 根据ID得到卡板
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public v_card Getv_card(string cardNO)
        {

            try
            {
                return db.FirstOrDefault<v_card>("SELECT * from v_card WHERE cardNO=@0", cardNO);
            }
            catch (Exception ex)
            {

                _logger.Error(ex); throw ex;
            }
        }

        /// <summary>
        /// 判断是否有这个卡板
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public bool IscardNO(string cardNO)
        {

            try
            {

                var v_card = db.FirstOrDefault<v_card>(@"SELECT * from v_card where cardNO=@0", cardNO);
                if (v_card == null)
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

        public void Add(v_card model)
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

        public void Edit(v_card model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {

                    model.updtime = DateTime.Now;
                    db.Update(model, new List<string>() { "cardSize", "transType", "remark","upduser","updtime" });


                    scope.Complete();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex); throw ex;
                }
            }
        }

        public void Deletes(string[] cardNOs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in cardNOs)
                    {
                        db.Delete(new v_card() { cardNO = item });
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

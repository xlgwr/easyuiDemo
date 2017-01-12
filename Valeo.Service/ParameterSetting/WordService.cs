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
    public class WordService : BaseService
    {
        /// <summary>
        /// 查询分页操作记录表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<WordModelVM> GetListPages(long page, long rows, string sort, string order, WordModel condition)
        {
            List<Object> listPara = new List<object>();

            var sql = new Sql().Append(@"select *, ");
            //类别(1:简体-拼音 2:简体-繁体 3:繁体-拼音 4:简体-字码 5-繁体-字码)
            sql.Append(@" (CASE Type WHEN 1 THEN N'简体-拼音' WHEN 2 THEN N'简体-繁体' WHEN 3 THEN N'繁体-拼音' WHEN 4 THEN N'简体-字码' WHEN 5 THEN N'繁体-字码' ELSE N'其它' END) as TypeVM ");
            sql.Append(@" from m_Word ");

            if (condition.Type > 0)
            {
                sql.Where(" Type = @0  ", condition.Type);
            }

            if (!string.IsNullOrEmpty(condition.WordKey))
            {
                sql.Where(" WordKey  like  @0  ",  condition.WordKey + "%");
            }

            if (!string.IsNullOrEmpty(condition.WordValue))
            {
                sql.Where(" WordValue  like  @0  ",  condition.WordValue + "%");
            }

            if (!string.IsNullOrEmpty(sort))
            {
                sql.OrderBy(sort + " " + order);
            }

            return db.Page<WordModelVM>(page, rows, sql);
        }

        public WordModel GetModel(long id)
        {
            var userModel = db.FirstOrDefault<WordModel>("select  * from m_Word where WordId=@0", id);

            return userModel;

        }
        public WordModel GetModel(WordModel id)
        {
            var userModel = db.FirstOrDefault<WordModel>("select  * from m_Word where WordKey=@0 and Type=@1 and WordValue=@2", id.WordKey, id.Type, id.WordValue);

            return userModel;

        }
        public void Add(WordModel model)
        {
            db.Insert(model);
        }
        public void Edit(WordModel model)
        {
            db.Update(model);
        }
        public void Delete(long id)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Delete(new WordModel() { WordId = id });
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

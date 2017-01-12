using System;
using System.Collections.Generic;
using Valeo.Domain;
using PetaPoco;

namespace Valeo.Service
{
    /// <summary>
    /// 人员信息服务类
    /// </summary>
    public class PersonService : BaseService
    {
      
        #region 【查询处理】

        /// <summary>
        /// 按条件取得人员信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<Person> GetPersonList(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                condition = "1 = 1";
            }

            string strSql = string.Format(@"
                SELECT * FROM PersonInfo WHERE {0} ORDER BY PersonName", condition);

            List<Person> list = db.Fetch<Person>(strSql);

            return list;
        }

        /// <summary>
        /// 按条件取得人员信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<Person> GetPersonByCondition(int startIndex, int pageSize, string condition = "")
        {
            if (condition == string.Empty)
            {
                condition = " 1 = 1 ";
            }

            string strSql = string.Format(@"SELECT * FROM PersonInfo WHERE {0}", condition);
            var list = db.Page<Person>(startIndex, pageSize, strSql);

            return list;
        }

        #endregion

        #region 【新增处理】

        /// <summary>
        /// 新增人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPerson(Person model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    //查看该客户是否存在
                    var result = db.Fetch<Person>(string.Format(@"
                        SELECT * FROM PersonInfo WHERE PersonName='{0}'", model.PersonName));

                    if (result.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        db.Insert("PersonInfo", "PersonID", false, model);

                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 【修改处理】

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyPerson(Person model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Update("PersonInfo", "PersonID", model, model.PersonID);

                    scope.Complete();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 【删除处理】

        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        public bool DeletePerson(string PersonID)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Delete("PersonInfo", "PersonID", null, PersonID);
                    scope.Complete();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
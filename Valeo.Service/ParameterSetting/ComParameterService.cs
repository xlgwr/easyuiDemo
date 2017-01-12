using Valeo.Domain.ParameterSetting;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service.ParameterSetting
{
    public class ComParameterService : BaseService
    {
        #region 查询处理

        public Page<ComParameterModel> GetAllComList(ComParameterModel CPM, long page, long rows, string sort, string order)
        {
            try
            {
                Sql sql = new Sql().Append("SELECT * from m_ComParameter");
                if (!string.IsNullOrEmpty(CPM.Comvalue))
                {
                    sql.Where("Comvalue LIKE @0",  CPM.Comvalue + "%");
                }
                if (!string.IsNullOrEmpty(CPM.Comkey))
                {
                    sql.Where("Comkey=@0", CPM.Comkey);
                }
                if (CPM.Comtype != 0)
                {
                    sql.Where("Comtype=@0", CPM.Comtype);
                }
                if (string.IsNullOrWhiteSpace(sort))
                {
                    sql.OrderBy("DspNo ASC");
                }
                else
                {
                    sql.OrderBy(sort + " " + order);
                }

                return db.Page<ComParameterModel>(page, rows, sql);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ComParameterModel GetSingleComParament(string Comkey)
        {
            Sql sql = new Sql().Append(@"SELECT * from m_ComParameter  where Comkey=@0", Comkey);
            return db.Single<ComParameterModel>(sql);
        }
        public List<ComParameterModel> GetListComParament()
        {
            Sql sql = new Sql().Append(@"SELECT * from m_ComParameter");
            var models = db.Fetch<ComParameterModel>(sql);
            return models;
        }
        public Dictionary<string, ComParameterModel> getDicComP()
        {
            var tmpdic = new Dictionary<string, ComParameterModel>();
            try
            {
                Sql sql = new Sql().Append(@"SELECT * from m_ComParameter");
                var models = db.Fetch<ComParameterModel>(sql);

                if (models!=null)
                {
                    foreach (var item in models)
                    {
                        tmpdic.Add(item.Comkey, item);
                    }
                }
                return tmpdic;
            }
            catch (Exception)
            {
                return tmpdic;
            }

        }

        #endregion

        #region 新增处理

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="CPM"></param>
        /// <returns></returns>
        public long AddSave(ComParameterModel CPM)
        {
            var result = db.Fetch<ComParameterModel>(string.Format(@"SELECT * from m_ComParameter  where Comkey='{0}'", CPM.Comkey));
            if (result.Count > 0)
            {
                return 0;
            }
            else
            {
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        db.Insert(CPM);
                        scope.Complete();
                        return 1;
                    }
                    catch (Exception)
                    {
                        return -1;
                        throw;
                    }
                }

            }

        }

        #endregion

        #region 修改处理

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="CPM"></param>
        public long EditSave(ComParameterModel CPM)
        {
            var result = db.Fetch<ComParameterModel>(string.Format(@"SELECT * from m_ComParameter  where Comkey='{0}'", CPM.Comkey));
            if (result.Count > 1)
            {
                return 0;
            }
            else
            {
                var oldModel = GetSingleComParament(CPM.Comkey);
                oldModel.Comvalue = CPM.Comvalue;
                oldModel.Comtype = CPM.Comtype;
                oldModel.Remark = CPM.Remark;
                oldModel.DspNo = CPM.DspNo;
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        db.Update(oldModel);
                        scope.Complete();
                        return 1;
                    }
                    catch (Exception)
                    {
                        return -1;
                        throw;
                    }

                }
            }

        }
        #endregion

        #region 删除处理
        public void Delete(string Comkey)
        {
            try
            {
                db.Delete("m_ComParameter", "Comkey", null, Comkey);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}

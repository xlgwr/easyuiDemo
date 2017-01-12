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
    public class SysParameterService : BaseService
    {
        /// <summary>
        /// 查询分页操作记录表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<SysParameterVM> GetListPages(long page, long rows, string sort, string order, SysParameterModel condition)
        {
            List<Object> listPara = new List<object>();

            var sql = new Sql().Append(@"select dspno,Paramkey,Paramvalue,Paramtype,");
            sql.Append(@"  (CASE Paramtype WHEN 0 THEN '系统参数' WHEN 1 THEN '用户参数' ELSE '其它' END) as ParamtypeVM,");
            sql.Append(@" Remark,Adduser,Addtime,Upduser,Updtime from dbo.m_Parameter");


            if (!string.IsNullOrEmpty(condition.Paramkey))
            {
                sql.Where(" Paramkey  like  @0  ",  condition.Paramkey + "%");
            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToLower().Equals("paramtypevm"))
                {
                    sql.OrderBy(" Paramtype " + order);
                }
                else
                {
                    sql.OrderBy(sort + " " + order);
                }
            }
            else
            {
                sql.OrderBy(" Paramtype,dspno ");
            }

            return db.Page<SysParameterVM>(page, rows, sql);
        }

        public SysParameterModel GetModel(string id)
        {
            var userModel = db.FirstOrDefault<SysParameterModel>("select  * from dbo.m_Parameter where Paramkey=@0", id);         

            return userModel;

        }
        public void Add(SysParameterModel model)
        {
            model.addtime = DateTime.Now;
            model.updtime = DateTime.Now;
            db.Insert(model);
        }
        public void Edit(SysParameterModel model)
        {
            model.updtime = DateTime.Now;
            db.Update(model, new List<string>() { "Paramvalue", "Remark", "Paramtype", "DspNo", "upduser", "updtime" });
        }
        public void Delete(string id)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {

                    db.Delete(new SysParameterModel() { Paramkey = id });

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


using Valeo.Common;
using Valeo.Domain.Parameter;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valeo.Domain.Models;

namespace Valeo.Service.Parameter
{
    public class ParameterService : BaseService
    {

        /// <summary>
        /// 得到所有的币种
        /// </summary>
        /// <returns></returns>
        public List<ParameterModel> GetAllCurrencys()
        {

            return db.Query<ParameterModel>("Where Paramtype=0 and Paramkey like 'Currency%' order by DspNo asc").ToList();
        }
        /// <summary>
        /// 得到所有案件类型
        /// </summary>
        /// <returns></returns>
        public List<CaseTypeModel> GetCaseType()
        { 
            return db.Query<CaseTypeModel>("Where 1=1").ToList();
        }
        /// <summary>
        /// 得到所有法院
        /// </summary>
        /// <returns></returns>
        public List<CourtModel> GetCourts()
        {
            return db.Query<CourtModel>("Where 1=1").ToList();
        }

        public List<CaseTypeModel> GetCaseType(long courtId)
        {
            return db.Query<CaseTypeModel>("Where CourtID=@0", courtId).ToList();
        }
        /// <summary>
        /// 得到所有国别
        /// </summary>
        /// <returns></returns>
        public List<Valeo.Domain.Models.CountryModel> GetAllCountry()
        {
            return db.Query<Valeo.Domain.Models.CountryModel>("Where 1=1 order by Sort ").ToList();
        }

        public void SetDataFinishTime(string userId)
        {
            var parameterModel = db.FirstOrDefault<ParameterModel>("Where Paramkey='DataFinishTime'");
            parameterModel.Paramvalue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            parameterModel.Upduser = userId;
            parameterModel.Updtime=DateTime.Now;
            db.Update(parameterModel);

        }
    }
}
 
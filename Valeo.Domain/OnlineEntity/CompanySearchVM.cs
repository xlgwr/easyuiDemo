using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
   public  class CompanySearchVM
    {
       /// <summary>
       /// 公司编号
       /// </summary>
       public string CRNo{get;set;}

       /// <summary>
       /// 公司英文名称
       /// </summary>
       public string FullName_En { get; set; }

       /// <summary>
       /// 公司中文名称
       /// </summary>
       public string FullName_Cn { get; set; }

       /// <summary>
       /// 成立日期开始
       /// </summary>
       public string IncorporationDateStart { get; set; }
     
       /// <summary>
       /// 成立日期结束
       /// </summary>
       public string IncorporationDateEnd { get; set; }

       /// <summary>
       /// 已告解散日期开始
       /// </summary>
       public string DissolutionDateStart { get; set; }

       /// <summary>
       /// 已告解散日期结束
       /// </summary>
       public string DissolutionDateEnd { get; set; }

       /// <summary>
       /// 是否上市(0:未上市 1:已上市)
       /// </summary>
       public long? Listed { get; set; }

       /// <summary>
       /// 营业执照号码
       /// </summary>
       public virtual string CIBRNO { get; set; }
    }
}

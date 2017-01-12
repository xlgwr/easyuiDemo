using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Enum
{
   public  class EnumCase
    {
       /// <summary>
        /// 1：线下可见（未审） 2:线上可见（已审）(公司以此为准)
       /// </summary>
       public enum EnumCaseShow
       {
           /// <summary>
           /// 1：线下可见（未审）
           /// </summary>
           OfflineShow = 1,
           /// <summary>
           ///  2:线上可见（已审）(公司以此为准)
           /// </summary>
           OnlineShow = 2
       }
    }
}

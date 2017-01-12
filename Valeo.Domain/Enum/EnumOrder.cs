using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Enum
{
   public class EnumOrder
    {
       public enum EnumPayState
       {
           /// <summary>
           /// 0：线下可见（未审）
           /// </summary>
           NoPay = 0,
           /// <summary>
           /// 1:线上可见（已审）(公司以此为准)
           /// </summary>
           YesPay = 1,
           /// <summary>
           /// 2:部分付款
           /// </summary>
           PartPay=2
       }
    }
}

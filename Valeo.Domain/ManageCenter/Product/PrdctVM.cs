using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 产品使用明细画面表格
    /// </summary>
   [Serializable] 
   public class PrdctVM
    {

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品详细-【产品名称-次数】
        /// </summary>
        public string ProductDetail { get; set; }

        /// <summary>
        /// 购买次数[不需要显示]
        /// </summary>
        public string UnitsBuy { get; set; }

        /// <summary>
        /// 已经使用次数
        /// </summary>
        public string UnitsUsed { get; set; }

        /// <summary>
        /// 剩余次数
        /// </summary>
        public string UnitsLost { get; set; }


        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 超出费用
        /// </summary>
        public string OverMoney { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        public string ValidityDate { get; set; }

    }
}

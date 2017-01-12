using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 订单明细表
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_OrderList")]
    [PetaPoco.PrimaryKey("OrderID")]
    public class OrderListModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long OrderListID { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public long OrderID { get; set; }

        /// <summary>
        /// 报价ID
        /// </summary>
        public long? QuoteListID { get; set; }

        /// <summary>
        /// 套餐ID
        /// </summary>
        public string PackageID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        public int UnitsBuy { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UnitsUsed { get; set; }

        /// <summary>
        /// 已开票次数(开票后+上，删掉则-下去)
        /// </summary>
        public int UnitsInvoice { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public float TotalAmount { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime EnabledDate { get; set; }

        /// <summary>
        /// 有效期(几个月)
        /// </summary>
        public int Validity { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime ValidityDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        ///// <summary>
        ///// 添加者
        ///// </summary>
        //public string AddUser { get; set; }

        ///// <summary>
        ///// 添加时间
        ///// </summary>
        //public DateTime AddDatetime { get; set; }

        ///// <summary>
        ///// 更新者
        ///// </summary>
        //public string UpdUser { get; set; }

        ///// <summary>
        ///// 更新时间
        ///// </summary>
        //public DateTime? UpdDatetime { get; set; }

        public class VarKey
        {
            public const string tablename = "t_OrderList";
            public const string OrderListID = "OrderListID";
            public const string OrderID = "OrderID";
            public const string QuoteListID = "QuoteListID";
            public const string PackageID = "PackageID";
            public const string ProductID = "ProductId";
            public const string ProductName = "ProductName";
            public const string UnitsBuy = "UnitsBuy";
            public const string UnitsUsed = "UnitsUsed";
            public const string UnitsInvoice = "UnitsInvoice";
            public const string Price = "Price";
            public const string TotalAmount = "TotalAmount";
            public const string EnabledDate = "EnabledDate";
            public const string Validity = "Validity";
            public const string ValidityDate = "ValidityDate";
            public const string Remark = "Remark";
            public const string AddUser = "AddUser";
            public const string AddDatetime = "AddDatetime";
            public const string UpdUser = "UpdUser";
            public const string UpdDatetime = "UpdDatetime";
        }
    }
}
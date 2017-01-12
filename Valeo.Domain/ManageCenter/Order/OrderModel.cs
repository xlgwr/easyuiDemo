using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 订单表
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_Order")]
    [PetaPoco.PrimaryKey("OrderID")]
    public class OrderModel
    {
        /// <summary>
        /// 订单id (20160527001)
        /// </summary>
        public long OrderID { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        /// 订单类别(0:线上下单 1:离线下单)
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 币别
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public float Total { get; set; }

        /// <summary>
        /// 0:禁用 1:启用 2:取消（在线付款的，自动为1；但离线的可以先用后付款，所以由客服手工点为1,如果此订单不要了则可点成2。）
        /// </summary>
        public int Enable { get; set; }

        /// <summary>
        /// 收款方式(0:现结 1:月结 2:包年 3:特殊)
        /// </summary>
        public int PayWay { get; set; }

        /// <summary>
        /// 是否付款(0:未付1:已付)(只针对0:现结的)
        /// </summary>
        public int PayState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否开票(0:不开票 1:要开票)
        /// </summary>
        public int Invoice { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdTime { get; set; }
    }

    public class VarKey
    {
        public const string tablename = "t_Order";
        public const string OrderID = "OrderID";
        public const string OrderType = "OrderType";
        public const string OrderDate = "OrderDate";
        public const string Currency = "Currency";
        public const string Total = "Total";
        public const string ProductName = "ProductName";
        public const string PaymentWay = "PaymentWay";
    }
}
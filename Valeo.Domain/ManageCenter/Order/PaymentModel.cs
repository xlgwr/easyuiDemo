using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 收款表
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_Payment")]
    [PetaPoco.PrimaryKey("PaymentID")]
    public class PaymentModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public string PaymentID { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 发票id
        /// </summary>
        public string InvoiceID { get; set; }

        /// <summary>
        /// 类别(0:充值 1:消费)
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 支付方式(0:paypal，1:中国银联，2:支付宝)
        /// </summary>
        public string PaymentWay { get; set; }

        /// <summary>
        /// 支付接口id(长度根据实际调整)
        /// </summary>
        public string PayInterfaceID { get; set; }

        /// <summary>
        /// 小计金额(当类别为0:充值时,会员余额需要增加)
        /// </summary>
        public double SubTotal { get; set; }

        /// <summary>
        /// 当前余额(1、充值时要显示 2、消费时如果是从余额扣的也要显示)
        /// </summary>
        public string RemainingSum { get; set; }

        /// <summary>
        /// 支付是否成功(0:失败 1:成功)
        /// </summary>
        public string Success { get; set; }

        /// <summary>
        /// 支付日期
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// Email人id(没发则为空)
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// 币别
        /// </summary>
        public virtual string Currency { get; set; }

        public class VarKey
        {
            public const string tablename = "t_Payment";
            public const string PaymentID = "PaymentID";
            public const string MemberID = "MemberID";
            public const string OrderID = "OrderID";
            public const string InvoiceID = "InvoiceID";
            public const string PayType = "PayType";
            public const string PaymentWay = "PaymentWay";
            public const string PayInterfaceID = "PayInterfaceID";
            public const string SubTotal = "SubTotal";
            public const string RemainingSum = "RemainingSum";
            public const string Success = "Success";
            public const string PayDate = "PayDate";
            public const string EmailID = "EmailID";
            public const string Currency = "Currency";
        }

    }
}

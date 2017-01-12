using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 我的订单画面表格
    /// </summary>
    [Serializable] 
    public class OrderVM
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
        /// 币别
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// 是否付款(0:未付1:已付)(只针对0:现结的)
        /// </summary>
        public string PayState { get; set; }

        /// <summary>
        /// 支付方式(0:paypal，1:中国银联，2:支付宝)
        /// </summary>
        public string PaymentWay { get; set; }


        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }


        /// <summary>
        /// 0:禁用 1:启用 2:取消（在线付款的，自动为1；但离线的可以先用后付款，所以由客服手工点为1,如果此订单不要了则可点成2。）
        /// </summary>
        public string Enable { get; set; }
    }
}

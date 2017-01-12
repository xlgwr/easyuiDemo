using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    public class OrderService : BaseService
    {


        /// <summary>
        /// 查询该用户所购买产品的列表
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public List<OrderListModel> getSel(string MemberID)
        {

            List<OrderListModel> listMember = new List<OrderListModel>();
            Sql sql = new Sql();
            sql.Append("  select tol.*");
            sql.Append("  from t_OrderList as tol ");
            sql.Append("  left join t_Order as tod on tol.OrderID=tol.OrderID and tod.MemberID =@0 ", MemberID);

            listMember = db.Fetch<OrderListModel>(sql);

            return listMember;
        }

        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<OrderVM> GetPageOrderList(GridPager pager, string fromdate = ""
                                                    , string todate = "",  string prdctNm = "", string paymentWay = "", string payStatus = "", string orderStatus = "")
        {


            Sql sql = new Sql()
                .Append(@"               SELECT   dbo.t_Order.OrderID, dbo.t_Order.OrderDate, dbo.t_OrderList.ProductName, dbo.t_Order.Currency, dbo.t_Order.Total,");
            sql.Append(@"                CASE dbo.t_Order.Enable WHEN '0' THEN '未启用' WHEN '1' THEN '启用' WHEN '2' THEN '取消' ELSE '启用' END AS Enable,");
            sql.Append(@"                CASE dbo.t_Payment.PaymentWay WHEN '0' THEN 'paypal' WHEN '1' THEN '中国银联' WHEN '2' THEN '支付宝' END AS PaymentWay,     ");
            sql.Append(@"                CASE dbo.t_Order.PayState WHEN '0' THEN '未付款' WHEN '1' THEN '已付款' ELSE '未付款' END AS PayState,  ");
            sql.Append(@"                CASE dbo.t_Order.PayState WHEN '0' THEN '付款' ELSE null END AS Operation ");

            sql.Append(@"                 FROM      dbo.t_Order INNER JOIN ");
            sql.Append(@"                 dbo.t_OrderList ON dbo.t_Order.OrderID = dbo.t_OrderList.OrderID left JOIN ");
            sql.Append(@"                 dbo.t_Payment ON dbo.t_OrderList.OrderID = dbo.t_Payment.OrderID ");

            //订单日期
            if (!string.IsNullOrEmpty(fromdate))
            {
                sql.Where("  dbo.t_Order.OrderDate>= @0  ", fromdate);
            }

            if (!string.IsNullOrEmpty(todate))
            {
                sql.Where("   dbo.t_Order.OrderDate<= @0  ", todate);
            }
            //产品
            if (prdctNm != "全部")
            {
                sql.Where("   dbo.t_OrderList.ProductName= @0  ", prdctNm);
            }

            if (paymentWay!="-1")
            {
                sql.Where("   dbo.t_Payment.PaymentWay = @0  ", paymentWay);
            }

            if (payStatus != "-1")
            {
                sql.Where("   dbo.t_Order.PayState = @0  ", payStatus);
            }

            if (orderStatus != "-1")
            {
                sql.Where("   dbo.t_Order.Enable= @0  ", orderStatus);
            }
            sql.OrderBy(" dbo.t_Order.OrderID");


            var list = db.Page<OrderVM>(pager.page, pager.pageSize, sql);
            string orderID=string.Empty;
            List<OrderVM> lists = new List<OrderVM>();
            lists.AddRange(list.Items);
            list.Items.Clear();
            for (int i = 0; i < lists.Count; i++)
            {
                if (orderID == lists[i].OrderID)
                {


                        int ctn = list.Items.Count;
                        list.Items[ctn - 1].ProductName += ("\r\n" + lists[i].ProductName);
                   
                }
                else
                {
                    DateTime dt;
                    dt = Convert.ToDateTime(lists[i].OrderDate);
                    lists[i].OrderDate = dt.ToString("yyyy-MM-dd HH:mm");
                    if (lists[i].PayState!="已付款")
                    {
                        lists[i].Operation = "付款";
                    }
                     orderID = lists[i].OrderID;
                    list.Items.Add(lists[i]);
                }
            }
            list.TotalItems = list.Items.Count;
            return list;
        }

        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<OrderVM> GetPageOneOrderList(string orderId)
        {
            List<OrderVM> list = new List<OrderVM>();
            if (string.IsNullOrEmpty(orderId)) return list;

            Sql sql = new Sql()
                .Append(@"               SELECT   dbo.t_Order.OrderID, dbo.t_Order.OrderDate, dbo.t_OrderList.ProductName, dbo.t_Order.Currency, dbo.t_Order.Total,");
            sql.Append(@"                CASE dbo.t_Order.Enable WHEN '0' THEN '未启用' WHEN '1' THEN '启用' WHEN '2' THEN '取消' ELSE '启用' END AS Enable,");
            sql.Append(@"                CASE dbo.t_Payment.PaymentWay WHEN '0' THEN 'paypal' WHEN '1' THEN '中国银联' WHEN '2' THEN '支付宝' END AS PaymentWay,     ");
            sql.Append(@"                CASE dbo.t_Order.PayState WHEN '0' THEN '未付款' WHEN '1' THEN '已付款' ELSE '未付款' END AS PayState,  ");
            sql.Append(@"                CASE dbo.t_Order.PayState WHEN '0' THEN '付款' ELSE null END AS Operation ");

            sql.Append(@"                 FROM      dbo.t_Order INNER JOIN ");
            sql.Append(@"                 dbo.t_OrderList ON dbo.t_Order.OrderID = dbo.t_OrderList.OrderID left JOIN ");
            sql.Append(@"                 dbo.t_Payment ON dbo.t_OrderList.OrderID = dbo.t_Payment.OrderID ");

            //订单日期
            if (!string.IsNullOrEmpty(orderId))
            {
                sql.Where("  dbo.t_Order.orderId= @0  ", orderId);
            }

            list = db.Fetch<OrderVM>(sql);


            string orderID = string.Empty;
            List<OrderVM> lists = new List<OrderVM>();
            lists.AddRange(list);
            list.Clear();
            for (int i = 0; i < lists.Count; i++)
            {
                if (orderID == lists[i].OrderID)
                {


                    int ctn = list.Count;
                    list[ctn - 1].ProductName += ("\r\n" + lists[i].ProductName);

                }
                else
                {
                    DateTime dt;
                    dt = Convert.ToDateTime(lists[i].OrderDate);
                    lists[i].OrderDate = dt.ToString("yyyy-MM-dd HH:mm");
                    if (lists[i].PayState != "已付款")
                    {
                        lists[i].Operation = "付款";
                    }
                    orderID = lists[i].OrderID;
                    list.Add(lists[i]);
                }
            }

            return list;
        }
    }
}

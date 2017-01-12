using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    /// <summary>
    /// 产品使用明细
    /// </summary>
    public class PrdctService : BaseService
    {
        #region 【查询处理】

        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<MemberModel> GetMemberList(string MemberID)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT   *
                    FROM    dbo.m_Member 
                    WHERE MemberID =@0", MemberID);

            List<MemberModel> list = db.Fetch<MemberModel>(sql);

            return list;
        }


        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<PrdctService> GetPageMessageList(int startIndex, int pageSize, string condition = "")
        {


            Sql sql = new Sql().Append(@" 
                    SELECT   REC.MemberID, REC.MessageID, REC.IsRead, MSG.UserID, MSG.MessageTitle, MSG.Message, MSG.SendTime
                    FROM    dbo.t_Recipient AS REC LEFT OUTER JOIN
                            dbo.t_Message AS MSG ON REC.MessageID = MSG.MessageID ");

            sql.Append(" ORDER BY MSG.SendTime DESC ");

            var list = db.Page<PrdctService>(startIndex, pageSize, sql);

            return list;
        }

        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<PrdctVM> GetPagePrdctList(GridPager pager, string MemberID )
        {
            

            Sql sql = new Sql().Append(@" 
                    SELECT   dbo.t_OrderList.OrderID, dbo.t_Order.OrderDate, dbo.m_Product.ProductName, 
                CAST(dbo.m_Product.ProductName AS varchar(50)) + '-' + CAST(dbo.t_OrderList.UnitsBuy AS varchar(50)) + '次'
                AS ProductDetail, ISNULL(dbo.t_OrderList.UnitsUsed, 0) AS UnitsUsed, ISNULL(dbo.t_OrderList.UnitsBuy, 0) 
                - ISNULL(dbo.t_OrderList.UnitsUsed, 0) AS UnitsLost, CONVERT(varchar(100), dbo.t_OrderList.ValidityDate, 23) 
                AS ValidityDate, dbo.m_Product.Price");


            sql.Append(" FROM      dbo.m_Product INNER JOIN  ");
            sql.Append("  dbo.t_OrderList ON dbo.m_Product.ProductId = dbo.t_OrderList.ProductId INNER JOIN  ");
            sql.Append("   dbo.t_Order ON dbo.t_OrderList.OrderID = dbo.t_Order.OrderID ");

            if (MemberID!="0")
            {
                sql.Where(" dbo.t_Order.MemberID = @0  ", MemberID);
            }


            sql.Append("  order by  dbo.t_Order.OrderDate ");

            var list = db.Page<PrdctVM>(pager.page, pager.pageSize, sql);


            string orderID = string.Empty;
            List<PrdctVM> lists = new List<PrdctVM>();
            lists.AddRange(list.Items);
            list.Items.Clear();
            for (int i = 0; i < lists.Count; i++)
            {
                DateTime dt;
                dt = Convert.ToDateTime(lists[i].OrderDate);
                lists[i].OrderDate = dt.ToString("yyyy-MM-dd HH:mm");
                if (Convert.ToInt32( lists [i].UnitsLost)<0)
                {
                    lists[i].OverMoney = (-Convert.ToInt32(lists[i].UnitsLost) * Convert.ToInt32(lists[i].Price )).ToString ();
                }
                else
                {
                    lists[i].OverMoney = "0";
                }

                list.Items.Add(lists[i]);
               
            }
            list.TotalItems = list.Items.Count;

            return list;
        }

        #endregion

    }
}

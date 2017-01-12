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
    /// 搜索记录业务查询
    /// </summary>
    public class SearchHistoryService : BaseService
    {


        #region 【查询处理】




        /// <summary>
        /// 按条件取得检索记录信息列表(分页数据)
        /// </summary>
        /// <param name="pager">分页信息</param>
        /// <param name="fromdate">开始日期</param>
        /// <param name="todate">结束日期</param>
        /// <param name="SchMemberNm">查册者</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="ProductName">产品名称</param>
        /// <param name="SchStatus">查询状态</param>
        /// <returns></returns>
        public Page<SearchHistoryVM> GetPageSchList(GridPager pager, string MemberID, string fromdate = "", string todate = "", string SchMemberNm = "", string SchContent = "", string ProductName = "", string SchStatus = "")
        {
            ///查册信息集合
            List<SearchHistoryVM> m_list = new List<SearchHistoryVM>();

            //订单明细id
            string m_OrderListID = GetOrderListID(ProductName);

            //【在线法庭】任务查询 t_TaskOnlineCase
            GetTaskOnlineCase(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【在线公司任务】任务查询 t_TaskOnlineCompany
            GetTaskOnlineCompany(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【在线土地任务】任务查询 t_TaskOnlineLand
            GetTaskOnlineLand(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【在线信贷任务】任务查询 t_TaskOnlineLoan
            GetTaskOnlineLoan(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【在线公共任务】任务查询 t_TaskOnlinePublic
            TaskOnlinePublic(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【离线法庭任务】任务查询   t_TaskOfflineCase
            TaskOfflineCase(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【离线公司任务】任务查询   t_TaskOfflineCompany
            TaskOfflineCompany(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【离线土地任务】任务查询   t_TaskOfflineLand
            TaskOfflineLand(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【离线其他任务】任务查询   t_TaskOfflineOther
            TaskOfflineOther(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);

            //【自动监察】任务查询  t_TaskAutoMonitoring
            TaskAuto(ref m_list, fromdate, todate, m_OrderListID, MemberID, SchStatus, SchContent, SchMemberNm);


            //【分页】
            List<SearchHistoryVM> m_listd = new List<SearchHistoryVM>();
            int start = Convert.ToInt32((pager.page - 1) * pager.pageSize);
            int end =Convert.ToInt32(start + pager.pageSize)> m_list.Count ? m_list.Count: Convert.ToInt32(start + pager.pageSize) ;
           
            for (int i = start; i < end; i++)
            {
                m_listd.Add(m_list[i]);
                
            }

            Page<SearchHistoryVM> list = new Page<SearchHistoryVM>()
            {
                Items = m_listd,
                ItemsPerPage = pager.pageSize,
                TotalItems = m_list.Count,
                TotalPages = m_list.Count / pager.pageSize,
                CurrentPage = pager.page,
            };

            return list;
        }


        /// <summary>
        /// 在线法庭任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void GetTaskOnlineCase(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(P_PerName_En+';','') +isnull(P_PerName_Cn+';','')  +isnull(D_PerName_En+';','')  +isnull(D_PerName_Cn+';','')  +isnull(Case_No+';','')  +isnull(Representation+';','')  +isnull(Street+';','')  +isnull(BuildName+';','')  +isnull(LotType+';','')  +isnull(LotNo+';','') as content  "));

                //查询表
                sql.Append("   from t_TaskOnlineCase ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(P_PerName_En,'')  +isnull(P_PerName_Cn,'')  +isnull(D_PerName_En,'')  +isnull(D_PerName_Cn,'')  +isnull(Case_No,'')  +isnull(Representation,'')  +isnull(Street,'')  +isnull(BuildName,'')  +isnull(LotType,'')  +isnull(LotNo,'') like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOnlineCaseVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "在线法庭搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,

                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 在线公司任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void GetTaskOnlineCompany(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(ComName_En+';','') +isnull(ComName_Cn+';','')  as content  "));

                //查询表
                sql.Append("   from t_TaskOnlineCompany ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(ComName_En,'')  +isnull(ComName_Cn,'')  like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOnlineCompanyVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "在线公司搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,

                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 在线土地任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void GetTaskOnlineLand(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(LotType+';','') +isnull(LotNo+';','') +isnull(BuildName+';','')+isnull(HouseNO+';','')+isnull(RoomNO+';','')+isnull(SeatNO+';','')+isnull(Floor+';','')+isnull(Street+';','')+isnull(StreetNumber+';','')+isnull(Area+';','')as content  "));

                //查询表
                sql.Append("   from t_TaskOnlineLand ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(LotType,'') +isnull(LotNo,'') +isnull(BuildName,'')+isnull(HouseNO,'')+isnull(RoomNO,'')+isnull(SeatNO,'')+isnull(Floor,'')+isnull(Street,'')+isnull(StreetNumber,'')+isnull(Area,'') like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOnlineLandVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "在线土地搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,

                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 在线信贷任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void GetTaskOnlineLoan(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(Idtype+';','') +isnull(NumberID+';','')  as content  "));

                //查询表
                sql.Append("   from t_TaskOnlineLoan ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(Idtype,'')  +isnull(NumberID,'')  like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOnlineLoanVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "在线信贷搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,

                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        /// <summary>
        /// 在线公共任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void TaskOnlinePublic(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(FullName_En+';','') +isnull(FullName_Cn+';','')  as content  "));

                //查询表
                sql.Append("   from t_TaskOnlinePublic ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(FullName_En,'')  +isnull(FullName_Cn,'')  like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOnlinePublicVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "在线公共搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,

                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 离线法庭任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void TaskOfflineCase(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(P_PerName_En+';','') +isnull(P_PerName_Cn+';','')  +isnull(D_PerName_En+';','')  +isnull(D_PerName_Cn+';','')  +isnull(Case_No+';','')  +isnull(Representation+';','')  +isnull(Street+';','')  +isnull(BuildName+';','')  +isnull(LotType+';','')  +isnull(LotNo+';','') as content  ")); 

                //查询表
                sql.Append("   from t_TaskOfflineCase ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(P_PerName_En,'') +isnull(P_PerName_Cn,'')  +isnull(D_PerName_En,'')  +isnull(D_PerName_Cn,'')  +isnull(Case_No,'')  +isnull(Representation,'')  +isnull(Street,'')  +isnull(BuildName,'')  +isnull(LotType,'')  +isnull(LotNo,'')  like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOfflineCaseVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "离线法庭搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,
                            Remark2 = list[i].Remark1,
                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 离线公司任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void TaskOfflineCompany(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(Name_En+';','') +isnull(Name_Cn+';','')+isnull(RegNo1+';','')+isnull(RegNo2+';','')+isnull(ComAddress+';','') as content  "));

                //查询表
                sql.Append("   from t_TaskOfflineCompany ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(Name_En,'') +isnull(Name_Cn,'')+isnull(RegNo1,'')+isnull(RegNo2,'')+isnull(ComAddress,'')  like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOfflineCompanyVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "离线公司搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,
                            Remark2 = list[i].Remark1,
                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 离线土地任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void TaskOfflineLand(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(LotType+';','') +isnull(LotNo+';','') +isnull(BuildName+';','')+isnull(HouseNO+';','')+isnull(RoomNO+';','')+isnull(SeatNO+';','')+isnull(Floor+';','')+isnull(Street+';','')+isnull(StreetNumber+';','')+isnull(Area+';','')+isnull(Section+';','')+isnull(Subsection+';','') as content  "));

                //查询表
                sql.Append("   from t_TaskOfflineLand ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(LotType,'') +isnull(LotNo,'') +isnull(BuildName,'')+isnull(HouseNO,'')+isnull(RoomNO,'')+isnull(SeatNO,'')+isnull(Floor,'')+isnull(Street,'')+isnull(StreetNumber,'')+isnull(Area,'')+isnull(Section,'')+isnull(Subsection,'') like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOfflineLandVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "离线土地搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,
                            Remark2 = list[i].Remark1,
                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 离线其他任务查询
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void TaskOfflineOther(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"select * ,isnull(SelectContent+';','')  as content  "));

                //查询表
                sql.Append("   from t_TaskOfflineOther ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(" and isnull(SelectContent,'')  like '%{0}%' ", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  Progress = @0  ", Progress);
                }
                if (!string.IsNullOrEmpty(SelectName))
                {
                    sql.Append("and  SelectName = @0  ", SelectName);
                }


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  addtime >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  addtime <= @0  ", todate);
                }

                var list = db.Fetch<TaskOfflineOtherVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].addtime.ToString(),
                            SchMemberNm = list[i].SelectName,
                            SchContent = list[i].content,
                            ProductName = GetProductName(list[i].OrderListID),
                            ProductDetail = "离线其他搜索",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,
                            Remark2 = list[i].Remark1,
                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 自动监察
        /// </summary>
        /// <param name="list">所有查询记录集合</param>
        /// <param name="fromdate">查询开始时间</param>
        /// <param name="todate">查询结束时间</param>
        /// <param name="OrderListID">订单明细id</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="Progress">进度(0:未处理 1:处理中 2:取消 3:完成)</param>
        /// <param name="SchContent">查册内容</param>
        /// <param name="SelectName">查册者</param>
        /// <returns></returns>
        public void TaskAuto(ref List<SearchHistoryVM> mlist, string fromdate = "", string todate = "", string OrderListID = "", string MemberID = "", string Progress = "", string SchContent = "", string SelectName = "")
        {

            try
            {

                Sql sql = new Sql();

                //查询字段
                sql.Append(
                 string.Format(@"SELECT   dbo.t_TaskAutoList.TaskID, dbo.t_TaskAutoMonitoring.TaskDate, dbo.t_TaskAutoMonitoring.OrderListID, 
                dbo.t_TaskAutoMonitoring.Progress, dbo.t_TaskAutoList.Remark, dbo.t_OrderList.ProductName, 
                ISNULL(dbo.t_TaskAutoList.P_PerName_En + ';', '') + ISNULL(dbo.t_TaskAutoList.P_PerName_Cn + ';', '') 
                + ISNULL(dbo.t_TaskAutoList.D_PerName_En + ';', '') + ISNULL(dbo.t_TaskAutoList.D_PerName_Cn + ';', '') 
                + ISNULL(dbo.t_TaskAutoList.Case_No + ';', '') + ISNULL(dbo.t_TaskAutoList.Representation + ';', '') AS SchContent  "));

                //查询表
                sql.Append(@"  FROM      dbo.t_TaskAutoList INNER JOIN
                dbo.t_TaskAutoMonitoring ON dbo.t_TaskAutoList.TaskID = dbo.t_TaskAutoMonitoring.TaskID INNER JOIN
                dbo.t_OrderList ON dbo.t_TaskAutoMonitoring.OrderListID = dbo.t_OrderList.OrderListID ");

                //查询条件

                sql.Append("   where 1=1  ");

                if (!string.IsNullOrEmpty(SchContent))
                {

                    sql.Append(string.Format(@" and (ISNULL(dbo.t_TaskAutoList.P_PerName_En, N'') + ISNULL(dbo.t_TaskAutoList.P_PerName_Cn, N'') 
                + ISNULL(dbo.t_TaskAutoList.D_PerName_En, N'') + ISNULL(dbo.t_TaskAutoList.D_PerName_Cn, N'') 
                + ISNULL(dbo.t_TaskAutoList.Case_No, N'') + ISNULL(dbo.t_TaskAutoList.Representation, N'') LIKE  '%{0}%' )", SchContent));
                }

                if (!string.IsNullOrEmpty(OrderListID))
                {
                    sql.Append("and dbo.t_TaskAutoMonitoring.OrderListID = @0  ", OrderListID);
                }
                if (!string.IsNullOrEmpty(MemberID))
                {
                    sql.Append("and dbo.t_TaskAutoMonitoring.MemberID = @0  ", MemberID);
                }
                if (Progress != "-1")
                {
                    sql.Append("and  dbo.t_TaskAutoMonitoring.Progress = @0  ", Progress);
                }
                //if (!string.IsNullOrEmpty(SelectName))
                //{
                //    sql.Append("and  SelectName = @0  ", SelectName);
                //}


                if (!string.IsNullOrEmpty(fromdate))
                {
                    sql.Append("and  dbo.t_TaskAutoMonitoring.TaskDate >= @0  ", fromdate);
                }

                if (!string.IsNullOrEmpty(todate))
                {
                    sql.Append("and  dbo.t_TaskAutoMonitoring.TaskDate <= @0  ", todate);
                }

                var list = db.Fetch<TaskAutoVM>(sql);

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SearchHistoryVM schVM = new SearchHistoryVM
                        {
                            SchDatetime = list[i].TaskDate,
                            SchMemberNm = "",
                            SchContent = list[i].SchContent,
                            ProductName = list[i].ProductName,
                            ProductDetail = "自动监察",
                            SchResult = "结果",
                            SchStatus = list[i].Progress,
                            Remark1 = list[i].Remark,
                            Remark2 = list[i].Remark1,
                        };

                        mlist.Add(schVM);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion

        #region Common

        /// <summary>
        /// 查询该用户所购买产品的列表
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public List<OrderListModel> getSel(string  MemberID)
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
        /// 根据选择【产品名称】获取订单明细id
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string GetOrderListID(string ProductName)
        {
            try
            {
                //没有选择则为空
                if (ProductName == "全部") return "";


                Sql sql = new Sql().Append(@" 
                    SELECT   *
                    FROM    dbo.t_OrderList 
                    WHERE ProductName =@0", ProductName);

                List<OrderListModel> list = db.Fetch<OrderListModel>(sql);


                return list[0].OrderListID.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 根据 【订单明细id】 获取获取产品名称
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string GetProductName(string OrderListID)
        {
            try
            {

                Sql sql = new Sql().Append(@" 
                    SELECT   *
                    FROM    dbo.t_OrderList 
                    WHERE OrderListID =@0", OrderListID);

                List<OrderListModel> list = db.Fetch<OrderListModel>(sql);

                return list[0].ProductName.ToString();
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }
        }

        #endregion


    }
}

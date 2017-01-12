using Valeo.Lang;
using System;
using System.Collections.Generic;

namespace Valeo.Domain
{
    /// <summary>
    /// 自动监察实体
    /// </summary>
    [Serializable]
    public class AutoMonitorModel
    {

        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderListModel OrderListModel = new OrderListModel();

        /// <summary>
        /// 监察任务信息
        /// </summary>
        public TaskAutoMonitoringModel AutoMonitoringModel = new TaskAutoMonitoringModel();

        /// <summary>
        /// 组别信息
        /// </summary>
        public TaskAutoGroupModel AutoGroupModel = new TaskAutoGroupModel();
        
  
        /// <summary>
        /// 监察条件|报告信息
        /// </summary>
        public List<ReportAutoMonitorModel> ListReportAutoMonitorModel = new List<ReportAutoMonitorModel>();
  
    }


    public class ReportAutoMonitorModel
    {
        /// <summary>
        /// 监察条件信息
        /// </summary>
        public TaskAutoListModel AutoListModel = new TaskAutoListModel();

        /// <summary>
        /// 监察条件信息
        /// </summary>
        public ReportAutoMonitoringModel ReportAutoModel = new ReportAutoMonitoringModel();

        /// <summary>
        /// 报告明细
        /// </summary>
        public List<ResultAutoMonitoringModel> ListResultAutoModel = new List<ResultAutoMonitoringModel>();

    }
}

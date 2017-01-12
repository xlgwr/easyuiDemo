using Valeo.Lang;
using System;
using System.Collections.Generic;

namespace Valeo.Domain
{
    /// <summary>
    /// 自动任务报告实体
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_ReportAutoMonitoring")]
    [PetaPoco.PrimaryKey("ReportID")]
    public class ReportAutoMonitoringModel
    {
        /// <summary>
        /// 报告id (6520160527000001)
        /// </summary>
        public long ReportID { get; set; }

        /// <summary>
        ///会员id
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        ///邮件id
        /// </summary>
        public long EmailID { get; set; }

        /// <summary>
        //任务id
        /// </summary>
        public long TaskID { get; set; }

        /// <summary>
        //组别id
        /// </summary>
        public long TaskGroupID { get; set; }
         
        /// <summary>
        //查询人id
        /// </summary>
        public long TaskListID { get; set; }

        /// <summary>
        ///有没有查到记录
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 添加者
        /// </summary>
        public string adduser { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string upduser { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime? addtime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updtime { get; set; }
    }
     
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 自动监察
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_TaskAutoMonitoring")]
    [PetaPoco.PrimaryKey("TaskID")]
    public class TaskAutoMonitoringModel
    {
        /// <summary>
        /// 任务id (50201605270001)
        /// </summary>
        public long TaskID { get; set; }

        /// <summary>
        /// 订单明细id
        /// </summary>
        public long OrderListID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        /// 进度(0:未处理 1:处理中 2:取消 3:完成)
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 完成人
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 任务日期
        /// </summary>
        public DateTime TaskDate { get; set; }

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

    public class TaskAutoMonitoringReport
    {
        /// <summary>
        /// 任务id (50201605270001)
        /// </summary>
        public long TaskID { get; set; }

        /// <summary>
        /// 订单明细id
        /// </summary>
        public long OrderListID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        /// 进度(0:未处理 1:处理中 2:取消 3:完成)
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 完成人
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 任务日期
        /// </summary>
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// 法庭
        /// </summary>
        public string CourtType { get; set; }

        /// <summary>
        /// 原告英文名   
        /// </summary>
        public string P_PerName_En { get; set; }

        /// <summary>
        /// 原告中文名
        /// </summary>
        public string P_PerName_Cn { get; set; }

        /// <summary>
        /// 被告英文名
        /// </summary>
        public string D_PerName_En { get; set; }

        /// <summary>
        /// 被告中文名
        /// </summary>
        public string D_PerName_Cn { get; set; }

        /// <summary>
        /// 案件编号
        /// </summary>
        public string Case_No { get; set; }

        /// <summary>
        /// 律师楼代表
        /// </summary>
        public string Representation { get; set; }
        /// <summary>
        /// 查册者
        /// </summary>
        public string SelectName { get; set; }

        /// <summary>
        /// 查册者称谓(Mr/Mrs/Ms)
        /// </summary>
        public string SelectSalutation { get; set; }

        /// <summary>
        /// 查册者电话
        /// </summary>
        public string SelectTel { get; set; }

        /// <summary>
        /// 查册者邮箱
        /// </summary>
        public string SelectEmail { get; set; }

        /// <summary>
        /// 备注(会员填的)
        /// </summary>
        public string Remark { get; set; }

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
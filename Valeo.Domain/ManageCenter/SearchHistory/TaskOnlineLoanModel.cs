using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 在线信贷任务表
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_TaskOnlineLoan")]
    [PetaPoco.PrimaryKey("TaskID")]
    public class TaskOnlineLoanModel
    {
        /// <summary>
        /// 任务id (50201605270001)
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// 订单明细id
        /// </summary>
        public long OrderListID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 进度(0:未处理 1:处理中 2:取消 3:完成)
        /// </summary>
        public long Progress { get; set; }

        /// <summary>
        /// 完成人
        /// </summary>
        public string UserID { get; set; }      

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

        /// <summary>
        /// 任务日期
        /// </summary>
        public DateTime? TaskDate { get; set; }

        #region  【查询内容】

        /// <summary>
        /// 证件类别
        /// </summary>
        public string Idtype { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string NumberID { get; set; }

       

        #endregion
    }
    public class TaskOnlineLoanModel2 : TaskOnlineLoanModel
    {
        public string MemberName { get; set; }
        public string SelectContent { get; set; }
    }
}
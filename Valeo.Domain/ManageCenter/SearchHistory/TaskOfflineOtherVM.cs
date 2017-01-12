using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 离线其他任务表
    /// </summary>
    [Serializable]
    public class TaskOfflineOtherVM
    {
        /// <summary>
        /// 任务id (50201605270001)
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// 订单明细id
        /// </summary>
        public string OrderListID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 进度(0:未处理 1:处理中 2:取消 3:完成)
        /// </summary>
        public string Progress { get; set; }

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
        /// 备注(客服填)
        /// </summary>
        public string Remark1 { get; set; }

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
        /// 查册内容
        /// </summary>
        public string content { get; set; }


        #region  【查询内容】

        /// <summary>
        /// 查询内容
        /// </summary>
        public string SelectContent { get; set; }



        #endregion
    }
}
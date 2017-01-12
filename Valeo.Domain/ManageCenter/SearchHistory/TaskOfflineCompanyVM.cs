using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 离线公司任务表
    /// </summary>
    [Serializable]
    public class TaskOfflineCompanyVM
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
        /// 0:公司 1:个人
        /// </summary>
        public string ComOrPer { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string Name_En { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string Name_Cn { get; set; }

        /// <summary>
        /// 公司注册号码/身份证号
        /// </summary>
        public string RegNo1 { get; set; }
        /// <summary>
        /// 商业登记号码/护照号
        /// </summary>
        public string RegNo2 { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string ComAddress { get; set; }


        /// <summary>
        /// 报告类别：公司(0:最近公司年报，1:组织章程大纲及章程细则，2:公司注册证书，3:公司年报-特别指明年份（需手动输入），4:抵押（需注明年份），5:有效地商业/分行登记证核证副本，6:其他，7:商业登记册内资料摘录的核证本，8:商业登记册内资料摘录的电子摘录，9:有效地商业/分行登记证核证副本，10:公司强制性清盘案记录查册） 人个:(11:破产案查册，12:有限公司董事查册，13:其他)
        /// </summary>
        public string ReportType { get; set; }


        #endregion
    }
}
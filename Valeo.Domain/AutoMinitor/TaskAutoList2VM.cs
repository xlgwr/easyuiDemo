using Valeo.Lang;
using System;
using System.Collections.Generic;

namespace Valeo.Domain
{
    /// <summary>
    /// 自动任务各组名单实体
    /// </summary>
    [Serializable]
    public class TaskAutoList2VM
    {
    
        /// <summary>
        /// 产品ID
        /// </summary>
        public long OrderListID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///监察日期
        /// </summary>
        public DateTime AutoDate { get; set; }

        /// <summary>
        ///原告英文名
        /// </summary>
        public string P_PerName_En { get; set; }

        /// <summary>
        ///原告中文名
        /// </summary>
        public string P_PerName_Cn { get; set; }

        /// <summary>
        ///被告英文名
        /// </summary>
        public string D_PerName_En { get; set; }

        /// <summary>
        ///被告中文名
        /// </summary>
        public string D_PerName_Cn { get; set; }

        /// <summary>
        ///案件编号
        /// </summary>
        public string Case_No { get; set; }

        /// <summary>
        ///律师楼代表
        /// </summary>
        public string Representation { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///备注1
        /// </summary>
        public string Remark1 { get; set; }

        /// <summary>
        /// 0:不是第一次 1:第一次
        /// </summary>
        public int First_time { get; set; }



        
    }
     
}

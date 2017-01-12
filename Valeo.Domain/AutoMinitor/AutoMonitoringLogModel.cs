using Valeo.Lang;
using System;
using System.Collections.Generic;

namespace Valeo.Domain
{
    /// <summary>
    /// 搜索日志实体
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_AutoMonitoringLog")]
    [PetaPoco.PrimaryKey("Id")]
    public class AutoMonitoringLogModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///报告id
        /// </summary>
        public long TaskID { get; set; }
         
        /// <summary>
        ///组别id
        /// </summary>
        public long TaskGroupID { get; set; }
      
        /// <summary>
        ///会员id
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        ///搜索日期
        /// </summary>
        public DateTime Search_date { get; set; }

        /// <summary>
        ///传入的搜索人数
        /// </summary>
        public int Number_search { get; set; }

        /// <summary>
        ///返回结果条数
        /// </summary>
        public int Search_result { get; set; }

        /// <summary>
        ///组名
        /// </summary>
        public string Search_group { get; set; }

    }
     
}

using Valeo.Lang;
using System;
using System.Collections.Generic;

namespace Valeo.Domain
{
    /// <summary>
    /// 搜索结果实体
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_ResultAutoMonitoring")]
    [PetaPoco.PrimaryKey("ResultID")]
    public class ResultAutoMonitoringModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long ResultID { get; set; }

        /// <summary>
        ///报告id
        /// </summary>
        public long ReportID { get; set; }
         
        /// <summary>
        ///法庭案件id
        /// </summary>
        public long CaseID { get; set; }

      
    }
     
}

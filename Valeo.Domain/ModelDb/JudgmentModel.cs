
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    /// <summary>
    /// 判决书
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_Judgment")]
    [PetaPoco.PrimaryKey("JudgementID",autoIncrement=true)]
    public class JudgmentModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long JudgementID { get; set; }
        
        
        /// <summary>
        /// 编号
        /// </summary>
        public virtual long Caseid { get; set; }
        
        /// <summary>
        /// 案件编号
        /// </summary>
        public virtual string CaseNo { get; set; }
        
        /// <summary>
        /// 日期
        /// </summary>
        public virtual System.DateTime? ResultDate { get; set; }
        
        /// <summary>
        /// 语言类型(0: 1: 2:)
        /// </summary>
        public virtual int Language { get; set; }
        
        /// <summary>
        /// 结果
        /// </summary>
        public virtual string Results { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
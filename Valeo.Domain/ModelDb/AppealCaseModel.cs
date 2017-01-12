
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    /// <summary>
    /// t_AppealCase(上诉案件)
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_AppealCase")]
    [PetaPoco.PrimaryKey("AppealID",autoIncrement=true)]
    public class AppealCaseModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long AppealID { get; set; }
        
        
        /// <summary>
        /// 案件编号
        /// </summary>
        public virtual string CaseNo { get; set; }
        
        /// <summary>
        /// 上诉案件编号
        /// </summary>
        public virtual string OldCaseNo { get; set; }
        
        /// <summary>
        /// 旧案件日期
        /// </summary>
        public virtual System.DateTime? OldDate { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string adduser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? addtime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string upduser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? updtime { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
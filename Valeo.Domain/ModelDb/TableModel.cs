
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_Table")]
    [PetaPoco.PrimaryKey("TableID",autoIncrement=true)]
    public partial class TableModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long TableID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string NewTable { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string OldTable { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string LanguageCode { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int DspNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Flag { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
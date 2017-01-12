
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_PublicTable")]
    [PetaPoco.PrimaryKey("PublicTableID",autoIncrement=true)]
    public partial class PublicTableModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long PublicTableID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long TableId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long PublicTypeID { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
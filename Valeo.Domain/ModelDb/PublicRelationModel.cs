
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_PublicRelation")]
    [PetaPoco.PrimaryKey("PLRelationID",autoIncrement=true)]
    public partial class PublicRelationModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long PLRelationID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long TableID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long PublicID { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
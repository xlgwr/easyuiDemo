
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Relationships")]
    [PetaPoco.PrimaryKey("Relationid",autoIncrement=true)]
    public class RelationshipsModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Relationid { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid_M { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid_C { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Relation { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Verify { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
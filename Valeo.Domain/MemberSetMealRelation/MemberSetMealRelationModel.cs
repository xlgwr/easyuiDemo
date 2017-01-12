
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberSetMealRelation
{
    [Serializable]
    [PetaPoco.TableName("m_MemberSetMealRelation")]
                [PetaPoco.PrimaryKey("RelationID",autoIncrement=true)]
    public class MemberSetMealRelationModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long RelationID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Type { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string PackProductID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int MemberGradeID { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
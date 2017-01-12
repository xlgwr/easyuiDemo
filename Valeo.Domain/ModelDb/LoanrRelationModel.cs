
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_LoanrRelation")]
    [PetaPoco.PrimaryKey("id",autoIncrement=true)]
    public partial class LoanrRelationModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long id { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long LoanID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Type { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
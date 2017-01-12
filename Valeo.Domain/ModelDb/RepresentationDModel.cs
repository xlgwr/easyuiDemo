
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Representation_D")]
    [PetaPoco.PrimaryKey("id",autoIncrement=true)]
    public class RepresentationDModel
    {
        #region 实体属性

        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long id { get; set; }


        /// <summary>
        /// 案件编号
        /// </summary>
        public virtual long Caseid { get; set; } 
 
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid_R { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int GroupNO { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
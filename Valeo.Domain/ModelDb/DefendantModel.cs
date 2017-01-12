
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Defendant")]
    [PetaPoco.PrimaryKey("id",autoIncrement=true)]
    public class DefendantModel
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
        /// Entityid
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 组别,为了与被告代表对应
        /// </summary>
        public virtual int GroupNO { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_UpdateLog")]
    [PetaPoco.PrimaryKey("Updateid",autoIncrement=true)]
    public class UpdateLogModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Updateid { get; set; }
        
        
        /// <summary>
        /// 案件编号
        /// </summary>
        public virtual long Caseid { get; set; }
        
        /// <summary>
        /// 修改内容
        /// </summary>
        public virtual string UpdateContent { get; set; }
        
        /// <summary>
        /// 修改日期
        /// </summary>
        public virtual System.DateTime? Updtime { get; set; }
        
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual string Upduser { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
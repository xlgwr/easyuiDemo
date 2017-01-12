
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Emails")]
    [PetaPoco.PrimaryKey("Emailid",autoIncrement=true)]
    public class EmailsModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Emailid { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Email { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
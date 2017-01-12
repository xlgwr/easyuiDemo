
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Telephones")]
    [PetaPoco.PrimaryKey("PhoneID",autoIncrement=true)]
    public class TelephonesModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long PhoneID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string PhoneType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string PhoneNumber { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
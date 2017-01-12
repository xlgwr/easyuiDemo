
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Collateral")]
    [PetaPoco.PrimaryKey("CollateralID",autoIncrement=true)]
    public partial class CollateralModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long CollateralID { get; set; }
        
        
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
        public virtual string LandNO { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long AddressID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string CollateralName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Cost { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Path { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Remark { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
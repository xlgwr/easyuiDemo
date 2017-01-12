
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_CollateralShare")]
    [PetaPoco.PrimaryKey("CollateralShareID",autoIncrement=true)]
    public partial class CollateralShareModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long CollateralShareID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long LoanID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string MortgagedSecurity { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string StockCode { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string ShareAmount { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Price { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string TotalValue { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
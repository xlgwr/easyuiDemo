
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Securitie")]
    [PetaPoco.PrimaryKey("SecuritieID",autoIncrement=true)]
    public partial class SecuritieModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long SecuritieID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long LoanID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int MarginFinancingPCheck { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string MarginFinancingPValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int MarginFinancingTCheck { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string MarginFinancingTValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int QuotaCheck { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string QuotaValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int PurchasePowerCheck { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string PurchasePowerValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int T2Check { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int CashCheck { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int NettingCheck { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
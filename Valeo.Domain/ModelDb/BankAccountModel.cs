
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_BankAccount")]
    [PetaPoco.PrimaryKey("BankAccountID",autoIncrement=true)]
    public class BankAccountModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long BankAccountID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Bank { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string AccountType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string AccountName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string AccountNumber { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? DateOpened { get; set; }

       
        
     
        
        #endregion

 

    }
}
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_EmploymentHistory")]
    [PetaPoco.PrimaryKey("Employmentid",autoIncrement=true)]
    public class EmploymentHistoryModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Employmentid { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid_P { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Employer_En { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Employer_Cn { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Department { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Position { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string LenghtServices { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string OfficeAddress { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string JoinedDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Tel1 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Tel2 { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
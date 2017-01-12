
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_SysModule")]
    [PetaPoco.PrimaryKey("Mod_id",autoIncrement=false)]
    public partial class SysModuleModel
    {
        #region 实体属性
        
        /// <summary>
        /// 自编
        /// </summary>
        public virtual string Mod_id { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Mod_nm { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Parentid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Url { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Iconic { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Sort { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Islast { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Version { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Flag { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Status { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Remark { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
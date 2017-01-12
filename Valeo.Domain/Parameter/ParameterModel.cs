
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Parameter
{
    [Serializable]
    [PetaPoco.TableName("m_Parameter")]
                [PetaPoco.PrimaryKey("Paramkey",autoIncrement=false)]
    public class ParameterModel
    {
        #region 实体属性
        
        /// <summary>
        /// 自编
        /// </summary>
        public virtual string Paramkey { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Paramvalue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int? Paramtype { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int? DspNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Adduser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Upduser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? Addtime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? Updtime { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
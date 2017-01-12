
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_PublicType")]
    [PetaPoco.PrimaryKey("PublicTypeID",autoIncrement=true)]
    public partial class PublicTypeModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long PublicTypeID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string LanguageCode { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
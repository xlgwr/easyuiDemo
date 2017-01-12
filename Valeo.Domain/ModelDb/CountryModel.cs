
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_Country")]
    [PetaPoco.PrimaryKey("Countryid",autoIncrement=true)]
    public class CountryModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Countryid { get; set; }
        
        
        /// <summary>
        /// 国名
        /// </summary>
        public virtual string CountryName { get; set; }
        
        /// <summary>
        /// 多语言编码
        /// </summary>
        public virtual string LanguageCode { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual int Sort { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
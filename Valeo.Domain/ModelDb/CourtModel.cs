
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_Court")]
    [PetaPoco.PrimaryKey("CourtID",autoIncrement=true)]
    public class CourtModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long CourtID { get; set; }
        
        
        /// <summary>
        /// 法院名称_英文
        /// </summary>
        public virtual string CourtName_En { get; set; }
        
        /// <summary>
        /// 法院名称_中文
        /// </summary>
        public virtual string CourtName_Cn { get; set; }
        
        /// <summary>
        /// 法院编码
        /// </summary>
        public virtual string CourtCode { get; set; }
        
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Tel { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 1:高等法庭及区域法院及小额钱债审裁处 2:破产案/个人债务重组
        /// </summary>
        public virtual int Type { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 
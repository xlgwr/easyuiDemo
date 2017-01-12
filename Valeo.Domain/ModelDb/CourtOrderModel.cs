
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_CourtOrder")]
    [PetaPoco.PrimaryKey("id",autoIncrement=true)]
    public class CourtOrderModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long id { get; set; }
        
        
        /// <summary>
        /// 编号
        /// </summary>
        public virtual long Caseid { get; set; }
        
        /// <summary>
        /// 案件编号
        /// </summary>
        public virtual string CaseNo { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 判案书文档路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public virtual int type { get; set; }
     
        
        #endregion

 

    }
}
 
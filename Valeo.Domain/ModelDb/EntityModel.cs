
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("s_Entity")]
    [PetaPoco.PrimaryKey("Entityid",autoIncrement=true)]
    public class EntityModel
    {
        public class VarKey
        {
            public const string tablename = "s_Entity";
            public const string PrimaryKey = "Entityid";
        }

        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Entityid { get; set; }
        
        
        /// <summary>
        /// 类别1:P 2:C 3:U(unknown)
        /// </summary>
        public virtual int Type { get; set; }
        
        /// <summary>
        /// 标记1:法庭 2:公司 3:土地 4:信贷 5:公共
        /// </summary>
        public virtual int Flag { get; set; }
        
        /// <summary>
        /// 参数(当flag=1时 这里将保存0:原告,1:被告 2:法官 3:原告律师 4:被告律师等)
        /// </summary>
        public virtual int Param1 { get; set; }

        public virtual int Same { get; set; }
        
     
        
        #endregion

 

    }
}
 
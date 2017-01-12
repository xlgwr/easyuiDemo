using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("m_Relation")]
    [PetaPoco.PrimaryKey("Relationid", autoIncrement = true)]
    public class RelationModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Relationid { get; set; }

        /// <summary>
        /// 关系名称
        /// </summary>
        public virtual string RelationNam { get; set; }

        /// <summary>
        /// 多语言编码
        /// </summary>
        public virtual string LanguageCode { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual long Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}

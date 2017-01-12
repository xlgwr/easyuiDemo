using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_CompanyChange")]
    [PetaPoco.PrimaryKey("Changeid", autoIncrement = true)]
    public class CompanyChangeModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Changeid { get; set; }

        /// <summary>
        /// 公司Entityid
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 0:英文 1:简体 2:繁体
        /// </summary>
        public virtual long Language { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual long Sort { get; set; }

        /// <summary>
        /// 变更内容_英文
        /// </summary>
        public virtual string ChangeContent_En { get; set; }

        /// <summary>
        /// 变更内容_中文
        /// </summary>
        public virtual string ChangeContent_Cn { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public virtual string EffectiveDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}

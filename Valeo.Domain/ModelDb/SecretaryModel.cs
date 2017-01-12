using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_Secretary")]
    [PetaPoco.PrimaryKey("Secretaryid", autoIncrement = true)]
    public class SecretaryModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Secretaryid { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public virtual long Entityid_C { get; set; }

        /// <summary>
        /// 董事ID
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 秘书开始日期
        /// </summary>
        public virtual string StartDate { get; set; }

        /// <summary>
        /// 秘书结束日期
        /// </summary>
        public virtual string EndDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
       
    }
}

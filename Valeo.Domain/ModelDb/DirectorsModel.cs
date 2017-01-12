using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_Directors")]
    [PetaPoco.PrimaryKey("Directorsid", autoIncrement = true)]
    public class DirectorsModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Directorsid { get; set; }

        /// <summary>
        /// Entityid_C公司id
        /// </summary>
        public virtual long Entityid_C { get; set; }

        /// <summary>
        /// Entityid董事id
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 董事开始日期
        /// </summary>
        public virtual string StartDate { get; set; }

        /// <summary>
        /// 董事结束日期
        /// </summary>
        public virtual string EndDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}

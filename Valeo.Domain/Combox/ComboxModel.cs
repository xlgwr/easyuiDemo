using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    [Serializable]
    [PetaPoco.TableName("m_Combox")]
    [PetaPoco.PrimaryKey("ComboxId", autoIncrement = false)]
    public class ComboxModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ComboxId { get; set; }

        /// <summary>
        /// 下拉项名称
        /// </summary>
        public string ComboxName { get; set; }
        /// <summary>
        /// 多语言编号
        /// </summary>
        public string LanguageCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DspNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}

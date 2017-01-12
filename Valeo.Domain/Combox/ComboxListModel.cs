using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Combox
{
    [Serializable]
    [PetaPoco.TableName("m_ComboxList")]
    [PetaPoco.PrimaryKey("ComboxListId", autoIncrement = false)]
    public class ComboxListModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ComboxListId { get; set; }

        /// <summary>
        /// 主表id 下拉项id
        /// </summary>
        public long ComboxId { get; set; }

        /// <summary>
        /// 下拉列表键（下拉项id+ComboxListKey为主键）
        /// </summary>
        public long ComboxListKey { get; set; }
        /// <summary>
        /// 下拉列表值
        /// </summary>
        public string ComboxListName { get; set; }
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

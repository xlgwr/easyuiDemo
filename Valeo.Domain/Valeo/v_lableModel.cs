using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 标签模板
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_lableModel")]
    [PetaPoco.PrimaryKey("lableID", autoIncrement = true)]
    public class v_lableModel : vBase
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public double lableID { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [StringLength(64)]
        public string lableName { get; set; }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string path { get; set; }
    }
}

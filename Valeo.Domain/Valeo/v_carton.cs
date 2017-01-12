using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 纸箱
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_carton")]
    [PetaPoco.PrimaryKey("cartonNO", autoIncrement = false)]
    public class v_carton : vBase
    {
        /// <summary>
        /// 纸箱型号 key
        /// </summary>
        [Key]
        [StringLength(64)]
        public string cartonNO { get; set; }
        /// <summary>
        /// 纸箱尺寸
        /// </summary>
        [StringLength(128)]
        public string cartonSize { get; set; }
        /// <summary>
        /// 卡板类型
        /// </summary>
        [StringLength(64)]
        public string cardType { get; set; }

        /// <summary>
        /// 卡板可放箱数
        /// </summary>
        public int pcs { get; set; }

    }
}

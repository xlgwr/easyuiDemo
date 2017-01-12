using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 材料
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_material")]
    [PetaPoco.PrimaryKey("partNO", autoIncrement = false)]
    public class v_material : vBase
    {
        #region Model
        /// <summary>
        /// Valeo料号
        /// </summary>
        [Key]
        [StringLength(64)]
        public string partNO { get; set; }
        /// <summary>
        /// 货名
        /// </summary>

        [StringLength(255)]
        public string partName { get; set; }
        /// <summary>
        /// 客户料号
        /// </summary>

        [StringLength(64)]
        public string customerPartNO { get; set; }
        /// <summary>
        /// 使用纸箱型号
        /// </summary>

        [StringLength(64)]
        public string cartonType { get; set; }
        /// <summary>
        /// 每箱数量
        /// </summary>
        public int pcsNumber { get; set; }
        /// <summary>
        /// 单重kg
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        #endregion Model
    }
}

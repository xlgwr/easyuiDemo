using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 卡板
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_card")]
    [PetaPoco.PrimaryKey("cardNO", autoIncrement = false)]
    public class v_card : vBase
    {
        /// <summary>
        /// 卡板型号 key
        /// </summary>
        [Key]
        [StringLength(64)]
        public string cardNO { get; set; }
        /// <summary>
        /// 卡板尺寸
        /// </summary>
        [StringLength(128)]
        public string cardSize { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        [StringLength(64)]
        public string transType { get; set; }
    
    }
}

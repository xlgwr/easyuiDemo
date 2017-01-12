using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 简称全称设定
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("m_ShortName")]
    [PetaPoco.PrimaryKey("Shortid", autoIncrement = true)]
    public class ShortNameModel
    {
        /// <summary>
        /// 主建 参数KEY(自编)
        /// </summary>
        public long Shortid { get; set; }

        /// <summary>
        /// 类型(0:名称/1:地址)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 全称
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// 替换顺序
        /// </summary>
        public int Sort { get; set; }


    }
}

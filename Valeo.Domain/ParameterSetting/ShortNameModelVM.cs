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
    public class ShortNameModelVM
    {
        /// <summary>
        /// 主建 参数KEY(自编)
        /// </summary>
        public string Shortid { get; set; }

        /// <summary>
        /// 类别(0:名称/1:地址)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 类别(0:名称/1:地址)
        /// </summary>
        public string TypeVM { get; set; }

        /// <summary>
        /// 缩写
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 全写
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// 替换顺序
        /// </summary>
        public int Sort { get; set; }


    }
}

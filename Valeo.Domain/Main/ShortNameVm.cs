using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo
{
    public class ShortNameVM
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 缩写 
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
 
    }
}

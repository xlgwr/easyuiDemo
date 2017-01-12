using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo
{
    public class WordVM
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 缩写 
        /// </summary>
        public string WordKey { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string WordValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

 
    }
}

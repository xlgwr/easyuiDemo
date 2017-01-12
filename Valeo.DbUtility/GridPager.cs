using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo
{
    public class GridPager
    {
        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int totalRows { get; set; }


        /// <summary>
        /// 每页行数
        /// </summary>
        public long pageSize { get; set; }
        /// <summary>
        /// 当前页是第几页
        /// </summary>
        public long page { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public long totalPages { get; set; } 

  
    }
}
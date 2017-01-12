using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 自动检索信息
    /// </summary>
    [Serializable]
    public class TaskAutoVM
    {

        /// <summary>
        /// 搜索  时间+日期
        /// </summary>
        public string TaskDate { get; set; }

        /// <summary>
        /// 搜索者
        /// </summary>
        public string SchMemberNm { get; set; }

        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SchContent { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品详细-【产品名称-次数】
        /// </summary>
        public string ProductDetail { get; set; }

        /// <summary>
        /// 搜索结果
        /// </summary>
        public string SchResult { get; set; }

        /// <summary>
        /// 搜索状态
        /// </summary>
        public string Progress { get; set; }       

        /// <summary>
        /// 前台备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 后台备注
        /// </summary>
        public string Remark1 { get; set; }
    }
}

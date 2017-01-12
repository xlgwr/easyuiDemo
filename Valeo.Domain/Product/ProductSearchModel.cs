using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Product
{
    public class ProductSearchModel
    {
        /// <summary>
        /// 产品种类
        /// </summary>
        public int BigType { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public int SmallType { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo { get; set; }
        /// <summary>
        /// 会员级别ID
        /// </summary>
        public int MemberGradeId { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberGrade
{
    public class RightKeyModel
    {
        /// <summary>
        /// 权限参数名key(
        /// 是否可搜法庭,
        /// 是否可搜土地,
        /// 是否可搜公司,
        /// 是否可搜信贷,
        /// 是否可搜其他,
        /// 可看交易金额,
        /// 搜索记录打印导出,
        /// 可看查土地必填地址,
        /// 查土址必填大厦名,
        /// 在线法庭精确查找，
        /// 在线法庭模糊查找，
        /// 是否可欠费)我们写在表中
        /// </summary>
        public string RightKey { get; set; }

        /// <summary>
        /// 多语言编码
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// 备注(权限说明)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注1(权限详细说明)
        /// </summary>
        public string Remark1 { get; set; }

        /// <summary>
        /// 0:系统固定权限 1:用户可改权限
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public string DspNo { get; set; }
    }
}

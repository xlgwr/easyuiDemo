using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberGradeRight
{
    /// <summary>
    /// 级别权限显示
    /// </summary>
    public class MemberGradeRightVM
    {
        /// <summary>
        /// key
        /// </summary>
        public string RightKey { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public long RightValue { get; set; }
        /// <summary>
        /// 多语言编码
        /// </summary>
        public string LanguageCode { get; set; }
        /// <summary>
        /// 多语言文本
        /// </summary>
        public string LanguageText { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public string DspNo { get; set; }
    }
}

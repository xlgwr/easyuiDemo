using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberRight
{
    /// <summary>
    /// 会员权限
    /// </summary>
    public class MemberRightModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long MemberRightID { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 权限参数名key(是否可搜法庭,是否可搜土地,是否可搜公司,是否可搜信贷,是否可搜其他,可看交易金额,搜索记录打印导出,允许打印发票,可看发票金额,查土地必填地址,查土址必填大厦名,在线法庭精确查找，在线法庭模糊查找，是否可欠费)
        /// </summary>
        public string RightKey { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public long RightValue { get; set; }

        /// <summary>
        /// 多语言编码
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// 多语言文本
        /// </summary>
        [ResultColumn]
        public string LanguageText { get; set; }

        /// <summary>
        /// 备注(权限说明)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public long DspNo { get; set; }
    }

    /// <summary>
    /// 会员权限
    /// </summary>
    public class MemberRightKeyModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long MemberRightID { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 权限参数名key(是否可搜法庭,是否可搜土地,是否可搜公司,是否可搜信贷,是否可搜其他,可看交易金额,搜索记录打印导出,允许打印发票,可看发票金额,查土地必填地址,查土址必填大厦名,在线法庭精确查找，在线法庭模糊查找，是否可欠费)
        /// </summary>
        public string RightKey { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public long RightValue { get; set; }


        /// <summary>
        /// 多语言编码
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// 多语言文本
        /// </summary>
        [ResultColumn]
        public string LanguageText { get; set; }

        /// <summary>
        /// 备注(权限说明)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public long DspNo { get; set; }

        /// <summary>
        /// 父节点ID,权限表没有第一级ID,第一级菜单为 RightMenuEnum
        /// </summary>
        public string ParentKey { get; set; }

    }
}

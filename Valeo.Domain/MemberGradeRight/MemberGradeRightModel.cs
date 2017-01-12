using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberGradeRight
{
    [Serializable]
    [PetaPoco.TableName("m_MemberGradeRight")]
    [PetaPoco.PrimaryKey("MemberGradeRightID")]
    public class MemberGradeRightModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long MemberGradeRightID { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public long MemberGradeID { get; set; }
       
        /// <summary>
        /// 权限参数名key(
        /// 是否可搜法庭,是否可搜土地,是否可搜公司,
        /// 是否可搜信贷,是否可搜其他,可看交易金额,
        /// 搜索记录打印导出,允许打印发票,可看发票金额,
        /// 查土地必填地址,查土址必填大厦名,在线法庭精确查找，
        /// 在线法庭模糊查找，是否可欠费)
        /// </summary>
        public string RightKey { get; set; }

        public string LanguageCode { get; set; }

        public long DspNo { get; set; }
        /// <summary>
        /// 是否有权限
        /// </summary>
        public long RightValue { get; set; }


        /// <summary>
        /// 备注(权限说明)
        /// </summary>
        public string Remark { get; set; }

        
    }
}

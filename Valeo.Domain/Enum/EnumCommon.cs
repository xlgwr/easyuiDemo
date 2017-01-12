using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Enum
{
    public class EnumCommon
    {
        /// <summary>
        /// 活跃度状态
        /// </summary>
        public enum ActivityEnum
        {
            /// <summary>
            /// 活跃
            /// </summary>
            Motion = 1,
            /// <summary>
            /// 不活跃
            /// </summary>
            Prohibit = 0
        }

        /// <summary>
        /// 启用禁用
        /// </summary>
        public enum StateEnum
        {
            /// <summary>
            /// 启用
            /// </summary>
            Enable = 1,
            /// <summary>
            /// 禁用
            /// </summary>
            Disable = 0
        }

        /// <summary>
        /// 称呼
        /// </summary>
        public enum CalledEnum
        {
            Mr = 1,
            Mrs = 2,
            Ms = 3
        };

        /// <summary>
        /// 支付类型
        /// </summary>
        public enum PaymentTypeEnum
        {
            /// <summary>
            /// 网上支付
            /// </summary>
            Cash = 1,
            /// <summary>
            /// 现金
            /// </summary>
            Cheque = 2,
            /// <summary>
            /// 支票
            /// </summary>
            Third = 3
        }

        /// <summary>
        /// 会员类型 0：个人  1：公司
        /// </summary>
        public enum MemberTypeEnum
        {
            /// <summary>
            /// 个人
            /// </summary>
            INDIVIDUAL = 0,
            /// <summary>
            /// 公司
            /// </summary>
            COMPANY = 1
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public enum BusinessTypeEnum
        {
            BTYPE1 = 1,
            BTYPE2 = 2,
            BTYPE3 = 3
        }

        /// <summary>
        /// 性别 0:男 1:女 2:其他
        /// </summary>
        public enum GenderEnum
        {
            /// <summary>
            /// 男
            /// </summary>
            Men = 0,
            /// <summary>
            /// 女
            /// </summary>
            Women = 1,
            /// <summary>
            /// 其他
            /// </summary>
            Otrher = 2
        }

        /// <summary>
        /// 婚姻状态 0;未婚 1：已婚 2：离婚 3:鳏寡 4、其他
        /// </summary>
        public enum MaritalStatusEnum
        {
            /// <summary>
            /// 未婚
            /// </summary>
            Unmarried = 0,
            /// <summary>
            /// 已婚
            /// </summary>
            Married = 1,
            /// <summary>
            /// 离婚
            /// </summary>
            Divorce = 2,
            /// <summary>
            /// 鳏寡
            /// </summary>
            Widowed = 3,
            /// <summary>
            /// 其他
            /// </summary>
            Otrher = 4
        }

        /// <summary>
        /// 是否上市(0:未上市 1:已上市)
        /// </summary>
        public enum ListedEnum
        {
            /// <summary>
            /// 未上市
            /// </summary>
            NotListed = 0,
            /// <summary>
            /// 已上市
            /// </summary>
            AlreadyListed = 1
        }
    }
}

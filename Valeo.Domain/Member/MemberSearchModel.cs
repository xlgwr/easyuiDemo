using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Member
{
    public class MemberSearchModel
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public int MemberType { get; set; }
        /// <summary>
        /// 模糊查询 英文 简体 繁体
        /// </summary>
        public string MemberNameAll { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public int MemberGradeID { get; set; }

        /// <summary>
        /// 会员状态
        /// </summary>
        public int Enable { get; set; }

    }
}

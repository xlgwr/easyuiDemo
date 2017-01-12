using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberGrade
{
    public class RightKeyVM
    {
        /// <summary>
        /// 级别ID
        /// </summary>
        public long MemberGradeID { get; set; }

        /// <summary>
        /// 级别权限ID
        /// </summary>
        public long MemberGradeRightID { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string MemberGrade { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 权限参数KEY
        /// </summary>
        public string RightKey { get; set; }

        /// <summary>
        /// 多语言KEY
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public string DspNo { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string RightValue { get; set; }

        /// <summary>
        /// 添加用户
        /// </summary>
        public virtual string Adduser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual string Addtime { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public virtual string Upduser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual string Updtime { get; set; }
        
    }
}

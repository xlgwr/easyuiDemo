using Valeo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Member
{
    /// <summary>
    /// 会员操作日志
    /// </summary>
    public class MemberLogVM
    {
        
         /// <summary>
        /// ID 
        /// </summary>
        public long LogID { get; set; }

        /// <summary>
        /// ID 
        /// </summary>
        public long MemberID { get; set;}

        /// <summary>
        /// 账号
        /// </summary>
        public string MemberName { get; set;}

        /// <summary>
        /// 名称（中文）
        /// </summary>
        public string FullName_Cn { get; set;}

        /// <summary>
        /// 名称（繁体）
        /// </summary>
        public string FullName_Tm { get; set; }

        /// <summary>
        /// 界面ID
        /// </summary>
        public string Form { get; set; }

        


        /// <summary>
        /// 操作分类(0:新建 1:修改 2:删除 3:查询等等)
        /// </summary>
        public long OperateType { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public string AddDateTime { get; set; }

        public string AddDateTimeVM { get {
            return DataConvert.DateTime2Format(AddDateTime, Enums.DateTimeFormat.DateTime); 
        } }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string AddDateTimeS { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string AddDateTimeE { get; set; }
        
        /// <summary>
        /// 姓（英文）
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 名（英文）
        /// </summary>
        public string GivenNames { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string FullName_En { 
            get {
                return Surname + GivenNames;
                } 
        }

        public string type { get; set; }

        /// <summary>
        /// 公司 CN
        /// </summary>
        public string CFullName_Cn { get; set; }
        /// <summary>
        /// 公司 TM
        /// </summary>
        public string CFullName_Tm { get; set; }
        /// <summary>
        /// 公司 EN
        /// </summary>
        public string CFullName_En { get; set; }
    }
}

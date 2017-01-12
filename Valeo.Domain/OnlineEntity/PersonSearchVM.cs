using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class PersonSearchVM
    {
        /// <summary>
        /// 中文
        /// </summary>
        public string FullName_En { get; set; }

        /// <summary>
        /// 英文
        /// </summary>
        public string FullName_Cn { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string GivenNames { get; set; }

        /// <summary>
        /// 别名En
        /// </summary>
        public virtual string Alias_En { get; set; }

        /// <summary>
        /// 别名Cn
        /// </summary>
        public virtual string Alias_Cn { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public long? Gender { get; set; }

        /// <summary>
        /// 出生日期开始
        /// </summary>
        public string BirthdateStart { get; set; }

        /// <summary>
        /// 出生日期结束
        /// </summary>
        public string BirthdateEnd { get; set; }

        /// <summary>
        /// 婚姻状态 0;未婚 1：已婚 2：离婚 3:鳏寡 4、其他
        /// </summary>
        public long MaritalStatus { get; set; }

        /// <summary>
        /// 国籍(Countryid)
        /// </summary>
        public long Nationality { get; set; }

        /// <summary>
        /// 出生地
        /// </summary>
        public string PlaceBirth { get; set; }

        /// <summary>
        /// 汉字编码
        /// </summary>
        public string ChineseCode { get; set; }

        public string EntityId { get; set; }

    }
}

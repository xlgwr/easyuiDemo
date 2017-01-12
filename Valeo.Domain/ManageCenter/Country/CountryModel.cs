using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ManageCenter.Country
{
    [Serializable]
    [PetaPoco.TableName("m_Country")]
    [PetaPoco.PrimaryKey("Countryid")]
    public class CountryModel
    {
        /// <summary>
        /// 国家ID
        /// </summary>
        public string Countryid { get; set; }

        /// <summary>
        /// 国家名
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// 多语言编码
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }

        public class VarKey
        {
            public const string tablename = "m_Country";
            public const string MemberID = "Countryid";
            public const string MemberName = "CountryName";
            public const string MemberGradeID = "LanguageCode";
            public const string Password = "Remark";
            public const string Enable = "Sort";
        }
    }
}

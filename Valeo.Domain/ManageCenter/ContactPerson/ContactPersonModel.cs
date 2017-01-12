using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{

    [Serializable]
    [PetaPoco.TableName("m_ContactPerson")]
    [PetaPoco.PrimaryKey("ContactPersonID")]
    public class ContactPersonModel
    {
        /// <summary>
        /// 联系人 ID
        /// </summary>
        public string ContactPersonID { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public string MemberComanyID { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string GivenNames { get; set; }

        /// <summary>
        /// 称呼(Mr/Mrs/Ms)
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// 中文姓名
        /// </summary>
        public string FullName_Cn { get; set; }

        /// <summary>
        /// 繁体姓名
        /// </summary>
        public string FullName_Tm { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficeTel1 { get; set; }

        /// <summary>
        /// 办公电话区号
        /// </summary>
        public string AreaCodeOfficeTel1 { get; set; }

        /// <summary>
        /// 办公电话直线
        /// </summary>
        public string OfficeTel2 { get; set; }

        /// <summary>
        /// 办公电话直线区号
        /// </summary>
        public string AreaCodeOfficeTel2 { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string AreaCodeMobilePhone { get; set; }

        /// <summary>
        /// 传真号码
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 传真号码区号
        /// </summary>
        public string AreaCodeFax { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 名片照片
        /// </summary>
        public string PicPath1 { get; set; }

        /// <summary>
        /// 其他文件
        /// </summary>
        public string PicPath2 { get; set; }

        /// <summary>
        /// 标志(收发票人)0:默认 1:收票人
        /// </summary>
        public int Default { get; set; }


        public class VarKey
        {
            public const string tablename = "m_contactperson";
            public const string membercomanyid = "membercomanyid";
            public const string surname = "surname";
            public const string givennames = "givennames";
            public const string salutation = "salutation";
            public const string fullname_cn = "fullname_cn";
            public const string fullname_tm = "fullname_tm";
            public const string position = "position";
            public const string department = "department";
            public const string officetel1 = "officetel1";
            public const string officetel2 = "officetel2";
            public const string mobilephone = "mobilephone";
            public const string fax = "fax";
            public const string email = "email";
            public const string picpath1 = "picpath1";
            public const string picpath2 = "picpath2";
            public const string Default = "default";


            public const string areacodeofficetel1 = "areacodeofficetel1";
            public const string areacodeofficetel2 = "areacodeofficetel2";
            public const string areacodefax = "areacodefax";
            public const string areacodemobilephone = "areacodemobilephone";
        }
    }
}

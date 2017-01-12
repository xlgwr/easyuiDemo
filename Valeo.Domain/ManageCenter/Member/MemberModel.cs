using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    public class MemberModelVM : MemberModel {
        public string SelectSalutation
        {
            get
            {
                return Salutation;
            }
        }
        /// <summary>
        ///  Mr = 1,
        ///  Mrs = 2,
        ///  Ms = 3
        /// </summary>
        public string SalutationVM
        {
            get
            {
                if (!string.IsNullOrEmpty(Salutation))
                {
                    var tocase = Salutation.Trim();

                    switch (tocase)
                    {
                        case "1":
                            return "Mr";
                        case "2":
                            return "Mrs";
                        case "3":
                            return "Ms";
                        default:
                            break;
                    }

                }

                return Salutation;
            }
        }
        private static string _memberNameVM = string.Empty;
        public string MemberNameAllVM
        {
            get { return _memberNameVM; }
            set
            {
                _memberNameVM = value;
                if (!string.IsNullOrEmpty(value))
                {
                    _memberNameVM = value.Replace("    ", " ");
                    _memberNameVM = _memberNameVM.Replace("   ", " ");
                    _memberNameVM = _memberNameVM.Replace("  ", " ");
                    if (!string.IsNullOrEmpty(SalutationVM))
                    {
                        _memberNameVM = SalutationVM + " " + _memberNameVM;
                    }
                }
            }
        }

        public string _MemberAddress = string.Empty;
        public string MemberAddress
        {
            get
            {
                return _MemberAddress;
            }
            set
            {
                _MemberAddress = value;
                if (!string.IsNullOrEmpty(value))
                {
                    _MemberAddress = value.Replace("    ", " ");
                    _MemberAddress = _MemberAddress.Replace("   ", " ");
                    _MemberAddress = _MemberAddress.Replace("  ", " ");
                }
            }
        }
        public string ContactPerson { get; set; }
    }
    /// <summary>
    /// 消息信息实体
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("m_Member")]
    [PetaPoco.PrimaryKey("MemberID")]
    public class MemberModel
    {
        /// <summary>
        /// 会员id(20160527001)
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 用户名会员帐号(唯一)
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public string MemberGradeID { get; set; }

        /// <summary>
        /// 会员密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public String RemainingSum { get; set; }

        /// <summary>
        /// 开票日
        /// </summary>
        public String InvoiceDate { get; set; }

        /// <summary>
        ///0:个人 1:公司
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 公司会员资料id
        /// </summary>
        public String MemberComanyID { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public String Surname { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public String GivenNames { get; set; }

        /// <summary>
        /// 称呼(Mr/Mrs/Ms)
        /// </summary>
        public String Salutation { get; set; }

        /// <summary>
        /// 中文姓名
        /// </summary>
        public String FullName_Cn { get; set; }

        /// <summary>
        /// 繁体姓名
        /// </summary>
        public String FullName_Tm { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public String RoomNO { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public String Floor { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public String Street { get; set; }

        /// <summary>
        /// 街道号
        /// </summary>
        public String StreetNumber { get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public String BuildName { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public String PostalCode { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public String Area { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// 座号
        /// </summary>
        public String SeatNO { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public String Province { get; set; }

        /// <summary>
        /// 国家(从国家表中去选)
        /// </summary>
        public String CountryID { get; set; }

        /// <summary>
        /// 屋号
        /// </summary>
        public String HouseNO { get; set; }

        /// <summary>
        /// 家庭电话
        /// </summary>
        public String HomeTel { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public String OfficeTel { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public String MobilePhone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// 目的1:Know my client
        /// </summary>
        public String Purpose1 { get; set; }

        /// <summary>
        /// 目的2:Know my family / friends / relatives
        /// </summary>
        public String Purpose2 { get; set; }

        /// <summary>
        /// 目的3:Know my employees - employment vetting / Selection of Contractors and vendors
        /// </summary>
        public String Purpose3 { get; set; }

        /// <summary>
        /// 目的4:Lending / Giving credit to Customers
        /// </summary>
        public String Purpose4 { get; set; }

        /// <summary>
        /// 目的5:Renting out your property
        /// </summary>
        public String Purpose5 { get; set; }

        /// <summary>
        /// 目的6:Legal proceedings
        /// </summary>
        public String Purpose6 { get; set; }

        /// <summary>
        /// 目的6:Others (please specify)
        /// </summary>
        public String Purpose7 { get; set; }

        /// <summary>
        /// 获取途径1:Referral 
        /// </summary>
        public String Pathway1 { get; set; }

        /// <summary>
        /// 获取途径2:Recommend by friends
        /// </summary>
        public String Pathway2 { get; set; }

        /// <summary>
        /// 获取途径3:Media: online marketing
        /// </summary>
        public String Pathway3 { get; set; }

        /// <summary>
        /// 获取途径4:Media: offline marketing
        /// </summary>
        public String Pathway4 { get; set; }

        /// <summary>
        /// 获取途径5:Internet searching
        /// </summary>
        public String Pathway5 { get; set; }

        /// <summary>
        /// 获取途径6:Others (please specify)
        /// </summary>
        public String Pathway6 { get; set; }

        /// <summary>
        /// 照片路径1
        /// </summary>
        public String PicPath1 { get; set; }

        /// <summary>
        /// 照片路径2
        /// </summary>
        public String PicPath2 { get; set; }

        /// <summary>
        /// 照片路径3
        /// </summary>
        public String PicPath3 { get; set; }

        /// <summary>
        /// 支付方式:默认为空，下拉菜单选择，选项为1:现金、2:支票、3:第三方机构
        /// </summary>
        public String PaymentWay { get; set; }

        /// <summary>
        /// 是否接受协议(0:不接受,1:接受)
        /// </summary>
        public String AcceptAgreement { get; set; }

        /// <summary>
        /// 邮箱验证(0:未通过 1:通过)
        /// </summary>
        public String EmailVerification { get; set; }

        /// <summary>
        /// 最后搜索时间
        /// </summary>
        public DateTime? LastSeachTime { get; set; }

        /// <summary>
        /// 会员是否可用
        /// </summary>
        public String Enable { get; set; }

        /// <summary>
        /// 添加者
        /// </summary>
        public string adduser { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string upduser { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime? addtime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updtime { get; set; }

        public class VarKey
        {
            public const string tablename = "m_member";
            public const string memberid = "memberid";
            public const string membername = "membername";
            public const string membergradeid = "membergradeid";
            public const string password = "password";
            public const string remainingsum = "remainingsum";
            public const string invoicedate = "invoicedate";
            public const string type = "type";
            public const string membercomanyid = "membercomanyid";
            public const string surname = "surname";
            public const string givennames = "givennames";
            public const string salutation = "salutation";
            public const string fullname_cn = "fullname_cn";
            public const string fullname_tm = "fullname_tm";
            public const string buildname = "buildname";
            public const string street = "street";
            public const string streetnumber = "streetnumber";
            public const string seatno = "seatno";
            public const string floor = "floor";
            public const string roomno = "roomno";
            public const string houseno = "houseno";
            public const string area = "area";
            public const string city = "city";
            public const string province = "province";
            public const string countryid = "countryid";
            public const string postalcode = "postalcode";
            public const string hometel = "hometel";
            public const string officetel = "officetel";
            public const string mobilephone = "mobilephone";
            public const string email = "email";
            public const string purpose1 = "purpose1";
            public const string purpose2 = "purpose2";
            public const string purpose3 = "purpose3";
            public const string purpose4 = "purpose4";
            public const string purpose5 = "purpose5";
            public const string purpose6 = "purpose6";
            public const string purpose7 = "purpose7";
            public const string pathway1 = "pathway1";
            public const string pathway2 = "pathway2";
            public const string pathway3 = "pathway3";
            public const string pathway4 = "pathway4";
            public const string pathway5 = "pathway5";
            public const string pathway6 = "pathway6";
            public const string picpath1 = "picpath1";
            public const string picpath2 = "picpath2";
            public const string picpath3 = "picpath3";
            public const string paymentway = "paymentway";
            public const string acceptagreement = "acceptagreement";
            public const string emailverification = "emailverification";
            public const string lastseachtime = "lastseachtime";
            public const string enable = "enable";
            public const string adduser = "adduser";
            public const string upduser = "upduser";
            public const string addtime = "addtime";
            public const string updtime = "updtime";
        }

    }
}

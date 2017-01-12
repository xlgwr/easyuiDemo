using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain 
{

    [Serializable]
    [PetaPoco.TableName("m_MemberComany")]
    [PetaPoco.PrimaryKey("MemberComanyID")]
    public class MemberComanyModel
    {
        /// <summary>
        /// 公司 ID
        /// </summary>
        public string MemberComanyID { get; set; }
        /// <summary>
        /// 公司名 EN
        /// </summary>
        public string FullName_En { get; set; }
        /// <summary>
        /// 公司名 Tm
        /// </summary>
        public string FullName_Tm { get; set; }
        /// <summary>
        /// 公司名 Cn
        /// </summary>
        public string FullName_Cn { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }
        /// <summary>
        /// 营业执照号码
        /// </summary>
        public string CIBRNO { get; set; }
        /// <summary>
        /// 大厦名称
        /// </summary>
        public string BuildName { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 街道号
        /// </summary>
        public string StreetNumber { get; set; }
        /// <summary>
        /// 座号
        /// </summary>
        public string SeatNO { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string RoomNO { get; set; }
        /// <summary>
        /// 屋号
        /// </summary>
        public string HouseNO { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryID { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// 照片路径1(营业执照)
        /// </summary>
        public string PicPath1 { get; set; }
        /// <summary>
        /// 照片路径2(其他照片)
        /// </summary>
        public string PicPath2 { get; set; }
        /// <summary>
        /// 财务牌照号码
        /// </summary>
        public string FinancialLicenseNo { get; set; }
        /// <summary>
        /// 证券牌照号码
        /// </summary>
        public string SecuritiesLicenceNo { get; set; }



        /// <summary>
        /// 财务牌照到期日
        /// </summary>
        public DateTime? FValidityDate { get; set; }

        [ResultColumn]
        public string FValidityDateString
        {
            get
            {

                if (FValidityDate != null)
                {
                    try
                    {
                        return Convert.ToDateTime(FValidityDate).ToString("yyyy-MM-dd");
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }





        /// <summary>
        /// 证券牌照到期日
        /// </summary>
        

        public DateTime? SValidityDate { get; set; }


        [ResultColumn]
        public string SValidityDateString
        {
            get
            {
                if (SValidityDate != null)
                {
                    try
                    {
                        return Convert.ToDateTime(SValidityDate).ToString("yyyy-MM-dd");
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 照片路径3(财务牌照)
        /// </summary>
        public string PicPath3 { get; set; }
        /// <summary>
        /// 照片路径4(证券牌照)
        /// </summary>
        public string PicPath4 { get; set; }

        public class VarKey
        {
            public const string tablename = "m_membercomany";
            public const string membercomanyid = "membercomanyid";
            public const string fullname_en = "fullname_en";
            public const string fullname_tm = "fullname_tm";
            public const string fullname_cn = "fullname_cn";
            public const string businesstype = "businesstype";
            public const string cibrno = "cibrno";
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
            public const string picpath1 = "picpath1";
            public const string picpath2 = "picpath2";
            public const string financiallicenseno = "financiallicenseno";
            public const string securitieslicenceno = "securitieslicenceno";
            public const string fvaliditydate = "fvaliditydate";
            public const string svaliditydate = "svaliditydate";
            public const string picpath3 = "picpath3";
            public const string picpath4 = "picpath4";
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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    public class MasterVM
    {

        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberID { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 公司名En
        /// </summary>
        public string CompanyName_En { get; set; }

        /// <summary>
        /// 公司名Tm
        /// </summary>
        public string CompanyName_Tm { get; set; }

        /// <summary>
        /// 公司名Cn
        /// </summary>
        public string CompanyName_Cn { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 营业执照号码
        /// </summary>
        public string CIBRNO { get; set; }

        /// <summary>
        /// 公司ID 
        /// </summary>
        public string MemberComanyID { get; set; }

        /// <summary>
        /// 公司  座号
        /// </summary>
        public string SeatNO { get; set; }

        /// <summary>
        /// 公司  层数
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 公司  单位
        /// </summary>
        public string RoomNO { get; set; }

        /// <summary>
        /// 公司  大厦名称
        /// </summary>
        public string BuildName { get; set; }

        /// <summary>
        /// 公司  街号
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// 公司  街名
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// 公司  屋号
        /// </summary>
        public string HouseNO { get; set; }

        /// <summary>
        /// 公司  城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 公司  邮编
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 公司  国家
        /// </summary>
        public string CountryID { get; set; }

        /// <summary>
        /// 公司  姓
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 公司  名
        /// </summary>
        public string GivenNames { get; set; }

        /// <summary>
        /// 公司  称谓
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// 公司  姓名Tm 
        /// </summary>
        public string Name_Tm { get; set; }

        /// <summary>
        /// 公司  姓名Cn
        /// </summary>
        public string Name_Cn { get; set; }

        /// <summary>
        /// 公司  部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 公司  职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 公司  办公室总线
        /// </summary>
        public string OfficeTel1 { get; set; }

        /// <summary>
        /// 公司  办公室直线
        /// </summary>
        public string OfficeTel2 { get; set; }

        /// <summary>
        /// 公司  手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 公司  传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 公司  电邮
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 公司  名片
        /// </summary>
        public string CPPicPath1 { get; set; }

        /// <summary>
        /// 公司  文件
        /// </summary>
        public string CPPicPath2 { get; set; }

        /// <summary>
        /// 公司  支付方式
        /// </summary>
        public string PaymentWay { get; set; }

        /// <summary>
        /// 公司  营业执照
        /// </summary>
        public string MCPicPath1 { get; set; }

        /// <summary>
        /// 公司  其他文件
        /// </summary>
        public string MCPicPath2 { get; set; }

        //=============================================================个人

        /// <summary>
        /// 个人  姓
        /// </summary>
        public string GSurname { get; set; }
        /// <summary>
        /// 个人  名
        /// </summary>
        public string GGivenNames { get; set; }
        /// <summary>
        /// 个人  称呼
        /// </summary>
        public string GSalutation { get; set; }
        /// <summary>
        /// 个人  姓名Tm
        /// </summary>
        public string GName_Tm { get; set; }
        /// <summary>
        /// 个人  姓名Cn
        /// </summary>
        public string GName_Cn { get; set; }
        /// <summary>
        /// 个人  座号
        /// </summary>
        public string GSeatNO { get; set; }
        /// <summary>
        /// 个人  层数
        /// </summary>
        public string GFloor { get; set; }
        /// <summary>
        /// 个人  单位
        /// </summary>
        public string GRoomNO { get; set; }
        /// <summary>
        /// 个人  大厦名称
        /// </summary>
        public string GBuildName { get; set; }
        /// <summary>
        /// 个人  街号
        /// </summary>
        public string GStreetNumber { get; set; }
        /// <summary>
        /// 个人  街名
        /// </summary>
        public string GStreet { get; set; }
        /// <summary>
        /// 个人  屋号
        /// </summary>
        public string GHouseNO { get; set; }
        /// <summary>
        /// 个人  城市
        /// </summary>
        public string GCity { get; set; }
        /// <summary>
        /// 个人  邮编
        /// </summary>
        public string GPostalCode { get; set; }
        /// <summary>
        /// 个人  国家
        /// </summary>
        public string GCountryID { get; set; }
        /// <summary>
        /// 个人  住宅电话
        /// </summary>
        public string GHomeTel { get; set; }
        /// <summary>
        /// 个人  办公电话
        /// </summary>
        public string GOfficeTel { get; set; }
        /// <summary>
        /// 个人  手机
        /// </summary>
        public string GMobilePhone { get; set; }
        /// <summary>
        /// 个人  电邮
        /// </summary>
        public string GEmail { get; set; }
        /// <summary>
        /// 个人  名片
        /// </summary>
        public string GPicPath1 { get; set; }
        /// <summary>
        /// 个人  文件
        /// </summary>
        public string GPicPath2 { get; set; }
       
        public string Purpose1 { get; set; }

        public string Purpose2 { get; set; }

        public string Purpose3 { get; set; }

        public string Purpose4 { get; set; }

        public string Purpose5 { get; set; }

        public string Purpose6 { get; set; }

        public string Purpose7 { get; set; }

        public string Pathway1 { get; set; }

        public string Pathway2 { get; set; }

        public string Pathway3 { get; set; }

        public string Pathway4 { get; set; }

        public string Pathway5 { get; set; }

        public string Pathway6 { get; set; }

        public string Type { get; set; }
    }
}

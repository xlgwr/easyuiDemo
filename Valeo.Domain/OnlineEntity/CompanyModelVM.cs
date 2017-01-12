using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class CompanyModelVM
    {
        #region 实体属性

        /// <summary>
        /// 公司ID
        /// </summary>
        public virtual long Entityid { get; set; }


        /// <summary>
        /// 公司编号
        /// </summary>
        public virtual string CRNo { get; set; }

        /// <summary>
        /// 营业执照号码
        /// </summary>
        public virtual string CIBRNO { get; set; }

        /// <summary>
        /// 0:英文 1:简体 2:繁体
        /// </summary>
        public virtual int Language { get; set; }

        /// <summary>
        /// 公司全名_英文
        /// </summary>
        public virtual string FullName_En { get; set; }

        /// <summary>
        /// 公司全名_中文
        /// </summary>
        public virtual string FullName_Cn { get; set; }

        /// <summary>
        /// 公司国别
        /// </summary>
        public virtual string CountryID { get; set; }

        /// <summary>
        /// 公司类别 0:有限公司 1:无限公司 2:股份公司
        /// </summary>
        public virtual string CompanyType { get; set; }

        /// <summary>
        /// 成立日期
        /// </summary>
        public virtual string IncorporationDate { get; set; }

        /// <summary>
        /// 注册资本
        /// </summary>
        public virtual string AuthorizedCapital { get; set; }

        /// <summary>
        /// 地域 0:本地 1:国外 2:其他
        /// </summary>
        public virtual int Areas { get; set; }
        public virtual string AreasVM { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        public virtual string PlaceRegistration { get; set; }

        /// <summary>
        /// 是否上市(0:未上市 1:已上市)
        /// </summary>
        public virtual int Listed { get; set; }
        public virtual string ListedVM { get; set; }

        /// <summary>
        /// 已发行股票(0:未发行 1:已发行)
        /// </summary>
        public virtual int IssuedShares { get; set; }
        public virtual string IssuedSharesVM { get; set; }

        /// <summary>
        /// 现状(公司注册处有这项)
        /// </summary>
        public virtual string ActiveStatus { get; set; }

        /// <summary>
        /// 清盘模式
        /// </summary>
        public virtual string WindingUpMode { get; set; }

        /// <summary>
        /// 已告解散日期
        /// </summary>
        public virtual string DissolutionDate { get; set; }

        /// <summary>
        /// 押记登记册
        /// </summary>
        public virtual string RegisterOfCharges { get; set; }

        /// <summary>
        /// 重要事项
        /// </summary>
        public virtual string ImportantNote { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 网页源码id 
        /// </summary>
        public virtual long HtmlID { get; set; }

        /// <summary>
        /// 1：线下可见（未审） 2:线上可见（已审）(公司以此为准)
        /// </summary>
        public virtual int Show { get; set; }
        public virtual string ShowVM { get; set; }

        /// <summary>
        /// 数据级别
        /// </summary>
        public virtual string DataGradeID { get; set; }

        public virtual string Enable { get; set; }

       


        #endregion

 
    }
}

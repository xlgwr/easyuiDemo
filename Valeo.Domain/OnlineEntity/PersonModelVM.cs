using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class PersonModelVM
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }


        /// <summary>
        /// 全名
        /// </summary>
        public virtual string FullName_En { get; set; }

        /// <summary>
        /// 简体姓名
        /// </summary>
        public virtual string FullName_Cn { get; set; }

        /// <summary>
        /// 别名英文
        /// </summary>
        public virtual string Alias_En { get; set; }

        /// <summary>
        /// 别名中文
        /// </summary>
        public virtual string Alias_Cn { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 性别 0:男 1:女 2:其他
        /// </summary>
        public virtual int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime Birthdate { get; set; }

        public virtual string BirthdateString
        {
            get
            {
                var date = "";
                var dateTime = Birthdate;
                if (dateTime!=null)
                {
                    date = dateTime.ToString("yyyy-MM-dd");
                    if (date.Equals("0001-01-01"))
                    {
                        date = "";
                    }
                }
                else
                {
                    date = "";
                }

               
                return date;
                
                
            }
           
        }

        /// <summary>
        /// 婚姻状态 0;未婚 1：已婚 2：离婚 3:鳏寡 4、其他
        /// </summary>
        public virtual int MaritalStatus { get; set; }

        /// <summary>
        /// 汉字编码
        /// </summary>
        public virtual string ChineseCode { get; set; }

        /// <summary>
        /// 国籍(Countryid)
        /// </summary>
        public virtual int Nationality { get; set; }

        /// <summary>
        /// 出生地
        /// </summary>
        public virtual string PlaceBirth { get; set; }

        /// <summary>
        /// 1：线下可见（未审） 2:线上可见（已审）(信贷以此为准)
        /// </summary>
        public virtual int Show { get; set; }

        /// <summary>
        /// 数据级别
        /// </summary>
        public virtual string DataGradeID { get; set; }

        /// <summary>
        /// 国籍名称
        /// </summary>
        public virtual string CountryName { get; set; }

        public virtual string GenderVM { get; set; }

        public virtual string MaritalStatusVM { get; set; }
    }
}

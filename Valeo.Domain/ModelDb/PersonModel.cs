
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("s_Person")]
    [PetaPoco.PrimaryKey("Entityid", autoIncrement = false)]
    public class PersonModel
    {
        public class VarKey
        {
            public const string tablename = "s_Person";
            public const string PrimaryKey = "Entityid";
        }
        #region 实体属性

        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }


        private string _FullName_En = "";
        private string _FullName_Cn = "";
        /// <summary>
        /// 全名
        /// </summary>
        public virtual string FullName_En
        {
            get
            {
                return _FullName_En;
            }
            set
            {

                _FullName_En = apiReg.getValueEN(value);

            }

        }

        /// <summary>
        /// 简体姓名
        /// </summary>
        public virtual string FullName_Cn
        {
            get
            {
                return _FullName_Cn;
            }
            set
            {
                _FullName_Cn = apiReg.getValueCN(value);

            }
        }

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
        public virtual int? Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public virtual System.DateTime? Birthdate { get; set; }

        /// <summary>
        /// 婚姻状态 0;未婚 1：已婚 2：离婚 3:鳏寡 4、其他
        /// </summary>
        public virtual int? MaritalStatus { get; set; }

        /// <summary>
        /// 汉字编码
        /// </summary>
        public virtual string ChineseCode { get; set; }

        /// <summary>
        /// 国籍(Countryid)
        /// </summary>
        public virtual int? Nationality { get; set; }

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





        #endregion



    }
    public class apiReg
    {
        public static string getValueCN(string value)
        {
            var _FullName_Cn = value;
            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _FullName_Cn = value.Replace(" ", "");
                }
                return _FullName_Cn;
            }
            catch (Exception ex)
            {
                return _FullName_Cn;
            }
        }
        public static string getValueEN(string value)
        {
            var _FullName_En = value;
            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _FullName_En = value.Replace(",", " ");
                    _FullName_En = apiReg.regDoubleSpace2(_FullName_En);
                }

                return _FullName_En;
            }
            catch (Exception ex)
            {
                return _FullName_En;
            }
        }
        public static string regDoubleSpace2(string strV)
        {
            var v = strV;
            try
            {

                var reg1 = @"[\f\n\r\t\v]";//去除换行符等
                var reg2 = @"[\u3000\u0020]{2,}";//去空格,两个以上空格改为一个，全角、半角

                v = ReplaceReg(reg1, v, " ");

                v = ReplaceReg(reg2, v, " ");


                return v;
            }
            catch (Exception)
            {
                return v;
            }

        }
        public static string ReplaceReg(string patt, string input, string totxt)
        {
            try
            {
                Regex rgx = new Regex(patt, RegexOptions.IgnoreCase);
                return rgx.Replace(input.ToString(), totxt).Trim();
            }
            catch (Exception ex)
            {

                return input;
            }
        }


    }

}

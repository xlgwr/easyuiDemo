using Valeo.Common;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Member
{
    /// <summary>
    /// 会员信息列表
    /// </summary>
    public class MemberListVM
    {
        public string _addtime;
        /// <summary>
        /// 申请时间
        /// </summary>
        public string addtime
        {
            get
            {
                if (!string.IsNullOrEmpty(_addtime))
                {
                    return DataConvert.DateTime2Format(_addtime, Enums.DateTimeFormat.DateTime);
                }
                return _addtime;
            }
            set { _addtime = value; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberID { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string GivenNames { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string FullName_En { get; set; }
        /// <summary>
        /// 中文名Tm
        /// </summary>
        public string FullName_Tm { get; set; }
        /// <summary>
        /// 中文名Cn
        /// </summary>
        public string FullName_Cn { get; set; }
        /// <summary>
        /// 电邮
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string MemberGradeID { get; set; }

        public string LastSeachTime { get; set; }

        /// <summary>
        /// 是否活跃
        /// </summary>
        public string LastSeachTimeString
        {
            get
            {

                if (LastSeachTime != null)
                {
                    try
                    {
                        DateTime? dts = Convert.ToDateTime(LastSeachTime);
                        //DateTime dt = Convert.ToDateTime(DateTime.Now.AddDays(-20).ToShortDateString());
                        DateTime dt = dts.Value.AddDays(+ActiveDay);
                        return dt >= DateTime.Now ? "活跃" : "不活跃";
                    }
                    catch (Exception)
                    {
                        return "不活跃";
                    }
                }
                else
                {
                    return "不活跃";
                }
            }
        }


        [ResultColumn]
        public int ActiveDay { get; set; }


        public string _Enable;
        /// <summary>
        /// 会员状态
        /// </summary>
        public string Enable
        {
            get
            {
                return _Enable == "1" ? "启用" : "禁用";
            }
            set { _Enable = value; }
        }
        /// <summary>
        /// 证券牌照号码
        /// </summary>
        public string SecuritiesLicenceNo { get; set; }
        /// <summary>
        /// 财务牌照号码
        /// </summary>
        public string FinancialLicenseNo { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        public string _RemainingSum { get; set; }
        /// <summary>
        /// 有无按金
        /// </summary>
        public string RemainingSum
        {
            get
            {
                if (!string.IsNullOrEmpty(_RemainingSum))
                {
                    return _RemainingSum = Convert.ToDouble(_RemainingSum) > 0 ? "有" : "无";
                }
                else
                {
                    return _RemainingSum = "无";
                }
            }
            set { _RemainingSum = value; }
        }

        /// <summary>
        /// 查，是否活跃
        /// </summary>
        public string SLastSeachTime { get; set; }

        /// <summary>
        /// 查，会员状态
        /// </summary>
        public string SEnable { get; set; }

        public int IsRemainingSum { get; set; }

        public string HomeTel { get; set; }

        public string OfficeTel { get; set; }

        public string MobilePhone { get; set; }

        public string Salutation { get; set; }
        public string SalutationVM
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Salutation))
                {
                    switch (Salutation)
                    {
                        case "-1":
                            return "";
                            break;
                        case "0":
                            return "Mr";
                            break;
                        case "1":
                            return "Mrs";
                            break;
                        case "2":
                            return "Ms";
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    return "";
                }
                return Salutation;

            }
        }
    }
}

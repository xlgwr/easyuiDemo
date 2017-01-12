using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class EducationVM
    {
        #region 实体属性

        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Educationid { get; set; }


        /// <summary>
        /// 个人编号
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 学校
        /// </summary>
        public virtual string Institute { get; set; }

        /// <summary>
        /// 学校类型
        /// </summary>
        public virtual string InstituteType { get; set; }

        /// <summary>
        /// 学位
        /// </summary>
        public virtual string Degree { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime? FromDate { get; set; }



        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? ToDate { get; set; }



        public virtual string FromDateString
        {
            get
            {
                var dateTime = FromDate;
                return dateTime != null ? dateTime.Value.ToString("yyyy-MM-dd") : "";
            }
            set
            {
                try
                {
                    if (value != null)
                        FromDate = Convert.ToDateTime(value);
                }
                catch
                {
                    // ignored
                }

            }
        }

        public virtual string ToDateString
        {
            get
            {
                var dateTime = ToDate;
                return dateTime != null ? dateTime.Value.ToString("yyyy-MM-dd") : "";
            }
            set
            {
                try
                {
                    if (value != null)
                        ToDate = Convert.ToDateTime(value);
                }
                catch
                {
                    // ignored
                }

            }
        }

        #endregion

    }
}

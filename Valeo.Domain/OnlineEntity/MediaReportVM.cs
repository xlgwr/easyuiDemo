using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class MediaReportVM
    {
        #region 实体属性

        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long MediaReportID { get; set; }


        /// <summary>
        /// entityid
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 媒体名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public virtual string Subject { get; set; }

        /// <summary>
        /// 报导内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 报导日期
        /// </summary>
        public virtual DateTime? ReportDate { get; set; }


        /// <summary>
        /// 报导日期
        /// </summary>
        public virtual string ReportDateString
        {
            get
            {
                var dateTime = ReportDate;
                return dateTime != null ? dateTime.Value.ToString("yyyy-MM-dd") : "";
            }
            set
            {
                try
                {
                    if (value != null)
                        ReportDate = Convert.ToDateTime(value);
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
